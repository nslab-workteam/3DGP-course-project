using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldingItem : MonoBehaviour
{
    [SerializeField] private GameObject inGameUiManager;
    [SerializeField] private GameObject holdObject;
    [SerializeField] private GameObject holdObjectLeft;
    public int holdingObject = -1;
    public int holdingObjectLeft = -1;
    [SerializeField] private Sprite[] imageList;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (holdingObject != -1){
            holdObject.GetComponent<Image>().enabled = true;
            holdObject.GetComponent<Image>().sprite = imageList[(int)holdingObject];
        }else{
            holdObject.GetComponent<Image>().enabled = false;
        }
        if (holdingObjectLeft != -1){
            holdObjectLeft.GetComponent<Image>().enabled = true;
            holdObjectLeft.GetComponent<Image>().sprite = imageList[(int)holdingObjectLeft];
        }else{
            holdObjectLeft.GetComponent<Image>().enabled = false;
        }
        if (holdingObjectLeft == (int)ObjectToPick.records && Input.GetKeyDown(KeyCode.Q)) {
            Cursor.lockState = CursorLockMode.None;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>().isStart = false;
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().isStart = false;
            foreach (GameObject o in inGameUiManager.GetComponent<IngameUI>().inGameUIPages)
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
        }
        if (holdingObjectLeft == (int)ObjectToPick.formula && Input.GetKeyDown(KeyCode.Q)) {
            Cursor.lockState = CursorLockMode.None;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>().isStart = false;
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().isStart = false;
            foreach (GameObject o in inGameUiManager.GetComponent<IngameUI>().inGameUIPages)
            {
                if (o.name == "RecipePage")
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
}
