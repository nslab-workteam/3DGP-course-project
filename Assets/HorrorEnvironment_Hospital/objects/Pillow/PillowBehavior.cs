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
        scissors2.SetActive(false);
        pillowCutEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCut) return;
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
            scissors2.GetComponent<AudioSource>().Play();
            this.StartCoroutine(_delayedPillowCutEffect());
            this.StartCoroutine(_delayedGloveAnimation());
            Debug.Log("cut");
            afterClick=false;
        }
    }

    IEnumerator _delayedGloveAnimation(){
        yield return new WaitForSeconds(4f);
        scissors2.SetActive(false);
        gloveAnimator.SetTrigger("GloveAnimated");
        hasCut = true;
    }

    IEnumerator _delayedPillowCutEffect(){
        yield return new WaitForSeconds(1f);
        scissors2Animator.SetTrigger("Scissor2Start");
        pillowCutEffect.SetActive(true);
        pillowCutEffect.GetComponentInChildren<ParticleSystem>().Play();
    }
}
