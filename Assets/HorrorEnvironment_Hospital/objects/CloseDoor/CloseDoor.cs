using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    private GameObject door;
    private bool close = false;

    private Animator[] temp;
    private Animator doorAnimator;

    public bool doorLock = false;

    // Start is called before the first frame update
    void Start()
    {
        door = GameObject.Find("CG/FLOOR_1st/TOILETS/doors_A");

        temp = GameObject.Find("CG/FLOOR_1st/TOILETS/doors_A").GetComponentsInChildren<Animator>();
        doorAnimator = temp[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision targetObj)
    {
        if (targetObj.gameObject.name == "PLAYER" && close == false)
        {
            Debug.Log("collide with floor");

            if (!doorLock) 
            {
                close = true;
                doorLock = true;
            }
        }

        if (close) {
            doorAnimator.SetTrigger("doorClose");

            GetComponent<AudioSource>().Play();
        }
    }
}
