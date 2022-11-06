using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost1Chasing : MonoBehaviour
{
    public GameObject player;
    public GameObject ghost;
    public GameObject sound;

    private int closeCount = 0;
    private bool closeToGhost = false;
    private bool isSoundPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        ghost.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = ghost.transform.position;
        pos.y = 0;
        Vector3 playerPos = player.transform.position;
        playerPos.y = 0;

        if (Vector3.Magnitude(pos - playerPos) <= 8.0f)
        {
            ghost.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;

            if (Vector3.Magnitude(pos - playerPos) <= 5.0f)
            {
                if (!isSoundPlayed) {
                    if (!sound.activeSelf)
                        sound.SetActive(true);
                    isSoundPlayed = true;
                }
                ghost.transform.LookAt(player.transform, Vector3.up);
                closeToGhost = true;
            }
            else if (closeToGhost)
            {
                closeToGhost = false;
                closeCount++;
            }
        }
        else
        {
            ghost.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        }



        if (closeCount >= 2)
        {
            // GetComponent<Animator>().SetBool("chasing", true);
        }
    }
}
