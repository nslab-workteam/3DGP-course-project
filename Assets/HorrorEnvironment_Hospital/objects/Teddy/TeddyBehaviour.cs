using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyBehaviour : MonoBehaviour
{
    [SerializeField] private IngameUI inGameUi;
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
            if (Input.GetMouseButtonDown(0)) {
                if (hit.collider.name == "Teddy") {
                    inGameUi.pickUp(ObjectToPick.doll);
                    GameObject.Find("Teddy").SetActive(false);
                }
            }
        }
    }
}
