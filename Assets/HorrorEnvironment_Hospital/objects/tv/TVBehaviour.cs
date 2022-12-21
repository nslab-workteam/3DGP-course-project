using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVBehaviour : MonoBehaviour
{
    public bool isPickedUp = false;
    [SerializeField] private GameObject hammer;
    [SerializeField] private GameObject TVSurface;
    GameObject player;
    public GameObject TVParticle;
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
                hit.collider.name == "TV" && 
                hammer.GetComponent<HammerBehaviour>().isPickedUp &&
                player.GetComponent<HoldingItem>().holdingObject == (int)ObjectToPick.hammer) 
                {
                    TVParticle.GetComponent<ParticleSystem>().Play();
                    TVSurface.SetActive(false);
            }
        }
    }
}
