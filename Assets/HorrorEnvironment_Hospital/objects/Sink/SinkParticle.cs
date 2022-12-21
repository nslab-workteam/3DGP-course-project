using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkParticle : MonoBehaviour
{
    public GameObject sink1Particle;
    public GameObject sink2Particle;
    public GameObject sink3Particle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 3f)) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(hit.collider.name == "sink1")
                {
                    sink1Particle.GetComponent<ParticleSystem>().Play();
                }
                else if(hit.collider.name == "sink2")
                {
                    sink2Particle.GetComponent<ParticleSystem>().Play();
                }
                else if(hit.collider.name == "sink3")
                {
                    sink3Particle.GetComponent<ParticleSystem>().Play();
                }
            }
        }
    }
}
