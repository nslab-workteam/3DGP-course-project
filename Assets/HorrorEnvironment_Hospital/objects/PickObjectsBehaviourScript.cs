using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObjectsBehaviourScript : MonoBehaviour
{
    private int usingChar;
    public GameObject MainCam;
    public GameObject inGameUiManager;

    // Start is called before the first frame update
    void Start()
    {
        usingChar = GameObject.Find("UIManager").GetComponent<gameMenu>().usingChar;
        MainCam = GameObject.Find("PLAYER/character" + usingChar + "/Main Camera");
        inGameUiManager = GameObject.Find("IngameUIManager");
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray;
        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 3f);
        if (Physics.Raycast(ray, out hit, 3f))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("PickObjectsBehaviourScript: " + hit.collider.name);
                if (hit.collider.name == "PaperStack")
                {
                    inGameUiManager.GetComponent<IngameUI>().pickUp(ObjectToPick.records);
                    inGameUiManager.GetComponent<IngameUI>().ShowHint("您已獲得病歷表");
                    // GameObject.Find("PaperStack").SetActive(false);
                } 
                else if (hit.collider.name == "Scissor")
                {
                    inGameUiManager.GetComponent<IngameUI>().pickUp(ObjectToPick.scissors);
                    GameObject.Find("Scissor").SetActive(false);
                }
                else if (hit.collider.name == "Pillow")
                {
                    inGameUiManager.GetComponent<IngameUI>().pickUp(ObjectToPick.pillow);
                    GameObject.Find("Pillow").SetActive(false);
                }
                else if (hit.collider.name == "Glove")
                {
                    inGameUiManager.GetComponent<IngameUI>().pickUp(ObjectToPick.glove);
                    GameObject.Find("Glove").SetActive(false);
                }
                else if (hit.collider.name == "Magnify Glass")
                {
                    inGameUiManager.GetComponent<IngameUI>().pickUp(ObjectToPick.magnifier);
                    GameObject.Find("Magnify Glass").SetActive(false);
                }
                else if (hit.collider.name == "suitcase")
                {
                    inGameUiManager.GetComponent<IngameUI>().pickUp(ObjectToPick.pass_case);
                    GameObject.Find("suitcase").SetActive(false);
                }
                else if (hit.collider.name == "Worn_Paper_Figures_FBX")
                {
                    inGameUiManager.GetComponent<IngameUI>().pickUp(ObjectToPick.formula);
                    GameObject.Find("Worn_Paper_Figures_FBX").SetActive(false);
                }
                else if (hit.collider.name == "SpecialLiquid")
                {
                    inGameUiManager.GetComponent<IngameUI>().pickUp(ObjectToPick.liquid);
                    GameObject.Find("SpecialLiquid").SetActive(false);
                }
                else if (hit.collider.name == "Teddy")
                {
                    inGameUiManager.GetComponent<IngameUI>().pickUp(ObjectToPick.doll);
                    GameObject.Find("Teddy").SetActive(false);
                }
                
            }
        }
    }
}
