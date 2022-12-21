using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClose : MonoBehaviour, GameMechanism
{
    [SerializeField] private FrightenCounter fricnt;
    Animator doorAnimator;
    bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        doorAnimator = GameObject.Find("doors_A_can_close").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player" && !activated) {
            doorAnimator.SetTrigger("doorClose");
            GetComponent<AudioSource>().Play();
            activated = true;
            fricnt.count++;
        }
    }

    public bool isActivated()
    {
        return activated;
    }

    public void Skip()
    {
        doorAnimator.SetTrigger("doorClose");
    }
}
