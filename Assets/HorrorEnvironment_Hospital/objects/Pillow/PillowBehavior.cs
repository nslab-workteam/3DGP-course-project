using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowBehavior : MonoBehaviour
{
    [SerializeField] private GameObject qteController;
    [SerializeField] private GameObject scissors;
    [SerializeField] private GameObject scissors2;
    [SerializeField] private GameObject pillowCutEffect;
    GameObject player;
    Animator scissors2Animator;
    [SerializeField] Animator gloveAnimator;
    public bool hasCut = false;
    bool afterClick = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        scissors2Animator = scissors2.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 3f)) {
            if (Input.GetMouseButtonDown(0) && 
                hit.collider.name == "Pillow" &&
                !hasCut && 
                scissors.GetComponent<ScissorsBehavior>().hasPickedUp &&
                player.GetComponent<HoldingItem>().holdingObject == (int)ObjectToPick.scissors) {
                    qteController.GetComponent<QTEController>().StartQTE(15, "剪開枕頭");
                    afterClick = true;
            }
        }

        if (afterClick && !qteController.GetComponent<QTEController>().qteShow) {
            scissors2.SetActive(true);
            GetComponent<AudioSource>().PlayDelayed(0.5f);
            this.StartCoroutine(_delayedPillowCutEffect());
            scissors2Animator.SetTrigger("Scissor2Start");
            this.StartCoroutine(_delayedGloveAnimation());
        }
    }

    IEnumerator _delayedGloveAnimation(){
        yield return new WaitForSeconds(4f);
        scissors2.SetActive(false);
        gloveAnimator.SetTrigger("GloveAnimated");
        hasCut = true;
    }

    IEnumerator _delayedPillowCutEffect(){
        yield return new WaitForSeconds(1.5f);
        pillowCutEffect.SetActive(true);
    }
}
