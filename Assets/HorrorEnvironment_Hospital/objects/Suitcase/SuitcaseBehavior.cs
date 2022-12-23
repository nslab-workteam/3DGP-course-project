using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitcaseBehavior : MonoBehaviour
{
    [SerializeField] private GameObject inGameUiManager;
    [SerializeField] private PlayerMovement movement;
    public bool hasOpened = false;
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
                hit.collider.name == "suitcase" &&
                movement.isStart) {
                    inGameUiManager.GetComponent<IngameUI>().inGameUIPages[4].SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    GameObject.Find("PLAYER").GetComponent<PlayerMovement>().enabled = false;
                    Camera.main.GetComponent<MouseLook>().isStart = false;
                }
        }
    }
}
