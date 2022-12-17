using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseController : MonoBehaviour
{

    public GameObject mouse;
    public GameObject player;

    private GameObject[] miceList;
    private bool startChasing = false;
    private bool startDestroy = false;
    private bool startPlay = false;
    // Start is called before the first frame update
    void Start()
    {
        miceList = new GameObject[50];
        for (int i=0; i<miceList.GetLength(0); i++) {
            miceList[i] = Instantiate(mouse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Chasing();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "PLAYER") {
            startChasing = true;
        }
    }

    void Chasing() {
        if (startChasing) {
            startPlay = PlayOnce(startPlay);
            for (int i=0; i<miceList.GetLength(0); i++) {
                miceList[i].transform.LookAt(player.transform.position);
                var nav = miceList[i].GetComponent<NavMeshAgent>();
                nav.SetDestination(player.transform.position);
                // Debug.Log("ramaining: "+nav.remainingDistance);
                if (nav.remainingDistance <= 1f) {
                    startDestroy = DestroyOnce(startDestroy);
                }
            }
        }
        
    }

    IEnumerator DestroyMouse() {
        yield return new WaitForSeconds(5);
        foreach(GameObject m in miceList) {
            Destroy(m);
        }
        startChasing = false;
        Destroy(this);
    }

    bool DestroyOnce(bool flag) {
        if (!flag) {
            StartCoroutine(DestroyMouse());
        }
        return true;
    }

    bool PlayOnce(bool flag) {
        if (!flag) {
            foreach(AudioSource a in GetComponents<AudioSource>()) {
                a.Play();
            }
        }
        return true;
    }
}
