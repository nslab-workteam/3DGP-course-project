using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldingItem : MonoBehaviour
{
    [SerializeField] private GameObject HoldObject;
    public int holdingObject = -1;
    [SerializeField] private Sprite[] imageList;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (holdingObject != -1){
            HoldObject.GetComponent<Image>().enabled = true;
            HoldObject.GetComponent<Image>().sprite = imageList[(int)holdingObject];
        }else{
            HoldObject.GetComponent<Image>().enabled = false;
        }
    }
}
