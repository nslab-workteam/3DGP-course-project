using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBehavior : MonoBehaviour
{
    [SerializeField] private GameObject inGameUiManager;
    [SerializeField] private GameObject suitcase;
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
            if (Input.GetMouseButtonDown(0) && 
                hit.collider.name == "Recipe" &&
                suitcase.GetComponent<SuitcaseBehavior>().hasOpened) {
                    inGameUiManager.GetComponent<IngameUI>().pickUp(ObjectToPick.formula);
                    GameObject.Find("Recipe").SetActive(false);
                }
        }
    }
}
