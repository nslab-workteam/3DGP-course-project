using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObjectsBehaviourScript : MonoBehaviour
{
    private int usingChar;
    public GameObject MainCam;
    public GameObject inGameUiManager;

    private Animator[] temp;
    private Animator scissor2Animator;
    private Animator gloveAnimator;
    private GameObject PillowCutEffect;
    private GameObject Scissor2;

    // Start is called before the first frame update
    void Start()
    {
        Scissor2 = GameObject.Find("Scissor2");
        PillowCutEffect = GameObject.Find("PillowCutEffect");

        PillowCutEffect.SetActive(false);
        Scissor2.SetActive(false);

        temp = Scissor2.GetComponentsInChildren<Animator>();
        scissor2Animator = temp[0];

        temp = GameObject.Find("Glove").GetComponentsInChildren<Animator>();
        gloveAnimator = temp[0];
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
                    Scissor2.SetActive(true);
                    this.StartCoroutine(_delayedPillowCutEffect());
                    scissor2Animator.SetTrigger("Scissor2Start");
                    //����
                    this.StartCoroutine(_delayedGloveAnimation());

                    //GameObject.Find("IngameUIManager").GetComponent<IngameUI>().pickUp(ObjectToPick.pillow);
                    //GameObject.Find("Pillow").SetActive(false);
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
                    // inGameUiManager.GetComponent<IngameUI>().pickUp(ObjectToPick.pass_case);
                    // GameObject.Find("suitcase").SetActive(false);
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
    IEnumerator _delayedGloveAnimation(){
        yield return new WaitForSeconds(4f);
        Scissor2.SetActive(false);
        gloveAnimator.SetTrigger("GloveAnimated");
    }
    IEnumerator _delayedPillowCutEffect(){
        yield return new WaitForSeconds(1.5f);
        PillowCutEffect.SetActive(true);
    }
}
