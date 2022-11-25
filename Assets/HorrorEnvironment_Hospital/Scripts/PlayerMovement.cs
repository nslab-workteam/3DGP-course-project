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

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    Animator player;

    Vector3 idleCamPos;
    Vector3 movingCamPos;
    bool isFirstPerson = true;

    GameObject mainCamera;
    float walkingParam = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        player = GetComponentInChildren<Animator>();
        idleCamPos = new Vector3(0.15f, 1.296f, 0.303f);
        movingCamPos = new Vector3(0.078f, 0.843f, 0.423f);
        mainCamera = GameObject.Find("Main Camera");
        moveSpeed = maxWalkSpeed;
    }

    void MyInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void MovePlayer() {
        moveDirection = orientation.right * verticalInput - orientation.forward * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f);
        player.SetFloat("Walk", Mathf.Lerp(player.GetFloat("Walk"), walkingParam, Time.deltaTime * 10));


        if (verticalInput == 0 && horizontalInput == 0) {
            // rb.velocity = Vector3.zero;
            RaycastHit hit;
            bool grounded = Physics.Raycast(transform.position, Vector3.down, out hit, 0.3f);
            player.SetBool("isWalk", false);
            walkingParam = 0.0f;
            Debug.Log(grounded);
            if (grounded)
                rb.drag = staticDrag;
            else
                rb.drag = 0.5f;
        }else if(horizontalInput < 0){
            player.SetBool("isWalk", true);
            walkingParam = -1.0f;
            rb.drag = 0.5f;
        } else {
            player.SetBool("isWalk", true);
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
        if (Input.GetKey(KeyCode.LeftControl)) {
            mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, movingCamPos, 20 * Time.deltaTime);
            player.SetBool("isCrouch", true);
        } else {
            mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, idleCamPos, 20 * Time.deltaTime);
            player.SetBool("isCrouch", false);
        }
    }

    void CheckSprint() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            moveSpeed = maxSprintSpeed;
            walkingParam = 2.0f;
        } else {
            moveSpeed = maxWalkSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckCrouch();
        CheckSprint();
        MyInput();
    }

    void FixedUpdate() {
        SpeedControl();
        MovePlayer();
    }

    // void OnCollisionEnter(Collider other) {
    //     if (other.gameObject.tag != "GROUND") {
    //         RaycastHit hit;
    //         bool grounded = Physics.Raycast(transform.position, Vector3.down, out hit);
    //         if (hit.collider.tag == "GROUND") {
    //             Vector3 ground_pos = hit.collider.transform.position;
    //             Vector3 ground_player_pos = transform.position;
    //             ground_player_pos.y = ground_pos.y;
    //             transform.position = Vector3.Lerp(transform.position, ground_player_pos, Time.deltaTime);
    //         }
    //     }
    // }
}
