using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeViewAspect : MonoBehaviour
{
    public bool isFirstPerson = true;
    Vector3 thirdPos;
    Vector3 firstPos;
    Vector3 thirdLightPos;
    Vector3 firstLightPos;
    Vector3 thirdAngle;
    Vector3 firstAngle;
    GameObject flashlight;
    // Start is called before the first frame update
    void Start()
    {
        thirdPos = new Vector3(0.15f, 1.561f, -0.6f);
        thirdAngle = new Vector3(20.4f, 0f, 0f);
        firstPos = new Vector3(0.15f, 1.296f, 0.303f);
        firstAngle = new Vector3(0f, 0f ,0f);
        thirdLightPos = new Vector3(-0.078f, -0.627f, 1.212f);
        firstLightPos = new Vector3(-0.08f, -0.194f, 0.37f);
        flashlight = GameObject.Find("Spot Light");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) {
            isFirstPerson = !isFirstPerson;
            if (isFirstPerson) {
                transform.localPosition = firstPos;
                transform.localRotation = Quaternion.Euler(firstAngle);
                flashlight.transform.localPosition = firstLightPos;
            } else {
                transform.localPosition = thirdPos;
                transform.localRotation = Quaternion.Euler(thirdAngle);
                flashlight.transform.localPosition = thirdLightPos;
            }
        }
    }
}
