using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBehaviourScript : MonoBehaviour
{
    private GameObject watcher;

    public GameObject gravityManage;

    private Animator[] temp;
    private Animator doorAnimator;


    // Start is called before the first frame update
    void Start()
    {
        watcher = GameObject.Find("watcher1");
        watcher.SetActive(false);

        temp = GameObject.Find("CG/FLOOR_1st/TOILETS/doors_A").GetComponentsInChildren<Animator>();
        doorAnimator = temp[0];
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
                    gravityManage.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    gravityManage.GetComponent<Rigidbody>().useGravity = true;
                    StartCoroutine(watcherPopUp());

                    doorAnimator.SetTrigger("doorOpen");
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

    IEnumerator watcherPopUp()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<AudioSource>().PlayDelayed(0.5f);
        watcher.SetActive(true);

        //doorAnimator.SetTrigger("doorOpen");
    }
}
