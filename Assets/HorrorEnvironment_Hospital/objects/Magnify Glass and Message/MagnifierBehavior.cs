using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifierBehavior : MonoBehaviour
{
    [SerializeField] private GameObject inGameUiManager;
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
                hit.collider.name == "Magnify Glass") {
                    inGameUiManager.GetComponent<IngameUI>().pickUp(ObjectToPick.magnifier);
                    GameObject.Find("Magnify Glass").SetActive(false);
            }
        }
    }
}
