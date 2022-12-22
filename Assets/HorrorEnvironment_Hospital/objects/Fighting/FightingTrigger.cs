using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingTrigger : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Animator[] doors;
    [SerializeField] private Animator monster;
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
            player.GetComponent<PlayerMovement>().maxWalkSpeed = 3;
            player.GetComponent<PlayerMovement>().maxSprintSpeed = 5;
            StartCoroutine(DelayStartFighting());
        }
    }

    IEnumerator DelayStartFighting() {
        yield return new WaitForSeconds(5f);
        monster.SetTrigger("Start");
    }
}
