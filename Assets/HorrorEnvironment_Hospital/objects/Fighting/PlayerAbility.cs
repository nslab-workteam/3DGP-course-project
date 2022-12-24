using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private float timePassed = 0;
    private float cooldown = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E) && timePassed >= cooldown) {
            player.GetComponentInChildren<Animator>().SetTrigger("Sprint");
            timePassed = 0;
        }
    }
}
