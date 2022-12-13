using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatorFloor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnColliderEnter(Collision other) {
        if (other.gameObject.tag != "Player") {
            Physics.IgnoreCollision(other.collider, GetComponent<BoxCollider>(), true);
        }
    }
}
