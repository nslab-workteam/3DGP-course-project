using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public GameObject liquidSimulator;
    PlayerMovement playerMovement;
    bool isBackpackOpened = false;
    int slotPointer = 0;
    int recordPagePointer = 1;
    int[] pourTimesLimit = {3, 1, 2, 4, 6, 2, 3, 3, 5, 1};
    int[] pourTimes;
    int totalPourTimes = 0;
    GameObject beaker;
    [SerializeField] GameObject hintText;

    public GameObject holdObject;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.Find("PLAYER").GetComponent<PlayerMovement>();
        foreach(GameObject o in inGameUIPages) {
            o.SetActive(false);
        }
        pourTimes = new int[10];
        beaker = GameObject.Find("Beaker");
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

    public void PourPotion(int n) {
        if (n == 1 || n == 3 || n == 5) {
            liquidSimulator.GetComponent<CopyParticle>().AddMoreLiquid(Color.red);
        } else if (n == 2 || n == 4 || n == 6) {
            liquidSimulator.GetComponent<CopyParticle>().AddMoreLiquid(Color.blue);
        } else if (n == 7 || n == 9) {
            liquidSimulator.GetComponent<CopyParticle>().AddMoreLiquid(Color.green);
        } else {
            liquidSimulator.GetComponent<CopyParticle>().AddMoreLiquid(new Color(255f, 135f, 0f));
        }
        pourTimes[n-1]++;
        int count = 0;
        for(int i=0; i<10; i++) {
            if (pourTimesLimit[i] != pourTimes[i])
                break;
            else
                count++;
        }
        if (count == 10) {
            // TODO: Finish medical
        }
        totalPourTimes++;
        if (totalPourTimes > 35) {
            totalPourTimes = 0;
            for(int i=0; i<10; i++) {
                pourTimes[i] = 0;
            }
            ClearBeaker();
        }
    }

    public void ClearBeaker() {
        Animator beakerAnimator = GameObject.Find("Beaker").GetComponent<Animator>();
        beakerAnimator.SetTrigger("pouring");
    }

    public void OnPotionBackClick() {
         foreach (GameObject o in inGameUIPages)
        {
            o.SetActive(false);
        }
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>().isStart = true;
        playerMovement.isStart = true;
    }

    public void ShowHint(string text) {
        foreach(GameObject o in inGameUIPages) {
            if (o.name == "Hint") {
                o.SetActive(true);
                break;
            }
        }
        hintText.GetComponent<TextMeshProUGUI>().text = text;
        hintText.GetComponent<Animator>().SetTrigger("showText");
    }

    IEnumerator CloseHintAfterSecond() {
        yield return new WaitForSeconds(6f);
        foreach(GameObject o in inGameUIPages) {
            if (o.name == "Hint") {
                o.SetActive(false);
                break;
            }
        }
    }

}
