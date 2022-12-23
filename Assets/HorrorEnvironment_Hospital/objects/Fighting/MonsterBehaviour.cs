using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBehaviour : MonoBehaviour
{
    [SerializeField] private int blood = 100;
    [SerializeField] private Image bloodMask;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float fillAmount = (float)blood / 100.0f;
        bloodMask.fillAmount = fillAmount;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.name == "arrow") {
            blood -= 2;
        }
    }
}
