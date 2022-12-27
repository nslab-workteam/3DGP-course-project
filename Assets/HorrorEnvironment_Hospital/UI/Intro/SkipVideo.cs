using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SkipVideo : MonoBehaviour
{
    [SerializeField] private PlayableDirector pd;
    float timePassed = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > 3f) {
            if (Input.GetMouseButtonDown(0)) {
                pd.time = 300.1f;
            }
        }
    }
}
