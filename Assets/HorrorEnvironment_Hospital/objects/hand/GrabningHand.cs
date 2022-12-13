using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabningHand : MonoBehaviour
{
    public GameObject Char1Hand;
    public GameObject Char2Hand;
  
    // Start is called before the first frame update
    void Start()
    {
        //Char1Hand = GameObject.Find("PLAYER/charater1/mixamorig:Hips/mixamorig:LeftUpLeg/mixamorig:LeftLeg/mixamorig:LeftFoot/mixamorig:LeftToeBase/Grabbing Hand");
        //Char2Hand = GameObject.Find("PLAYER/charater2/mixamorig:Hips/mixamorig:LeftUpLeg/mixamorig:LeftLeg/mixamorig:LeftFoot/mixamorig:LeftToeBase/Grabbing Hand");
        Char1Hand.SetActive(false);
        Char2Hand.SetActive(false);
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
            //Debug.Log(GameObject.Find("character1"));
            if (GameObject.Find("character1") != null)
            {
                Char1Hand.SetActive(true);
            }
            else 
            {
                Char2Hand.SetActive(true);
            }

            GetComponent<PlayerMovement>().maxWalkSpeed = 0.5f;
            GetComponent<PlayerMovement>().maxSprintSpeed = 1.5f;
            GetComponentInChildren<Animator>().SetBool("isSlow", true);
            GetComponentInChildren<Animator>().speed = 0.5f / 1.5f;
        }
    }

    void OnTriggerExit(Collider targetObj) {
        if (targetObj.gameObject.name == "HandsPlane")
        {
            if (GameObject.Find("character1") != null)
            {
                Char1Hand.SetActive(false);
            }
            else
            {
                Char2Hand.SetActive(false);
            }

            GetComponent<PlayerMovement>().maxWalkSpeed = 1.5f;
            GetComponent<PlayerMovement>().maxSprintSpeed = 3f;
            GetComponentInChildren<Animator>().SetBool("isSlow", false);
            GetComponentInChildren<Animator>().speed = 1f;
        }
    }
}
