using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabningHand : MonoBehaviour
{
    private GameObject hand1;
    private GameObject hand2;
    // Start is called before the first frame update
    void Start()
    {
        //hand1 = GameObject.Find("PLAYER/charater1/mixamorig:Hips/mixamorig:LeftUpLeg/mixamorig:LeftLeg/mixamorig:LeftFoot");
        hand2 = GameObject.Find("PLAYER/charater1/mixamorig:Hips/mixamorig:LeftUpLeg/mixamorig:LeftLeg/mixamorig:LeftFoot/mixamorig:LeftToeBase");
        //hand1.SetActive(false);
        hand2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider targetObj)
    {
        if (targetObj.gameObject.name == "HandsPlane")
        {
            Debug.Log("collide with HandsPlane");
            //hand1.SetActive(true);
            hand2.SetActive(true);
            GetComponent<PlayerMovement>().maxWalkSpeed = 0.5f;
            GetComponentInChildren<Animator>().SetBool("isSlow", true);
            GetComponentInChildren<Animator>().speed = 0.5f / 1.5f;
        }
    }

    void OnTriggerExit(Collider targetObj) {
        if (targetObj.gameObject.name == "HandsPlane")
        {
            //hand1.SetActive(false);
            hand2.SetActive(false);
            GetComponent<PlayerMovement>().maxWalkSpeed = 1.5f;
            GetComponentInChildren<Animator>().SetBool("isSlow", false);
            GetComponentInChildren<Animator>().speed = 1f;
        }
    }
}
