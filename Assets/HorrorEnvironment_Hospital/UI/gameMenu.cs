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
    private string breadcrum;
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
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            GamePause();
        }
        pages[1].GetComponentInChildren<Slider>().SetValueWithoutNotify(volume);
    }

    void GamePause() {
        Cursor.lockState = CursorLockMode.None;
        menu.SetActive(true);
        aim.SetActive(false);
        player.GetComponentInChildren<Camera>().GetComponent<MouseLook>().isStart = false;
        GameObject.Find("StartButton").GetComponentInChildren<TextMeshProUGUI>().text = "Resume";
    }

    public void OnStartClick() {
        Cursor.lockState = CursorLockMode.Locked;
        menu.SetActive(false);
        aim.SetActive(true);
        player.GetComponentInChildren<Camera>().GetComponent<MouseLook>().isStart = true;
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
}
