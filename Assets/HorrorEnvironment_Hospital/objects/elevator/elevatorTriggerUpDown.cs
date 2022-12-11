using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatorTriggerUpDown : MonoBehaviour
{
    public GameObject parentObject;
	public GameObject parentTo;
    Animator elevator;
    int elevator_state = 1;
    float delay = 0f;
    // Start is called before the first frame update
    void Start()
    {
        elevator = GameObject.Find("elevator_A").GetComponent<Animator>();
        parentObject.transform.parent = parentTo.transform;
    }

    // Update is called once per frame
    void Update()
    {
        delay += Time.deltaTime;
        if (delay >= 5f) {
            delay = 5f;
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            if (elevator_state == 1 && delay >= 5f) {
                elevator.SetTrigger("elevator_down");
                delay = 0;
                elevator_state = -1;
            }
            if (elevator_state == -1 && delay >= 5f) {
                elevator.SetTrigger("elevator_up");
                delay = 0;
                elevator_state = 1;
            }
        }
    }
}
