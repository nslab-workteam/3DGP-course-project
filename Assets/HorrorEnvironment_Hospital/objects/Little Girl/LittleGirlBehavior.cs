using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleGirlBehavior : MonoBehaviour
{
    [SerializeField] private GameObject dialogManager;
    [SerializeField] private GameObject paperStack;
    
    bool startDialogFlg = false;
    public bool hasTalkedTo = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 3f)) {
            if (Input.GetMouseButtonDown(0) && hit.collider.name == "Little Girl") {
                if (paperStack.GetComponent<PaperStackBehavior>().isPickedUp) {
                    Debug.Log("Has picked up record");
                    StartDialogOnce(ref startDialogFlg);
                } else {
                    Debug.Log("Haven't picked up record");
                }
            }
        }
        if (dialogManager.GetComponent<UsageCase>().isDialogFinish) {
            startDialogFlg = false;
            hasTalkedTo = true;
        }
    }

    void StartDialogOnce(ref bool flg) {
        if (!flg) {
            dialogManager.GetComponent<UsageCase>().StartDialog(1);
            flg = true;
        }
    }
}
