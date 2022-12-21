using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    public bool isPickedUp = false;
    GameObject inGameUiManager;
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
        if (Physics.Raycast(ray, out hit, 3f)) 
        {
            if (Input.GetMouseButtonDown(0) && 
                hit.collider.name == "Key") 
                {
                    isPickedUp = true;
                    GameObject.Find("Key").SetActive(false);
                    inGameUiManager.GetComponent<IngameUI>().pickUp(ObjectToPick.key);
            }
        }
    }
}
