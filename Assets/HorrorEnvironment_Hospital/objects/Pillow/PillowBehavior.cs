using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowBehavior : MonoBehaviour
{
    [SerializeField] private GameObject scissors;
    [SerializeField] private GameObject scissors2;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 3f)) {
            if (Input.GetMouseButtonDown(0) && 
                hit.collider.name == "Pillow" && 
                player.GetComponent<HoldingItem>().holdingObject == (int)ObjectToPick.scissors &&
                scissors.GetComponent<ScissorsBehavior>().hasPickedUp) {
                
            }
        }
    }
}
