using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OutlineControl : MonoBehaviour
{
    public GameObject[] objects;
    bool[] hasClicked;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject o in objects) {
            try {
                o.GetComponent<Outline>().enabled = false;
                Debug.Log(o.name);
            } catch (Exception e) {
                // Debug.LogError(e.StackTrace);
            }
        }
        hasClicked = new bool[objects.GetLength(0)];
        for(int i=0; i<hasClicked.GetLength(0); i++) {
            hasClicked[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 3f)) {
            for(int i=0; i<objects.GetLength(0); i++) {
                try {
                    if (hit.collider.name == objects[i].name){
                        objects[i].GetComponent<Outline>().enabled = true;
                        if (Input.GetMouseButton(0)) {
                            hasClicked[i] = true;
                        }
                    }else {
                        objects[i].GetComponent<Outline>().enabled = false;
                    }
                } catch (Exception e) {
                    // Debug.LogError(e.StackTrace);
                }
            }
        }
    }
}
