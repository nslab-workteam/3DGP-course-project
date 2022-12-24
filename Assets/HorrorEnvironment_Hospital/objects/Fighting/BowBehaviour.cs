using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BowBehaviour : MonoBehaviour
{
    [SerializeField] private Image weapon;
    [SerializeField] private GameObject bow;
    [SerializeField] private PlayerAttack atk;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown(){
        weapon.enabled = true;
        bow.SetActive(false);
        atk.canAtk = true;
    }
}
