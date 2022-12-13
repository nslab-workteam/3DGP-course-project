using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatorButton : MonoBehaviour
{
    public string toFloor;
    Animator elevatorAnimator;
    // Start is called before the first frame update
    void Start()
    {
        elevatorAnimator = GameObject.Find("elevator_A").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 3f) && Input.GetMouseButton(0)) {
            if (!(hit.collider.name == this.gameObject.name)) return;
            if (elevatorAnimator.GetCurrentAnimatorStateInfo(0).IsName(toFloor)) return;

            elevatorAnimator.SetTrigger("to_" + toFloor);
        }
    }
}
