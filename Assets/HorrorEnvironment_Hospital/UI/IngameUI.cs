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
        }
    }

    public void OnBackClick()
    {

    }

    //public void OnSlot1Click() 
    //{
    //    if (slotButtons[0].GetComponent<Image>().sprite == null)    //�Ū��A�٪F��
    //    {
    //        if (holdObject.GetComponent<Image>().sprite != null)
    //        {
    //            slotButtons[0].GetComponent<Image>().sprite = holdObject.GetComponent<Image>().sprite;
    //            holdObject.GetComponent<Image>().sprite = null;
    //        }
    //    }
    //    else if (slotButtons[0].GetComponent<Image>().sprite == "PaperStack")
    //    {
    //        foreach (GameObject o in inGameUIPages)
    //        {
    //            if (o.name == "MedicalRecordPage")
    //            {
    //                o.SetActive(true);
    //            }
    //            else
    //            {
    //                o.SetActive(false);
    //            }
    //        }
    //    }
    //    else
    //    { //���F��

    //    }
    //}

}
