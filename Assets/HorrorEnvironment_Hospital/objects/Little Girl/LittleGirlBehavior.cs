using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleGirlBehavior : MonoBehaviour
{
    [SerializeField] private GameObject dialogManager;
    [SerializeField] private GameObject paperStack;
    [SerializeField] private HoldingItem hold;
    [SerializeField] private PlayerMovement movement;
    
    bool startDialogFlg = false;
    public bool hasTalkedTo = false;
    int stage = 0;
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
            if (Input.GetMouseButtonDown(0) && hit.collider.name == "Little Girl" && movement.isStart) {
                if (stage == 0) {
                    if (paperStack.GetComponent<PaperStackBehavior>().isPickedUp &&
                        hold.holdingObjectLeft == (int)ObjectToPick.records) {
                            Debug.Log("Has picked up record");
                            StartDialogOnce(ref startDialogFlg, 1);
                            stage = 1;
                    }else {
                        Debug.Log("Haven't picked up record");
                        StartDialogOnce(ref startDialogFlg, 2);
                    }
                }
                if (stage == 1) {
                    if (hold.holdingObject == (int)ObjectToPick.doll) {
                        StartDialogOnce(ref startDialogFlg, 3);
                    }
                }
            }
        }
        if (dialogManager.GetComponent<UsageCase>().isDialogFinish) {
            startDialogFlg = false;
            hasTalkedTo = true;
        }
    }

    void StartDialogOnce(ref bool flg, int index) {
        if (!flg) {
            dialogManager.GetComponent<UsageCase>().StartDialog(index);
            flg = true;
        }
    }
}
