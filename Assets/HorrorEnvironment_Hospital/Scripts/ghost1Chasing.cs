using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost1Chasing : MonoBehaviour
{
    public GameObject player;

    private int closeCount = 0;
    private bool closeToGhost = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 playerPos = player.transform.position;

        if (Vector3.Magnitude(pos - playerPos) <= 5.0f) {
            this.transform.LookAt(player.transform, Vector3.up);
            closeToGhost = true;
        }else if (closeToGhost) {
            closeToGhost = false;
            closeCount++;
        }

        if (closeCount >= 2) {
            GetComponent<Animator>().SetBool("chasing", true);
        }
    }
}
