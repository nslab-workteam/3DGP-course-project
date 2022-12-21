using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MixPotionsBehaviour : MonoBehaviour
{
    public GameObject potionMixturePage;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject glove;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 3f)) {
            if (hit.collider.name == "Potions" && Input.GetMouseButtonDown(0) && 
                player.GetComponent<HoldingItem>().holdingObject == (int)ObjectToPick.glove) {
                potionMixturePage.SetActive(true);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>().isStart = true;
                Cursor.lockState = CursorLockMode.None;
                GameObject.Find("PLAYER").GetComponent<PlayerMovement>().enabled = false;
                Camera.main.GetComponent<MouseLook>().isStart = false;
                if (player.GetComponent<HoldingItem>().holdingObjectLeft == (int)ObjectToPick.formula) {
                    GameObject.FindWithTag("hint").GetComponent<Image>().enabled = true;
                } else {
                    GameObject.FindWithTag("hint").GetComponent<Image>().enabled = false;
                }
            }
        }
    }
}
