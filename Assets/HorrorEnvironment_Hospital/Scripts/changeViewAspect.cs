using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeViewAspect : MonoBehaviour
{
    public bool isFirstPerson = true;
    Vector3 thirdPos;
    Vector3 firstPos;
    Vector3 thirdAngle;
    Vector3 firstAngle;
    // Start is called before the first frame update
    void Start()
    {
        thirdPos = new Vector3(0.15f, 1.561f, -0.6f);
        thirdAngle = new Vector3(20.4f, 0f, 0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
