using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class ghost1Chasing : MonoBehaviour
{
    public GameObject player;
    public GameObject ghost;
    public GameObject sound;
    private PostProcessingBehaviour vfx;
    public float ghostWait = 5f;

    private int closeCount = 0;
    private bool closeToGhost = false;
    private bool isSoundPlayed = false;
    private bool isLookAt = false;
    private bool isChased = false;
    private float fogTime = 0.0f;
    private GameObject fog;
    private ParticleSystem fogParticle;
    private float ghostAccel = 3.0f;
    private bool triggerOnce = false;
    private float ghostWaitTime = 0f;
    private Vector3 ghostWaitPos;
    private GameObject sfx;
    private GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        ghost.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        fog = GameObject.Find("Fog particals");
        fogParticle = fog.GetComponentInChildren<ParticleSystem>();
        ghostWaitPos = new Vector3(-2.202f, 0, -0.306f);
        sfx = GameObject.Find("HA_ghost");
        vfx = GameObject.Find("Main Camera").GetComponent<PostProcessingBehaviour>();
        mainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = ghost.transform.position;
        pos.y = 0;
        Vector3 playerPos = player.transform.position;
        playerPos.y = 0;

        sfx.transform.position = ghost.transform.position;

        if (Vector3.Magnitude(pos - playerPos) <= 8.0f)
        {
            ghost.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;

            ColorGradingModel.Settings settings = vfx.profile.colorGrading.settings;
            settings.basic.contrast = Random.Range(0.3f, 1.7f);
            settings.channelMixer.red = new Vector3(Random.Range(0.2f, 0.8f), Random.Range(0.2f, 0.8f), Random.Range(0.2f, 0.8f));
            settings.channelMixer.green = new Vector3(Random.Range(0.2f, 0.8f), Random.Range(0.2f, 0.8f), Random.Range(0.2f, 0.8f));
            settings.channelMixer.blue = new Vector3(Random.Range(0.2f, 0.8f), Random.Range(0.2f, 0.8f), Random.Range(0.2f, 0.8f));
            vfx.profile.colorGrading.settings = settings;
            
            if (!isSoundPlayed) {
                sound.GetComponent<AudioSource>().Play();
                isSoundPlayed = true;
            }
            isLookAt = true;
            closeToGhost = true;
            
            if (!triggerOnce) {
                ghost.transform.position = ghostWaitPos;
                triggerOnce = true;
            } else if (closeToGhost) {
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

        if (closeCount >= 2) {
            // AI chasing

            
            Vector3 charaterFront = player.transform.position + player.transform.forward * 3f;
            Vector3 charaterBack = player.transform.position - player.transform.forward * 3f;
            fogParticle.Play();
            if (ghostWaitTime == 0) {
                // for stage 1
                // first, go to charater's front
                ghost.transform.position = Vector3.Lerp(ghost.transform.position, charaterFront, Time.deltaTime * ghostAccel);
                ghostAccel += 1f;
                // play sound
                sfx.GetComponent<AudioSource>().Play();
            }

            if (ghostWaitTime >= ghostWait) {
                // for stage 2
                // wait 0.5 second
                // penerate to charater's back
                ghost.transform.position = Vector3.Lerp(ghost.transform.position, charaterBack, Time.deltaTime * ghostAccel);
                ghostAccel += 1f;
                
            }

            if (ghostWaitTime < ghostWait) {
                // if almost got to charater's front
                // calculate for 0.5 second
                ghostWaitTime += Time.deltaTime;
                ghostAccel = 0f;
                // freeze movement
                player.GetComponent<PlayerMovement>().enabled = false;
                mainCamera.GetComponent<MouseLook>().isStart = false;
                player.transform.LookAt(ghost.transform.position, Vector3.up);
                Debug.Log("Freezing..., wait time =" + ghostWaitTime);
            }

            if ((ghost.transform.position - charaterBack).magnitude <= 0.5f) {
                // finally
                isChased = true;
            }
            if (isChased) {
                player.GetComponent<PlayerMovement>().enabled = true;
                mainCamera.GetComponent<MouseLook>().isStart = true;
                fog.transform.position = player.transform.position + player.transform.forward * 2f;
                fogParticle.Play();
                fogTime += Time.deltaTime;
                ColorGradingModel.Settings settings = vfx.profile.colorGrading.settings;
                settings.basic.contrast = 1.07f;
                settings.channelMixer.red = new Vector3(1f, 0.34f, -0.22f);
                settings.channelMixer.green = new Vector3(0f, 1f, 0f);
                settings.channelMixer.blue = new Vector3(0f, 0f, 1f);
                vfx.profile.colorGrading.settings = settings;
            }
            if (fogTime >= 5) {
                fogParticle.Stop();
                ghost.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
                Destroy(this);
            }
        }
    }
}
