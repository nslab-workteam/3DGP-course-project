using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixPotionsBehaviour : MonoBehaviour
{
    public GameObject potionMixturePage;
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
                potionMixturePage.SetActive(true);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>().isStart = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
