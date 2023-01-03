using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float maxSprintSpeed;
    public float maxWalkSpeed;
    private float moveSpeed;

    public float staticDrag;

    [Header("Ground Check")]
    public Transform orientation;
    public bool isStart = false;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    Animator[] player;
    Animator mPlayer;

    Vector3 idleCamPos, idleCamPos2;
    Vector3 movingCamPos, movingCamPos2;
    bool isFirstPerson = true;

    GameObject mainCamera;
    float walkingParam = 0.0f;

    gameMenu gMenu;
    float runningEnergyMax = 5f;
    [SerializeField] float runningEnergyUsed = 0f;
    [SerializeField] private AudioSource sighSfx;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        player = GetComponentsInChildren<Animator>();
        mPlayer = player[0];

        idleCamPos = new Vector3(0.15f, 1.296f, 0.303f);
        movingCamPos = new Vector3(0.078f, 0.843f, 0.423f);

        idleCamPos2 = new Vector3(-0.011f, 1.468f, 0.119f);
        movingCamPos2 = new Vector3(-0.011f,0.869f,0.442f);
        
        moveSpeed = maxWalkSpeed;
        gMenu = GameObject.Find("UIManager").GetComponent<gameMenu>();
    }

    void MyInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void MovePlayer() {
        moveDirection = orientation.right * verticalInput - orientation.forward * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f);
        mPlayer.SetFloat("Walk", Mathf.Lerp(mPlayer.GetFloat("Walk"), walkingParam, Time.deltaTime * 10));


        if (verticalInput == 0 && horizontalInput == 0) {
            // rb.velocity = Vector3.zero;
            RaycastHit hit;
            bool grounded = Physics.Raycast(transform.position, Vector3.down, out hit, 0.3f);
            mPlayer.SetBool("isWalk", false);
            walkingParam = 0.0f;
            if (grounded)
                rb.drag = staticDrag;
            else
                rb.drag = 0.5f;
        }else if(horizontalInput < 0){
            mPlayer.SetBool("isWalk", true);
            walkingParam = -1.0f;
            rb.drag = 0.5f;
        } else {
            mPlayer.SetBool("isWalk", true);
            walkingParam = 1.0f;
            rb.drag = 0.5f;
        }
    }

    void SpeedControl() {
        Vector3 flatVal = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (flatVal.magnitude > moveSpeed) {
            Vector3 limitedVal = flatVal.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVal.x, rb.velocity.y, limitedVal.z);
        }
        
        if (rb.velocity.y > 0) {
            rb.velocity = rb.velocity.normalized * moveSpeed;
        }
    }

    void CheckCrouch() {
        mainCamera = GameObject.Find("PLAYER/character" + gMenu.usingChar + "/Main Camera");
        if (gMenu.usingChar == 1) {
            if (Input.GetKey(KeyCode.LeftControl)) {
                mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, movingCamPos, 20 * Time.deltaTime);
                mPlayer.SetBool("isCrouch", true);
            } else {
                mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, idleCamPos, 20 * Time.deltaTime);
                mPlayer.SetBool("isCrouch", false);
            }
        } else {
            if (Input.GetKey(KeyCode.LeftControl)) {
                mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, movingCamPos2, 20 * Time.deltaTime);
                mPlayer.SetBool("isCrouch", true);
            } else {
                mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, idleCamPos2, 20 * Time.deltaTime);
                mPlayer.SetBool("isCrouch", false);
            }
        }
        
    }

    void CheckSprint() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            if (runningEnergyUsed <= runningEnergyMax) {
                moveSpeed = maxSprintSpeed;
                walkingParam = 2.0f;
                runningEnergyUsed += Time.deltaTime;
            } else {
                moveSpeed = maxWalkSpeed;
                sighSfx.Play();
            }
        } else {
            moveSpeed = maxWalkSpeed;
            runningEnergyUsed -= Time.deltaTime;
            runningEnergyUsed = Mathf.Clamp(runningEnergyUsed, 0, 5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStart) return;
        mPlayer = player[gMenu.usingChar-1];
        CheckCrouch();
        CheckSprint();
        MyInput();
    }

    void FixedUpdate() {
        SpeedControl();
        if (!isStart) return;
        MovePlayer();
    }
}
