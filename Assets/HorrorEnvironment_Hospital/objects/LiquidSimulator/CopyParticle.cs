using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyParticle : MonoBehaviour
{
    [SerializeField] private Transform beaker;
    public GameObject particle;
    Vector3 original_pos;
    // Start is called before the first frame update
    void Start()
    {
        original_pos = particle.transform.position;
        particle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMoreLiquid(Color color) {
        GameObject _particle = Instantiate(particle, original_pos, Quaternion.identity);
        _particle.SetActive(true);
        _particle.GetComponent<SpriteRenderer>().enabled = true;
        _particle.GetComponent<SpriteRenderer>().color = color;
        for(int i=0; i<32; i++) {
            GameObject p = Instantiate(_particle,
                new Vector3(original_pos.x, original_pos.y + 0.02f * i, original_pos.z),
                Quaternion.identity);
            p.transform.parent = beaker;
        }
    }
}
