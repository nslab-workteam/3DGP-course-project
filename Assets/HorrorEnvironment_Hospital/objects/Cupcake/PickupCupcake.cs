using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickupCupcake : MonoBehaviour
{
    public int foundCupcakeNum = 0;
    public GameObject[] cupcake;
    public TextMeshProUGUI uiText = null;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         Ray ray;
        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 3f);
        if (Physics.Raycast(ray, out hit, 3f, LayerMask.NameToLayer("cupcake")))
        {
            if (Input.GetMouseButtonDown(0))
            {
                for(int i=1;i<11;i++){
                    if(hit.collider.name == "muffin" + i.ToString())
                    {
                        cupcake[i-1].SetActive(false);
                        foundCupcakeNum++;
                        uiText.text = foundCupcakeNum.ToString();
                    }
                }
            }
        }
    }
}
