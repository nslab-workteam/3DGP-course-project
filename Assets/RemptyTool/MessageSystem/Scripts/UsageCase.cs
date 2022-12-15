using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RemptyTool.ES_MessageSystem;

[RequireComponent(typeof(ES_MessageSystem))]
public class UsageCase : MonoBehaviour
{
    private ES_MessageSystem msgSys;
    public UnityEngine.UI.Text uiText;
    public TextAsset[] textAssets;
    public GameObject dialog;
    [SerializeField] private GameObject gameMenuManager;
    public int assetIndex = 0;
    public bool isDialogFinish = false;
    public bool pause = false;
    private List<string> textList = new List<string>();
    private int textIndex = 0;
    

    void Start()
    {
        msgSys = this.GetComponent<ES_MessageSystem>();
        //add special chars and functions in other component.
        // msgSys.AddSpecialCharToFuncMap("UsageCase", CustomizedFunction);
        dialog.SetActive(false);
    }

    public void StartDialog() {
        if (uiText == null)
        {
            Debug.LogError("UIText Component not assign.");
        }
        else {
            Cursor.lockState = CursorLockMode.None;
            gameMenuManager.GetComponent<gameMenu>().StartDialog();
            ReadTextDataFromAsset(textAssets[assetIndex++]);
            textIndex = 0;
            msgSys.Next();
            dialog.SetActive(true);
            pause = false;
        }
    }

    void StartDialog(int i) {
        if (uiText == null)
        {
            Debug.LogError("UIText Component not assign.");
        }
        else{
            Cursor.lockState = CursorLockMode.None;
            gameMenuManager.GetComponent<gameMenu>().StartDialog();
            ReadTextDataFromAsset(textAssets[i]);
            assetIndex = i + 1;
            textIndex = 0;
            dialog.SetActive(true);
            pause = false;
        }
    }

    private void CustomizedFunction()
    {
        Debug.Log("Hi! This is called by CustomizedFunction!");
    }

    private void ReadTextDataFromAsset(TextAsset _textAsset)
    {
        textList.Clear();
        textList = new List<string>();
        textIndex = 0;
        var lineTextData = _textAsset.text.Split('\n');
        foreach (string line in lineTextData)
        {
            textList.Add(line);
        }
    }

    void Update()
    {
        if (pause) return;
        if (Input.GetKeyDown(KeyCode.S))
        {
            //You can sending the messages from strings or text-based files.
            if (msgSys.IsCompleted)
            {
                msgSys.SetText("Send the messages![lr] HelloWorld![w]");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            //Continue the messages, stoping by [w] or [lr] keywords.
            msgSys.Next();
        }

        //If the message is complete, stop updating text.
        if (msgSys.IsCompleted == false)
        {
            uiText.text = msgSys.text;
        }

        //Auto update from textList.
        if (msgSys.IsCompleted == true && textIndex < textList.Count)
        {
            msgSys.SetText(textList[textIndex]);
            textIndex++;
        }

        // Debug.Log("textIndex="+textIndex+", textList.Count="+textList.Count);
        // Debug.Log("isDialogFinish="+isDialogFinish);
        if (textIndex == textList.Count && textList.Count != 0 && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))) {
            isDialogFinish = true;
            dialog.SetActive(false);
            pause = true;
            Cursor.lockState = CursorLockMode.Locked;
            gameMenuManager.GetComponent<gameMenu>().AfterIntroDialog();
        }
    }

    public void SkipDialog() {
        textIndex = textList.Count;
        isDialogFinish = true;
        dialog.SetActive(false);
        pause = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
