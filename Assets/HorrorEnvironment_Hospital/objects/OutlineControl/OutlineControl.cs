using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineControl : MonoBehaviour
{
    public Camera mainCam;
    public GameObject[] objects;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject o in objects) {
            o.GetComponent<Outline>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 3f)) {
            foreach (GameObject o in objects) {
                if (hit.collider.name == o.name)
                    o.GetComponent<Outline>().enabled = true;
                else
                    o.GetComponent<Outline>().enabled = false;
            }
        }
    }
}
