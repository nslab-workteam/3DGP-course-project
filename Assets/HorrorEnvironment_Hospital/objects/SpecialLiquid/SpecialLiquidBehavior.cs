using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialLiquidBehavior : MonoBehaviour
{
    [SerializeField] private GameObject inGameUiManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inGameUiManager.GetComponent<IngameUI>().isPotionMixtureFinished) {
            

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 3f)) {
                if (Input.GetMouseButtonDown(0)) {
                    if (hit.collider.name == "SpecialLiquid") {
                        inGameUiManager.GetComponent<IngameUI>().pickUp(ObjectToPick.liquid);
                        GameObject.Find("SpecialLiquid").SetActive(false);
                    }
                }
            }
        }
    }
}
