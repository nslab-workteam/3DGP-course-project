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
    public GameObject startButton;
    public GameObject resumeButton;
    private string breadcrum = "None";
    private float volume = 0.5f;


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
    }

    public void AfterIntroDialog() {
        aim.SetActive(true);
        dialogue.SetActive(false);
        player.GetComponentInChildren<Camera>().GetComponent<MouseLook>().isStart = true;
        player.GetComponent<PlayerMovement>().isStart = true;
    }
}
