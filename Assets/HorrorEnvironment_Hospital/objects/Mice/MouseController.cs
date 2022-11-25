using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{

    public GameObject mouse;
    public GameObject player;

    private GameObject[] miceList;
    private bool startChasing = false;
    private bool startDestroy = false;
    private int[][] floor1;
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
        if (!startChasing) return;
        Vector3 playerPos = player.transform.position;
        foreach (GameObject m in miceList) {
            Vector3 diff = m.transform.position - playerPos;
            diff.y = 0;
            if (diff.magnitude <= 1f) {
                startDestroy = DestroyOnce(startDestroy);
                playerPos.y += 3f;
                m.transform.LookAt(new Vector3(playerPos.x, 100f, playerPos.z), Vector3.up);
            } else {
                m.transform.LookAt(player.transform, Vector3.up);
            }
            m.GetComponent<Rigidbody>().AddForce((playerPos - m.transform.position).normalized * 5f, ForceMode.Force);
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
}
