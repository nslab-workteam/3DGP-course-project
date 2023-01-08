using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorOnB1 : StateMachineBehaviour
{
    [SerializeField] private Texture2D baseFloor;
    [SerializeField] private MeshRenderer floor;
    private BoxCollider door1F, door2F, doorB1;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       floor = GameObject.FindWithTag("elevator").GetComponent<MeshRenderer>();
       door1F = GameObject.Find("Door1F").GetComponent<BoxCollider>();
       door2F = GameObject.Find("Door2F").GetComponent<BoxCollider>();
       doorB1 = GameObject.Find("DoorB1").GetComponent<BoxCollider>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       floor.material.mainTexture = baseFloor;
       door1F.enabled = true;
       door2F.enabled = true;
       doorB1.enabled = false;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
