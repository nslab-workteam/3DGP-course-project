using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightingTrigger : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Animator[] doors;
    [SerializeField] private Animator monster;
    [SerializeField] private AudioSource ambience;
    [SerializeField] private AudioClip clip;
    [Header("UI")]
    [SerializeField] private GameObject mBlood;
    [SerializeField] private GameObject monsterBlood;
    private AudioClip backup;
    // Start is called before the first frame update
    void Start()
    {
        backup = ambience.clip;
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
            ambience.clip = clip;
            ambience.Play();
            ambience.loop = true;
            mBlood.SetActive(true);
            monsterBlood.SetActive(true);
            StartCoroutine(DelayStartFighting());
        }
    }

    IEnumerator DelayStartFighting() {
        yield return new WaitForSeconds(3f);
        monster.SetTrigger("Start");
        Destroy(this);
    }
}
