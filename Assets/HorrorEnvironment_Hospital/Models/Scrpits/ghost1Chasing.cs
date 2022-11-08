using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost1Chasing : MonoBehaviour
{
    public GameObject player;
    public GameObject ghost;
    public GameObject sound;
    public float animationMaxSpeed = 5f;

    private int closeCount = 0;
    private bool closeToGhost = false;
    private bool isSoundPlayed = false;
    private float animationSpeed = 0f;
    private bool isLookAt = false;
    private float fogTime = 0.0f;
    private GameObject fog;
    private ParticleSystem fogParticle;
    private bool isChased = false;
    private float ghostAccel = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        ghost.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        fog = GameObject.Find("Fog particals");
        fogParticle = fog.GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = ghost.transform.position;
        pos.y = 0;
        Vector3 playerPos = player.transform.position;
        playerPos.y = 0;

        if (Vector3.Magnitude(pos - playerPos) <= 8.0f)
        {
            ghost.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
            
            if (Vector3.Magnitude(pos - playerPos) <= 5.0f)
            {
                if (!isSoundPlayed) {
                    if (!sound.activeSelf)
                        sound.SetActive(true);
                    isSoundPlayed = true;
                }
                isLookAt = true;
                closeToGhost = true;
            }
            else if (closeToGhost)
            {
                closeToGhost = false;
                closeCount++;
            }

            if (isLookAt) {
                Vector3 lookAtPos = player.transform.position;
                lookAtPos.y = 0;
                ghost.transform.LookAt(lookAtPos, Vector3.up);
            }
        }
        else
        {
            ghost.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        }



        if (closeCount >= 2)
        {
            
            Animator ghostAnimator = ghost.GetComponent<Animator>();
            ghostAnimator.SetBool("chasing", true);
            animationSpeed = Mathf.Lerp(animationSpeed, animationMaxSpeed, Time.deltaTime * 3f);
            ghostAnimator.SetFloat("animationSpeed", animationSpeed);

            // AI chasing
            pos.y = 0;
            playerPos.y = 0;
            if (!isChased) {
                ghost.transform.position = Vector3.Lerp(pos, playerPos, Time.deltaTime * ghostAccel);
                ghostAccel += 3.0f;
            }
            fogParticle.transform.position = ghost.transform.position;

            if (!fogParticle.isPlaying) {
                fogParticle.Play();
                Debug.Log("Play vfx");
            }

            if ((pos - playerPos).magnitude <= 0.1f) {
                isChased = true;
            }

            if (isChased) {
                ghost.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
                ghost.GetComponent<CapsuleCollider>().enabled = false;
                fogTime += Time.deltaTime;
                if (fogTime >= 5) {
                    fogParticle.Stop();
                    Debug.Log("Destroy");
                    Destroy(this);
                }
            }
        }
    }
}
