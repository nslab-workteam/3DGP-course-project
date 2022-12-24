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
        Vector3 target = transform.position + mRig.velocity.normalized * 3f;
        transform.LookAt(target);
        transform.Rotate(new Vector3(0, -90f, 0));
    }
}
