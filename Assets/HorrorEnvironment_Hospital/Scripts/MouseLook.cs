using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensX = 400;
    public float sensY = 400;
    public Transform orientation;
    public GameObject player;
    float xRotation;
    float yRotation;
    public bool isStart = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 90f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStart) return;
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        if (mouseX != 0f) {
            Quaternion origRot = player.transform.rotation;
            player.transform.rotation = Quaternion.Euler(origRot.x, yRotation + 90f, 0);
        }

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation + 90f, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }
}
