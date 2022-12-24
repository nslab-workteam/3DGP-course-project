using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintStateBehaviour : StateMachineBehaviour
{
    private CapsuleCollider playerCollider;
    private GameObject player;
    private CapsuleCollider monsterCollider;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerCollider = GameObject.FindWithTag("Player").GetComponent<CapsuleCollider>();
        monsterCollider = GameObject.Find("Monster").GetComponent<CapsuleCollider>();
        player = GameObject.FindWithTag("Player");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Physics.IgnoreCollision(playerCollider, monsterCollider, true);
        player.GetComponent<PlayerMovement>().staticDrag = 0;
        player.GetComponent<Rigidbody>().AddForce(player.transform.TransformDirection(Vector3.forward).normalized * 50f);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Physics.IgnoreCollision(playerCollider, monsterCollider, false);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<PlayerMovement>().staticDrag = 100;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
