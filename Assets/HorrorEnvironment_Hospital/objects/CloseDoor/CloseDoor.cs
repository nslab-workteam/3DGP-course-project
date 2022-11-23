using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    private GameObject door;
    private bool close = false;
    // Start is called before the first frame update
    void Start()
    {
        door = GameObject.Find("CG/FLOOR_1st/TOILETS/doors_A");
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
            
            close = true;
        }

        if (close) {
            door.transform.rotation = Quaternion.Euler(0.0f, 90f, 0.0f);
            //door.transform.Rotate(0f, -63f, 0f);
            GetComponent<AudioSource>().Play();
        }
    }
}
