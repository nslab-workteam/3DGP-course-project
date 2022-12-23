using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class AnnieBehaviour : MonoBehaviour, GameMechanism
{
    public GameObject player;
    public GameObject ghost;
    public GameObject myCamera;
    public GameObject fog;
    public AudioClip audio1;
    public AudioClip audio2;
    public bool activated = false;
    [SerializeField] private FrightenCounter fricnt;

    private AnnieState state = AnnieState.Idle;
    private PostProcessingBehaviour vfx;
    private AudioSource sound;
    private Vector3 ghostWaitPos;
    private float ghostAccel = 3.0f;
    bool soundFlag1 = false;
    bool soundFlag2 = false;
    bool fogFlag = false;
    bool coroutineFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        vfx = myCamera.GetComponent<PostProcessingBehaviour>();
        
        ghostWaitPos = new Vector3(-2.202f, 0, -0.306f);

        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        myCamera = GameObject.FindGameObjectWithTag("MainCamera");

        sound.transform.position = ghost.transform.position;

        switch(state) {
            case AnnieState.Idle:
                IdleState();
                break;
            case AnnieState.ShowUp:
                ShowUpState();
                break;
            case AnnieState.MoveToPlayersFront:
                MoveToPlayersFrontState();
                break;
            case AnnieState.MoveToPlayersBack:
                MoveToPlayersBackState();
                break;
            case AnnieState.Chased:
                ChasedState();
                break;
            case AnnieState.End:
                break;
        }
    }

    void IdleState() {

        // behaviour
        ghost.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;

        // transition
        if ((player.transform.position - ghost.transform.position).magnitude <= 8.0f) {
            state = AnnieState.ShowUp;
        }
    }

    void ShowUpState() {

        // behaviour
        ghost.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        ghost.transform.localPosition = ghostWaitPos;
        Vector3 lookAtPos = player.transform.position;
        lookAtPos.y = 0;
        ghost.transform.LookAt(lookAtPos, Vector3.up);
        soundFlag1 = PlaySound1(soundFlag1);

        // transition
        if ((player.transform.position - ghost.transform.position).magnitude <= Random.Range(4.0f, 6.0f)) {
            state = AnnieState.MoveToPlayersFront;
        }
    }

    void MoveToPlayersFrontState() {
        
        //behaviour
        Vector3 charaterFront = player.transform.position + player.transform.forward * 3f;
        ghost.transform.position = Vector3.Lerp(ghost.transform.position, charaterFront, Time.deltaTime * ghostAccel);
        ghostAccel += 1f;
        Vector3 lookAtPos = player.transform.position;
        lookAtPos.y = 0;
        ghost.transform.LookAt(lookAtPos, Vector3.up);
        player.GetComponent<PlayerMovement>().enabled = false;
        myCamera.GetComponent<MouseLook>().isStart = false;
        player.transform.LookAt(ghost.transform.position, Vector3.up);
        player.transform.rotation = Quaternion.Euler(0f, 90f, 0f);

        // transition
        coroutineFlag = CoroutineStart(coroutineFlag);
    }

    void MoveToPlayersBackState() {

        // behaviour
        Vector3 charaterBack = player.transform.position - player.transform.forward * 3f;
        soundFlag2 = PlaySound2(soundFlag2);
        ghost.transform.position = Vector3.Lerp(ghost.transform.position, charaterBack, Time.deltaTime * ghostAccel);
        ghostAccel += 1f;

        // transition
        if ((ghost.transform.position - charaterBack).magnitude <= 0.5f) {
            // fog.transform.position = ghost.transform.position;
            fogFlag = FogOn(fogFlag);
            state = AnnieState.Chased;
        }
    }

    void ChasedState() {

        // behaviour
        player.GetComponent<PlayerMovement>().enabled = true;
        myCamera.GetComponent<MouseLook>().isStart = true;
        ghost.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        // this.StartCoroutine(_FogOff());
        fricnt.count++;

        // transition
        state = AnnieState.End;

    }

    bool PlaySound1(bool flag) {
        if (!flag) {
            sound.PlayOneShot(audio1);
        }
        return true;
    }

    bool PlaySound2(bool flag) {
        if (!flag) {
            sound.PlayOneShot(audio2);
        }
        return true;
    }

    bool FogOn(bool flag) {
        if (!flag) {
            fog.GetComponentInChildren<ParticleSystem>().Play();
        }
        return true;
    }

    bool CoroutineStart(bool flag) {
        if (!flag) {
            this.StartCoroutine(_TransitGoBackState());
        }
        return true;
    }

    IEnumerator _TransitGoBackState() {
        yield return new WaitForSeconds(3f);
        state = AnnieState.MoveToPlayersBack;
        Debug.Log("Transition to Back state");
        ghostAccel = 0f;
    }

    IEnumerator _FogOff() {
        yield return new WaitForSeconds(5f);
        fog.GetComponentInChildren<ParticleSystem>().Stop();
        // Destroy(this);
        activated = true;
    }

    public void Skip() {
        GameObject.Find("Annie Ghost").SetActive(false);
    }

    public bool isActivated()
    {
        return activated;
    }
}
