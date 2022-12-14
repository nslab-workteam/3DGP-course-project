using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Image skill;
    private float timePassed = 0;
    private float cooldown = 20;
    // Start is called before the first frame update
    void Start()
    {
        timePassed = 20;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= cooldown) {
            skill.enabled = true;
            if (Input.GetKeyDown(KeyCode.E)) {
                player.GetComponentInChildren<Animator>().SetTrigger("Sprint");
                timePassed = 0;
                skill.enabled = false;
            }
        }
        
    }
}
