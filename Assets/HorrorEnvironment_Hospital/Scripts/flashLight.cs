using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashLight : MonoBehaviour
{
    public Light flashLightEntity;
    public float minFlickerTime = 0.1f;
    public float maxFlickerTime = 0.3f;
    private float nextFlicker = 0.0f;
    private float timePassed = 0.0f;
    float minLight = 1f, maxLight = 1.2f;
    // Start is called before the first frame update
    void Start()
    {
        nextFlicker = Random.Range(minFlickerTime, maxFlickerTime);
    }

    // Update is called once per frame
    void Update()
    {
        // flickering in random time
        timePassed += Time.deltaTime;
        if (timePassed >= nextFlicker) {
            flashLightEntity.intensity = Random.Range(minLight, maxLight);
            timePassed = 0.0f;
            nextFlicker = Random.Range(minFlickerTime, maxFlickerTime);
        }
    }
}
