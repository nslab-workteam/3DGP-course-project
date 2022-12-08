using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameUI : MonoBehaviour
{
    public GameObject inGameUI;
    public GameObject[] inGameUIPages;
    public GameObject backpack;
    PlayerMovement playerMovement;
    bool isBackpackOpened = false;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.Find("PLAYER").GetComponent<PlayerMovement>();
        foreach(GameObject o in inGameUIPages) {
            o.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) {
            if (!isBackpackOpened) {
                inGameUI.SetActive(true);
                backpack.SetActive(true);
                isBackpackOpened = true;
                Cursor.lockState = CursorLockMode.None;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>().isStart = false;
                playerMovement.isStart = false;
            } else {
                inGameUI.SetActive(true);
                backpack.SetActive(false);
                isBackpackOpened = false;
                Cursor.lockState = CursorLockMode.Locked;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>().isStart = true;
                playerMovement.isStart = true;
            }
        }
    }
}
