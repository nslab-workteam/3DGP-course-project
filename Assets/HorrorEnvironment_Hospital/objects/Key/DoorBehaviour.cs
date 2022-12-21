using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    GameObject player;
    [SerializeField] private GameObject key;
    public GameObject fireworkParticle;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 3f)) 
        {
            if (Input.GetMouseButtonDown(0) && 
                hit.collider.name == "Torus" && 
                key.GetComponent<HammerBehaviour>().isPickedUp &&
                player.GetComponent<HoldingItem>().holdingObject == (int)ObjectToPick.key) 
                {
                    //成功
                    fireworkParticle.GetComponent<ParticleSystem>().Play();
            }
        }
    }
}
