using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

enum Character {
    BOY,
    GIRL
}

enum GameProcess {
    AnnieGhost = 0b1,
    Curtain = 0b10,
    MusicBox = 0b100,
    Picture = 0b1000
}

class PlayerState {
    public string timestamp;
    public Vector3 playerPos;
    public Quaternion rotation;
    public Character usedCharater;
    public int gameProcess;
}

public class gameMenu : MonoBehaviour
{

    public GameObject player;
    public GameObject menu;
    public List<GameObject> lightToClose;
    public GameObject[] pages;
    public GameObject[] SFX;
    public GameObject aim;
    public GameObject dialogue;
    public GameObject dialogueManager;
    public GameObject startButton;
    public GameObject resumeButton;
    public GameObject LoadSaveCheck_Menu;
    [Header("Game Process")]
    public GameObject[] mechs;
    private string breadcrum = "None";
    private float volume = 0.5f;
    private int stateSlot = 0;


    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Fog particals").GetComponentInChildren<ParticleSystem>().Stop();
        foreach(GameObject o in lightToClose) {
            Destroy(o);
        }
        foreach(GameObject o in pages) {
            if (o.name == "MainPage") {
                o.SetActive(true);
            } else {
                o.SetActive(false);
            }
        }
        aim.SetActive(false);
        dialogue.SetActive(false);
        resumeButton.SetActive(false);
        startButton.SetActive(true);
        player.GetComponentInChildren<Camera>().GetComponent<MouseLook>().isStart = false;
        player.GetComponent<PlayerMovement>().isStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Game pause
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GamePause();
        }

        // Volume slider update
        pages[1].GetComponentInChildren<Slider>().SetValueWithoutNotify(volume);

        // Check dialog finish
        if (dialogueManager.GetComponentInChildren<UsageCase>().isDialogFinish) {
            Debug.Log("AfterIntroDialog");
            AfterIntroDialog();
            dialogueManager.GetComponentInChildren<UsageCase>().isDialogFinish = false;
        }
    }

    void GamePause() {
        Cursor.lockState = CursorLockMode.None;
        menu.SetActive(true);
        aim.SetActive(false);
        player.GetComponentInChildren<Camera>().GetComponent<MouseLook>().isStart = false;
        resumeButton.SetActive(true);
        startButton.SetActive(false);
        dialogueManager.GetComponentInChildren<UsageCase>().pause = true;
    }

    public void OnStartClick() {
        menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        // call msgSys
        dialogueManager.GetComponentInChildren<UsageCase>().StartDialog();
        dialogueManager.GetComponentInChildren<UsageCase>().pause = false;
        // wait for msgSys finish
    }

    public void OnResumeClick() {
        menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        dialogueManager.GetComponentInChildren<UsageCase>().pause = false;
    }

    public void OnSettingClick() {
        foreach(GameObject o in pages) {
            if (o.name == "SettingPage") {
                o.SetActive(true);
            } else {
                o.SetActive(false);
            }
        }
    }

    public void OnBackClick() {
        foreach(GameObject o in pages) {
            if (o.name == "MainPage") {
                o.SetActive(true);
            } else {
                o.SetActive(false);
            }
        }
    }

    public void OnVolumeUpClick() {
        volume = Mathf.Clamp(volume + 0.1f, 0f, 1f);
        foreach(GameObject s in SFX) {
            s.GetComponent<AudioSource>().volume = volume;
        }
    }

    public void OnVolumeDownClick() {
        volume = Mathf.Clamp(volume - 0.1f, 0f, 1f);
        foreach(GameObject s in SFX) {
            s.GetComponent<AudioSource>().volume = volume;
        }
    }

    public void OnLoadSaveClick() {
        foreach(GameObject o in pages) {
            if (o.name == "Load_SavePage") {
                o.SetActive(true);
            } else {
                o.SetActive(false);
            }
        }
        LoadSaveCheck_Menu.SetActive(false);
        for(int i=1; i<=6; i++) {
            if (System.IO.File.Exists(System.IO.Path.Combine(Application.streamingAssetsPath, "record"+i))) {
                StreamReader file = new StreamReader(System.IO.Path.Combine(Application.streamingAssetsPath, "record"+i));
                string loadJson = file.ReadToEnd();
                file.Close();
                PlayerState state;
                state = JsonUtility.FromJson<PlayerState>(loadJson);

                GameObject button = GameObject.Find("Record"+i);
                TextMeshProUGUI textArea = button.GetComponentInChildren<TextMeshProUGUI>();
                textArea.text = state.timestamp;
            }
        }
    }

    public void AfterIntroDialog() {
        aim.SetActive(true);
        dialogue.SetActive(false);
        player.GetComponentInChildren<Camera>().GetComponent<MouseLook>().isStart = true;
        player.GetComponent<PlayerMovement>().isStart = true;
    }

    public void OnSaveState(int index) {
        LoadSaveCheck_Menu.SetActive(true);
        stateSlot = index;
    }

    public void OnSaveClick() {
        GameObject button = GameObject.Find("Record"+stateSlot);
        TextMeshProUGUI textArea = button.GetComponentInChildren<TextMeshProUGUI>();
        textArea.text = System.DateTime.Now.ToString();
        // TODO: save json
        PlayerState state = new PlayerState();
        state.timestamp = textArea.text;
        state.playerPos = player.transform.position;
        state.rotation = player.transform.rotation;
        state.gameProcess = GetGameProcess();
        state.usedCharater = Character.GIRL;
        string saveString = JsonUtility.ToJson(state);
        StreamWriter file = new StreamWriter(System.IO.Path.Combine(Application.streamingAssetsPath, "record" + stateSlot));
        file.Write(saveString);
        file.Close();

        LoadSaveCheck_Menu.SetActive(false);
    }

    public void OnLoadClick() {
        // TODO: load json
        StreamReader file = new StreamReader(System.IO.Path.Combine(Application.streamingAssetsPath, "record"+stateSlot));
        string loadJson = file.ReadToEnd();
        file.Close();
        PlayerState state;
        state = JsonUtility.FromJson<PlayerState>(loadJson);
        player.transform.position = state.playerPos;
        player.transform.rotation = state.rotation;
        RestoreGameProcess(state.gameProcess);
        SetPlayerSkin(state.usedCharater);

        LoadSaveCheck_Menu.SetActive(false);
    }

    public void OnCancelSaveLoadClick() {
        LoadSaveCheck_Menu.SetActive(false);
    }

    int GetGameProcess() {
        int state = 0;
        foreach(var m in mechs) {
            if (m.name == "Annie Ghost") {
                if (m.GetComponent<AnnieBehaviour>().isActivated()) {
                    state = state | (int)GameProcess.AnnieGhost;
                }
            }
            if (m.name == "curtain") {
                if (m.GetComponent<DropBehaviourScript>().isActivated()) {
                    state = state | (int)GameProcess.Curtain;
                }
            }
            if (m.name == "MusicBox_Model") {
                if (m.GetComponent<activateMusicbox>().isActivated()) {
                    state = state | (int)GameProcess.MusicBox;
                }
            }
            if (m.name == "Picture Variant") {
                if (m.GetComponentInChildren<pictureFalling>().isActivated()) {
                    state = state | (int)GameProcess.Picture;
                }
            }
        }
        return state;
    }

    void RestoreGameProcess(int process) {
        foreach(var m in mechs) {
            if (m.name == "Annie Ghost") {
                if ((process & (int)GameProcess.AnnieGhost) != 0) {
                    m.GetComponent<AnnieBehaviour>().Skip();
                }
            }
            if (m.name == "curtain") {
                if ((process & (int)GameProcess.Curtain) != 0) {
                    m.GetComponent<DropBehaviourScript>().Skip();
                }
            }
            if (m.name == "MusicBox_Model") {
                if ((process & (int)GameProcess.MusicBox) != 0) {
                    m.GetComponent<activateMusicbox>().Skip();
                }
            }
            if (m.name == "Picture Variant") {
                if ((process & (int)GameProcess.Picture) != 0) {
                    m.GetComponentInChildren<pictureFalling>().Skip();
                }
            }
        }
    }

    void SetPlayerSkin(Character c) {
        // TODO
    }
}
