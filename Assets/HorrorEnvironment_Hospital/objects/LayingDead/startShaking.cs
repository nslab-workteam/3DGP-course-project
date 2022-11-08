using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startShaking : MonoBehaviour
{
    private Animator[] temp;
    private Animator layingDeadAnimator;
    private bool count = false;
    private float timer, interval;
    private AudioSource scream;

    // Start is called before the first frame update
    void Start()
    {
        temp = GameObject.Find("Laying Seizure").GetComponents<Animator>();
        layingDeadAnimator = temp[0];
        timer = 0f;
        interval = 3f;
        scream = GameObject.Find("Laying Seizure").GetComponent<AudioSource>();
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
                if (hit.collider.name == "layingDeadCarpet")
                {
                    bool start = layingDeadAnimator.GetBool("layingDeadStart");
                    //Debug.Log(start);
                    if (!start)
                    {
                        //layingDeadAnimator.SetTrigger("layingDeadStart");
                        GetComponent<Cloth>().useGravity = false;
                        count = true;
                        //GetComponent<AudioSource>().Play();
                    }
                }
            }
        }

        if (count)
        {
            //Debug.Log("timer");
            //Debug.Log(timer);
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                layingDeadAnimator.SetTrigger("layingDeadStart");
                scream.Play();
                gameObject.SetActive(false);
                timer = 0f;
                count = false;
            }
        }
    }
}
