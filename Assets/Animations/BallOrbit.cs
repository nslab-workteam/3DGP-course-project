using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOrbit : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float degreesPerSecond = 20;

    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.forward, degreesPerSecond * Time.deltaTime);
    }
}
