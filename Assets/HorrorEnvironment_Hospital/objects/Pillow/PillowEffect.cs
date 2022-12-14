using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowEffect : MonoBehaviour
{
    private Animator[] temp;
    private Animator scissor2Animator;
    private Animator gloveAnimator;
    public GameObject PillowCutEffect;
    public GameObject Scissor2;

    private int usingChar;
    public GameObject MainCam;

    // Start is called before the first frame update
    void Start()
    {
        PillowCutEffect.SetActive(false);
        Scissor2.SetActive(false);

        temp = GameObject.Find("Scissor2").GetComponentsInChildren<Animator>();
        scissor2Animator = temp[0];

        temp = GameObject.Find("Glove").GetComponentsInChildren<Animator>();
        gloveAnimator = temp[0];
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray;
        RaycastHit hit;

        usingChar = GameObject.Find("UIManager").GetComponent<gameMenu>().usingChar;
        MainCam = GameObject.Find("PLAYER/character" + usingChar + "/Main Camera");

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 3f))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.name == "Pillow")
                {
                    Scissor2.SetActive(true);
                    PillowCutEffect.SetActive(true);
                    scissor2Animator.SetTrigger("Scissor2Start");
                    //©µ¿ð
                    gloveAnimator.SetTrigger("GloveAnimated");
                }
            }
        }
    }
}
