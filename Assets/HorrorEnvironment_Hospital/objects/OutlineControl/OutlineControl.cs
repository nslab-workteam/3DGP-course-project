using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OutlineControl : MonoBehaviour
{
    public GameObject[] objects;
    public bool[] canClicked;
    public enum objectMapping {
        MusicBox, curtain, layingDeadCarpet, cuteCatPic,
        PaperStack, LittleGril, Scissor, Pillow,
        Magnify, suitcase, Potions, SpecialLiquid,
        Teddy, button_1f, button_2f, button_b1
    }

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
        canClicked = new bool[objects.GetLength(0)];
        for(int i=0; i<canClicked.GetLength(0); i++) {
            canClicked[i] = false;
        }
        for(int i=0; i<4; i++) {
            canClicked[i] = true;
        }
        for(int i=13; i<16; i++) {
            canClicked[i] = true;
        }
        canClicked[(int)objectMapping.PaperStack] = true;
        canClicked[(int)objectMapping.LittleGril] = true;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 3f)) {
            for(int i=0; i<objects.GetLength(0); i++) {
                try {
                    Reclickable clickTest;
                    if (hit.collider.name == objects[i].name && canClicked[i]){
                        objects[i].GetComponent<Outline>().enabled = true;
                        if (Input.GetMouseButton(0)) {
                            canClicked[i] = true;
                        }
                    }else if (hit.collider.gameObject.TryGetComponent<Reclickable>(out clickTest)) {
                        hit.collider.gameObject.GetComponent<Outline>().enabled = true;
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
