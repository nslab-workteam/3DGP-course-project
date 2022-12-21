using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrightenCounter : MonoBehaviour
{
    [SerializeField] public int count = 0;
    [SerializeField] private Image progressbar;
    int total = 7;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float fillAmount = (float)count / (float)total;
        progressbar.fillAmount = fillAmount;
    }

    public int FrightenTime() {
        return count;
    }
}
