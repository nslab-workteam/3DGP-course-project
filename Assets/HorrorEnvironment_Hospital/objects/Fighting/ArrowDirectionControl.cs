using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDirectionControl : MonoBehaviour
{
    private Rigidbody mRig;
    public bool destroyAfterDelay = false;
    // Start is called before the first frame update
    void Start()
    {
        mRig = GetComponent<Rigidbody>();
        if (destroyAfterDelay) {
            Destroy(this.gameObject, 10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(mRig.velocity);
        transform.Rotate(0, 90, 0);
    }

    private void OnCollisionEnter(Collision other) {
        Physics.IgnoreCollision(GetComponent<CapsuleCollider>(), other.collider);
    }
}
