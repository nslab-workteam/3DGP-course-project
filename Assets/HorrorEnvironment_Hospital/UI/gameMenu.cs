using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

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
    public int usedCharater;
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

    //��
    public int pageIndex = 0;
    public UnityEngine.UI.Button PreviousPageBtn;
    public UnityEngine.UI.Button NextPageBtn;
    public GameObject UIRoomChar1;
    public GameObject UIRoomChar2;
    private GameObject SceneChar1;
    private GameObject SceneChar2;
    public int usingChar;
    public GameObject[] CG;
    private float LightIntensity = 0f;

    // character camera
    GameObject cam1;
    GameObject cam2;


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

        //��
        SceneChar1 = GameObject.Find("PLAYER/character1");
        SceneChar2 = GameObject.Find("PLAYER/character2");

        cam1 = GameObject.Find("PLAYER/character1/Main Camera");
        cam2 = GameObject.Find("PLAYER/character2/Main Camera");

        usingChar = 1;
        SceneChar2.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        // Game pause
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GamePause();
        }

        //// Volume slider update
        //pages[1].GetComponentInChildren<Slider>().SetValueWithoutNotify(volume);

        //// Brightness slider update
        //pages[1].GetComponentInChildren<Slider>().SetValueWithoutNotify(LightIntensity);

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

        // Volume slider update
        pages[1].GetComponentsInChildren<Slider>()[0].SetValueWithoutNotify(volume);
    }

    public void OnVolumeDownClick() {
        volume = Mathf.Clamp(volume - 0.1f, 0f, 1f);
        foreach(GameObject s in SFX) {
            s.GetComponent<AudioSource>().volume = volume;
        }

        // Volume slider update
        pages[1].GetComponentsInChildren<Slider>()[0].SetValueWithoutNotify(volume);
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
        if (SceneChar1.activeSelf)
        {
            cam1.GetComponent<MouseLook>().isStart = true;
        }
        else {
            cam2.GetComponent<MouseLook>().isStart = true;
        }
        player.GetComponent<PlayerMovement>().isStart = true;
    }

    public void OnCharacterSelectionClick()
    {
        foreach (GameObject o in pages)
        {
            if (o.name == "CharacterPage")
            {
                o.SetActive(true);
            }
            else
            {
                o.SetActive(false);
            }
        }
        //��
        PreviousPageBtn.gameObject.SetActive(false);
        UIRoomChar2.SetActive(false);
    }

    //��
    public void OnNextButtonClick()
    {
        if (pageIndex == 0)
        {
            pageIndex = 1;
            PreviousPageBtn.gameObject.SetActive(true);
            NextPageBtn.gameObject.SetActive(false);
            UIRoomChar2.SetActive(true);
            UIRoomChar1.SetActive(false);
            //�]�w��������character
            SceneChar2.SetActive(true);
            SceneChar1.SetActive(false);
            usingChar = 2;
        }
    }

    public void OnPreviousButtonClick()
    {
        if (pageIndex == 1)
        {
            pageIndex = 0;
            PreviousPageBtn.gameObject.SetActive(false);
            NextPageBtn.gameObject.SetActive(true);
            UIRoomChar2.SetActive(false);
            UIRoomChar1.SetActive(true);
            //�]�w��������character
            SceneChar1.SetActive(true);
            SceneChar2.SetActive(false);
            usingChar = 1;
        }
    }

    public void OnDemoVideoClick()
    {
        foreach (GameObject o in pages)
        {
            if (o.name == "DemoVideoPage")
            {
                o.SetActive(true);
            }
            else
            {
                o.SetActive(false);
            }
        }
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
        state.usedCharater = usingChar;
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
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void OnBrightUpClick()
    {
        //volume = Mathf.Clamp(volume + 0.1f, 0f, 1f);
        LightIntensity = Mathf.Clamp(LightIntensity+0.5f, 0f, 8f);
        foreach (GameObject s in CG)
        {
            s.GetComponent<Light>().intensity = LightIntensity;
        }

        // Brightness slider update
        pages[1].GetComponentsInChildren<Slider>()[1].SetValueWithoutNotify(LightIntensity);
    }

    public void OnBrightDownClick()
    {
        LightIntensity = Mathf.Clamp(LightIntensity - 0.5f, 0f, 8f);
        foreach (GameObject s in CG)
        {
            s.GetComponent<Light>().intensity = LightIntensity;
        }

        // Brightness slider update
        pages[1].GetComponentsInChildren<Slider>()[1].SetValueWithoutNotify(LightIntensity);
    }
    
    void SetPlayerSkin(int c) {
        // TODO
    }
}
