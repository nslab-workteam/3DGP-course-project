using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBehaviour : MonoBehaviour
{
    [SerializeField] private float blood = 100;
    [SerializeField] private Image bloodMask;
    [SerializeField] private PlayerAttack attkInfo;
    [SerializeField] private Animator[] doors;
    [SerializeField] private GameObject fightUI;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;
    
    [SerializeField] private AudioClip explode;
    [SerializeField] private FightingTrigger trigger;
    private PlayerMovement playerMove;
    public int stage = 1;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float fillAmount = blood / 100.0f;
        bloodMask.fillAmount = fillAmount;
        if (blood <= 0 && stage == 1) {
            blood = 100;
            stage = 2;
        }
        if (blood <= 0 && stage == 2) {
            stage = 3;
            foreach (Animator a in doors) {
                a.SetTrigger("OpenDoor");
            }
            fightUI.SetActive(false);
            audioSource.PlayOneShot(clip);
            trigger.AfterFighting();
            blood = 100;
            playerMove.maxWalkSpeed = 1.5f;
            playerMove.maxSprintSpeed = 3;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name.Contains("arrow")) {
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<ParticleSystem>().Play();
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            audioSource.PlayOneShot(explode);
            blood -= attkInfo.force;
            Destroy(other.gameObject, 1f);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.name.Contains("arrow")) {
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<ParticleSystem>().Play();
            audioSource.PlayOneShot(explode);
            blood -= attkInfo.force;
            Destroy(other.gameObject, 1);
        }
    }
}
