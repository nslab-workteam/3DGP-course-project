using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ObjectToPick {
    scissors,
    doll,
    pass_case,
    glove,
    magnifier,
    pillow,
    liquid,
    records,
    formula
}

public class IngameUI : MonoBehaviour
{
    public GameObject inGameUI;
    public GameObject[] inGameUIPages;
    public GameObject backpack;
    public Sprite[] imageList;
    public GameObject[] slotButtons;
    PlayerMovement playerMovement;
    bool isBackpackOpened = false;
    int slotPointer = 0;
    int recordPagePointer = 1;

    public GameObject holdObject;

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

    public void pickUp(ObjectToPick pick) {
        for (int i = 0; i < 10; i++) {
            if (slotButtons[i].GetComponent<Image>().sprite == null) 
            {
                slotPointer = i;
                break;
            }
        }
        slotButtons[slotPointer].GetComponent<Image>().sprite = imageList[(int)pick];
        switch(pick) {
            case ObjectToPick.scissors:
                break;
            case ObjectToPick.records:
                Button.ButtonClickedEvent recordsEvent = new Button.ButtonClickedEvent();
                recordsEvent.AddListener(() => {
                    foreach (GameObject o in inGameUIPages)
                    {
                        if (o.name == "MedicalRecordPage")
                        {
                            o.SetActive(true);
                        }
                        else
                        {
                            o.SetActive(false);
                        }
                    }
                });
                slotButtons[slotPointer].GetComponent<Button>().onClick = recordsEvent;
                break;
        }
    }

    public void OnRecordNextClick() {
        recordPagePointer++;
        if (recordPagePointer > 3) {
            recordPagePointer = 1;
        }
        for(int i=1; i<3; i++) {
            GameObject.Find("Page"+i).GetComponent<Image>().enabled = true;
        }
        for(int i=1; i<recordPagePointer; i++) {
            GameObject.Find("Page"+i).GetComponent<Image>().enabled = false;
        }
    }

    public void OnRecordPreviousClick() {
        recordPagePointer--;
        if (recordPagePointer < 1) {
            recordPagePointer = 3;
        }
        for(int i=1; i<3; i++) {
            GameObject.Find("Page"+i).GetComponent<Image>().enabled = true;
        }
        for(int i=1; i<recordPagePointer; i++) {
            GameObject.Find("Page"+i).GetComponent<Image>().enabled = false;
        }
    }

    public void OnRecordBackClick()
    {
        foreach (GameObject o in inGameUIPages)
        {
            o.SetActive(false);
        }
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>().isStart = true;
        playerMovement.isStart = true;
        isBackpackOpened = false;
    }

}
