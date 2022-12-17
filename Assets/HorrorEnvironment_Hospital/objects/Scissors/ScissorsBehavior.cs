using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorsBehavior : MonoBehaviour
{
    [SerializeField] private GameObject littleGirl;
    GameObject inGameUiManager;
    public bool hasPickedUp = false;

    // Start is called before the first frame update
    void Start()
    {
        inGameUiManager = GameObject.Find("IngameUIManager");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 3f)) {
            if (Input.GetMouseButtonDown(0) && hit.collider.name == "Scissor") {
                if (littleGirl.GetComponent<LittleGirlBehavior>().hasTalkedTo) {
                    inGameUiManager.GetComponent<IngameUI>().pickUp(ObjectToPick.scissors);
                    inGameUiManager.GetComponent<IngameUI>().ShowHint("您已獲得剪刀");
                    GameObject.Find("Scissor").SetActive(false);
                    hasPickedUp = true;
                }
            }
        }
    }
}
