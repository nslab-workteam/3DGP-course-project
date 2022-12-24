using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterRunStateBehaviour : StateMachineBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    private MonsterBehaviour monsterState;
    float timePassed = 0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GameObject.Find("Monster").GetComponent<NavMeshAgent>();
        monsterState = GameObject.Find("Monster").GetComponent<MonsterBehaviour>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(player.position);
        if (monsterState.stage == 1 && stateInfo.IsName("Run") || monsterState.stage == 2 && stateInfo.IsName("Running Crawl")) {
            timePassed += Time.deltaTime;
            if (timePassed >= Random.Range(5, 10)) {
                int prob = Random.Range(1, 100);
                if (prob <= 70) {
                    animator.SetTrigger("Attack");
                } else {
                    animator.SetTrigger("Scream");
                }
                timePassed = 0;
            }
        } else if (monsterState.stage == 2 && stateInfo.IsName("Run")){
            animator.SetTrigger("Stage2");
        }
        if (monsterState.stage == 3) {
            animator.SetTrigger("Died");
        }
        

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
