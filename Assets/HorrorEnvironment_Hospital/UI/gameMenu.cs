using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    private string breadcrum;
    private float volume = 0.5f;

    //湘的
    public int pageIndex = 0;
    public UnityEngine.UI.Button PreviousPageBtn;
    public UnityEngine.UI.Button NextPageBtn;
    public GameObject UIRoomChar1;
    public GameObject UIRoomChar2;
    private GameObject SceneChar1;
    private GameObject SceneChar2;


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
        player.GetComponentInChildren<Camera>().GetComponent<MouseLook>().isStart = false;
        player.GetComponent<PlayerMovement>().isStart = false;

        //湘的
        SceneChar1 = GameObject.Find("PLAYER/character1");
        SceneChar2 = GameObject.Find("PLAYER/character2");
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
            AfterIntroDialog();
        }
    }

    void GamePause() {
        Cursor.lockState = CursorLockMode.None;
        menu.SetActive(true);
        aim.SetActive(false);
        player.GetComponentInChildren<Camera>().GetComponent<MouseLook>().isStart = false;
        GameObject.Find("StartButton").GetComponentInChildren<TextMeshProUGUI>().text = "Resume";
    }

    public void OnStartClick() {
        menu.SetActive(false);
        // call msgSys
        dialogueManager.GetComponentInChildren<UsageCase>().StartDialog();
        // wait for msgSys finish
        
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
    }

    public void AfterIntroDialog() {
        Cursor.lockState = CursorLockMode.Locked;
        aim.SetActive(true);
        dialogue.SetActive(false);
        player.GetComponentInChildren<Camera>().GetComponent<MouseLook>().isStart = true;
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
        //湘的
        PreviousPageBtn.gameObject.SetActive(false);
        UIRoomChar2.SetActive(false);
    }

    //湘的
    public void OnNextButtonClick()
    {
        if (pageIndex == 0)
        {
            pageIndex = 1;
            PreviousPageBtn.gameObject.SetActive(true);
            NextPageBtn.gameObject.SetActive(false);
            UIRoomChar2.SetActive(true);
            UIRoomChar1.SetActive(false);
            //設定場景中的character
            SceneChar1.SetActive(false);
            SceneChar2.SetActive(true);
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
            //設定場景中的character
            SceneChar1.SetActive(true);
            SceneChar2.SetActive(false);
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
}
