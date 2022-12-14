using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum ObjectToPick {
    scissors,
    doll,
    glove,
    magnifier,
    pillow,
    liquid,
    records,
    formula,
    hammer,
    key,
    none,
}

public class IngameUI : MonoBehaviour
{
    public GameObject inGameUI;
    public GameObject[] inGameUIPages;
    public gameMenu mGameMenu;
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
    private string[] ObjectName = {
        "剪刀", "娃娃", "手套", "放大鏡", "枕頭", "特殊液體", "病歷表", "配方", "鐵鎚", "鑰匙"
    };
    public bool isPotionMixtureFinished = false;
    [SerializeField] private GameObject specialLiquid;
    [Header("Success Page")]
    [SerializeField] private GameObject successPage;
    [SerializeField] private Timer timer;
    [SerializeField] private PickupCupcake cupcakeCounter;
    [SerializeField] private Text timeElapsed;
    [SerializeField] private Text cupCake;
    [SerializeField] private Text frightened;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.Find("PLAYER").GetComponent<PlayerMovement>();
        foreach(GameObject o in inGameUIPages) {
            o.SetActive(false);
        }
        pourTimes = new int[10];
        beaker = GameObject.Find("Beaker");
        specialLiquid.SetActive(false);
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
        Debug.Log("Pick up " + pick + ", index " + (int)pick);
        ShowHint("您已獲得" + ObjectName[(int)pick]);
        slotPointer = (int)pick;
        slotButtons[slotPointer].GetComponent<Image>().sprite = imageList[(int)pick];
        switch(pick) {
            case ObjectToPick.records:
                Button.ButtonClickedEvent _recordsEvent2 = new Button.ButtonClickedEvent();
                _recordsEvent2.AddListener(() => {
                    if (holdObject.GetComponent<HoldingItem>().holdingObjectLeft != -1) return;
                    holdObject.GetComponent<HoldingItem>().holdingObjectLeft = (int)ObjectToPick.records;
                    slotButtons[(int)ObjectToPick.records].GetComponent<Image>().sprite = imageList[(int)ObjectToPick.none];
                    slotButtons[(int)ObjectToPick.records].GetComponent<Button>().enabled = false;
                });
                slotButtons[slotPointer].GetComponent<Button>().onClick = _recordsEvent2;
                break;
            case ObjectToPick.formula:
                slotButtons[slotPointer].GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
                slotButtons[slotPointer].GetComponent<Button>().onClick.AddListener(
                    () => {
                        if (holdObject.GetComponent<HoldingItem>().holdingObjectLeft != -1) return;
                        holdObject.GetComponent<HoldingItem>().holdingObjectLeft = (int)ObjectToPick.formula;
                        slotButtons[(int)ObjectToPick.formula].GetComponent<Image>().sprite = imageList[(int)ObjectToPick.none];
                        slotButtons[(int)ObjectToPick.formula].GetComponent<Button>().enabled = false;
                    }
                );
                break;
            default:
                slotButtons[slotPointer].GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
                slotButtons[slotPointer].GetComponent<Button>().onClick.AddListener(
                    () => {
                        if (holdObject.GetComponent<HoldingItem>().holdingObject != -1) return;
                        holdObject.GetComponent<HoldingItem>().holdingObject = (int)pick;
                        slotButtons[(int)pick].GetComponent<Image>().sprite = imageList[(int)ObjectToPick.none];
                        slotButtons[(int)pick].GetComponent<Button>().enabled = false;
                    }
                );
                break;
        }
    }

    public void ReturnObject() {
        int obj = holdObject.GetComponent<HoldingItem>().holdingObject;
        if (obj == -1) return;
        slotButtons[obj].GetComponent<Image>().sprite = imageList[obj];
        slotButtons[obj].GetComponent<Button>().enabled = true;
        holdObject.GetComponent<HoldingItem>().holdingObject = -1;
    }

    public void ReturnObjectLeft() {
        int obj = holdObject.GetComponent<HoldingItem>().holdingObjectLeft;
        if (obj == -1) return;
        slotButtons[obj].GetComponent<Image>().sprite = imageList[obj];
        slotButtons[obj].GetComponent<Button>().enabled = true;
        holdObject.GetComponent<HoldingItem>().holdingObjectLeft = -1;
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
            Debug.Log("TODO: Finish medical");
            OnPotionBackClick();
            Destroy(GameObject.Find("Beaker"));
            specialLiquid.SetActive(true);
            isPotionMixtureFinished = true;
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
        for(int i=0; i<pourTimes.GetLength(0); i++) {
            pourTimes[i] = 0;
        }
        totalPourTimes = 0;
    }

    public void OnPotionBackClick() {
         foreach (GameObject o in inGameUIPages)
        {
            o.SetActive(false);
        }
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>().isStart = true;
        GameObject.Find("PLAYER").GetComponent<PlayerMovement>().enabled = true;
        playerMovement.isStart = true;
    }

    public void ShowHint(string text) {
        foreach(GameObject o in inGameUIPages) {
            if (o.name == "Hint") {
                o.SetActive(true);
                break;
            }
        }
        hintText.GetComponent<Text>().text = text;
        hintText.GetComponent<Animator>().SetTrigger("showText");
        StartCoroutine(_delayCloseHint());
    }

    IEnumerator _delayCloseHint() {
        yield return new WaitForSeconds(6f);
        foreach(GameObject o in inGameUIPages) {
            if (o.name == "Hint") {
                o.SetActive(false);
                break;
            }
        }
    }

    public void OnKeypadBackClick()
    {
        foreach (GameObject o in inGameUIPages)
        {
            o.SetActive(false);
        }
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>().isStart = true;
        GameObject.Find("PLAYER").GetComponent<PlayerMovement>().enabled = true;
        playerMovement.isStart = true;
    }

    public void LockPlayer() {
        Cursor.lockState = CursorLockMode.None;
        GameObject.FindWithTag("MainCamera").GetComponent<MouseLook>().isStart = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().isStart = false;
    }

    public void UnlockPlayer() {
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindWithTag("MainCamera").GetComponent<MouseLook>().isStart = true;
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().isStart = true;
    }

    public void OnFailed() {
        foreach (GameObject o in inGameUIPages) {
            if (o.name == "FailedPage") {
                o.SetActive(true);
            } else {
                o.SetActive(false);
            }
        }
    }

    public void OnMenuClicked() {
        foreach (GameObject o in inGameUIPages) {
            Debug.Log(o.name);
            o.SetActive(false);
        }
        mGameMenu.menu.SetActive(true);
        foreach (GameObject o in mGameMenu.pages) {
            if (o.name == "MainPage") {
                o.SetActive(true);
            } else {
                o.SetActive(false);
            }
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnSuccess() {
        foreach (GameObject o in inGameUIPages) {
            if (o.name == "SuccessPage") {
                o.SetActive(true);
            } else {
                o.SetActive(false);
            }
        }
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("UpperLeftUI")) {
            o.SetActive(false);
        }

        float timeUsed = 1200 - timer.timeRemaining;
        int second = (int)(timeUsed % 60);
        int minutes = (int)(timeUsed / 60);
        timeElapsed.text = string.Format("{0:00}:{1:00}", minutes, second);
        
        cupCake.text = cupcakeCounter.foundCupcakeNum.ToString();

        frightened.text = GameObject.Find("FrightenCounter").GetComponent<FrightenCounter>().count.ToString();
    }

    public void OnSuccessEasy() {
        foreach (GameObject o in inGameUIPages) {
            if (o.name == "SuccessPage") {
                o.SetActive(true);
            } else {
                o.SetActive(false);
            }
        }
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("UpperLeftUI")) {
            o.SetActive(false);
        }
        float timeUsed = 1200 - timer.timeRemaining;
        int second = (int)(timeUsed % 60);
        int minutes = (int)(timeUsed / 60);
        timeElapsed.text = string.Format("{0:00}:{1:00}", minutes, second);
        cupCake.text = "";
        frightened.text = "";
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("AchievementText")) {
            o.SetActive(false);
        }
        
    }
}
