using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBehaviourScript : MonoBehaviour
{
    public GameObject gravityManage;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray;
        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.name == "curtain2") {
                    GetComponent<AudioSource>().PlayDelayed(1);
                    gravityManage.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    gravityManage.GetComponent<Rigidbody>().useGravity = true;


                }
            }
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "GROUND")
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<MeshCollider>(), GetComponentInChildren<MeshCollider>());
        }
    }
}
