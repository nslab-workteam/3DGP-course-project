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
        hand1 = GameObject.Find("PLAYER/charater1/mixamorig:Hips/mixamorig:LeftUpLeg/mixamorig:LeftFoot");
        hand2 = GameObject.Find("PLAYER/charater1/mixamorig:Hips/mixamorig:LeftUpLeg/mixamorig:LeftFoot/mixamorig:LeftToeBase");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision targetObj)
    {
        if (targetObj.gameObject.name == "PLAYER" && close == false)
        {
            Debug.Log("collide with floor");

            close = true;
        }

        if (close)
        {
            door.transform.rotation = Quaternion.Euler(0.0f, 90f, 0.0f);
            //door.transform.Rotate(0f, -63f, 0f);
            GetComponent<AudioSource>().Play();
        }
    }
}
