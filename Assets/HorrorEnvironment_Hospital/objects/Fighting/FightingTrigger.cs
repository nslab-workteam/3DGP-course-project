using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingTrigger : MonoBehaviour
{
    [SerializeField] private Animator[] doors;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            Debug.Log("Start fight");
            foreach(Animator i in doors) {
                i.SetTrigger("CloseDoor");
            }
        }
    }
}
