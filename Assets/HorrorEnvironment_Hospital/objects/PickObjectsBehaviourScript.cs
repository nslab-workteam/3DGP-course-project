using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameObjectBehavior;

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
    private OutlineControl outlineManager;
    private GameObject PasswordSpotLight;
    private GameObject PasswordCamera;
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
        inGameUiManager = GameObject.Find("IngameUIManager");

        outlineManager = GameObject.Find("OutlineManager").GetComponent<OutlineControl>();

        PasswordSpotLight = GameObject.Find("Password Spot Light");
        PasswordSpotLight.SetActive(false);

        PasswordCamera = GameObject.Find("Password Camera");
        PasswordCamera.SetActive(false);
        
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
                // if (hit.collider.name == "PaperStack")
                // {
                //     inGameUiManager.GetComponent<IngameUI>().pickUp(ObjectToPick.records);
                //     inGameUiManager.GetComponent<IngameUI>().ShowHint("您已獲得病歷表");
                //     GameObject.Find("PaperStack").SetActive(false);
                // } 
                // if (hit.collider.name == "Scissor")
                // {
                //     inGameUiManager.GetComponent<IngameUI>().pickUp(ObjectToPick.scissors);
                //     GameObject.Find("Scissor").SetActive(false);
                // }
                if (hit.collider.name == "Pillow")
                {
                    Scissor2.SetActive(true);
                    this.StartCoroutine(_delayedPillowCutEffect());
                    scissor2Animator.SetTrigger("Scissor2Start");
                    this.StartCoroutine(_delayedGloveAnimation());

                    //GameObject.Find("IngameUIManager").GetComponent<IngameUI>().pickUp(ObjectToPick.pillow);
                    //GameObject.Find("Pillow").SetActive(false);
                }
                if (hit.collider.name == "Glove")
                {
                    inGameUiManager.GetComponent<IngameUI>().pickUp(ObjectToPick.glove);
                    GameObject.Find("Glove").SetActive(false);
                }
                if (hit.collider.name == "Magnify Glass")
                {
                    inGameUiManager.GetComponent<IngameUI>().pickUp(ObjectToPick.magnifier);
                    GameObject.Find("Magnify Glass").SetActive(false);
                }
                if (hit.collider.name == "suitcase")
                {
                    inGameUiManager.GetComponent<IngameUI>().inGameUIPages[4].SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    GameObject.Find("PLAYER").GetComponent<PlayerMovement>().enabled = false;
                    Camera.main.GetComponent<MouseLook>().isStart = false;
                    // inGameUiManager.GetComponent<IngameUI>().pickUp(ObjectToPick.pass_case);
                    // GameObject.Find("suitcase").SetActive(false);
                }
                if (hit.collider.name == "Recipe")
                {
                    inGameUiManager.GetComponent<IngameUI>().pickUp(ObjectToPick.formula);
                    GameObject.Find("Recipe").SetActive(false);
                }
                if (hit.collider.name == "SpecialLiquid")
                {
                    inGameUiManager.GetComponent<IngameUI>().pickUp(ObjectToPick.liquid);
                    GameObject.Find("SpecialLiquid").SetActive(false);
                }
                if (hit.collider.name == "Teddy")
                {
                    inGameUiManager.GetComponent<IngameUI>().pickUp(ObjectToPick.doll);
                    GameObject.Find("Teddy").SetActive(false);
                }
                else if (hit.collider.name == "SuitcasePassword")
                {
                    //打開燈
                    PasswordSpotLight.SetActive(true);
                    //切換相機
                    Camera.main.GetComponent<AudioListener>().enabled = false;
                    PasswordCamera.SetActive(true);
                    PasswordCamera.GetComponent<AudioListener>().enabled = true;
                    this.StartCoroutine(_RoomOutPassword());
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
    IEnumerator _RoomOutPassword(){
        yield return new WaitForSeconds(3f);
        PasswordCamera.GetComponent<AudioListener>().enabled = false;
        PasswordCamera.SetActive(false);
        Camera.main.GetComponent<AudioListener>().enabled = true;
    }
}
