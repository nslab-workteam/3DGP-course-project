using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAttkStateBehaviour : StateMachineBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attkSound;
    [SerializeField] private PlayerHurt hurt;
    private Transform player;
    private Transform monster;
    [SerializeField] private float attkRange = 3f;
    private float timePassed = 0f;
    private bool hurted = false;
    private bool triggeredFlg = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = GameObject.Find("Monster").GetComponent<NavMeshAgent>();
        audioSource = GameObject.Find("Monster").GetComponent<AudioSource>();
        monster = GameObject.Find("Monster").transform;
        audioSource.PlayOneShot(attkSound);
        player = GameObject.FindWithTag("Player").transform;
        hurt = GameObject.Find("AttackSystem").GetComponent<PlayerHurt>();
        hurted = false;
        triggeredFlg = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(monster.transform.position);
        Debug.DrawRay(monster.position, monster.transform.TransformDirection(new Vector3(1, 0, 1)).normalized * attkRange);
        Debug.DrawRay(monster.position, monster.transform.TransformDirection(new Vector3(-1, 0, 1)).normalized * attkRange);
        if (IsPointInTriangle(player.transform.position,
                              monster.position + monster.transform.TransformDirection(new Vector3(1, 0, 1)).normalized * attkRange,
                              monster.position, 
                              monster.position + monster.transform.TransformDirection(new Vector3(-1, 0, 1)).normalized * attkRange))
        {
            Debug.Log("hurt player");
            hurted = true;
        }
        if (hurted && !triggeredFlg) {
            timePassed += Time.deltaTime;
            if (timePassed > 1f) {
                hurt.DecreaseHealth();
                timePassed = 0f;
                hurted = false;
                triggeredFlg = true;
            }
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

    bool IsPointInTriangle(Vector3 p, Vector3 a, Vector3 b, Vector3 c) {
        Vector3 pa = p - a;
        Vector3 pb = p - b;
        Vector3 pc = p - c;
        float cross1 = Cross2D(pa, pb);
        float cross2 = Cross2D(pb, pc);
        float cross3 = Cross2D(pc, pa);
        // Debug.Log(cross1 + ", " + cross2 + ", " + cross3);
        if ((cross1 > 0 && cross2 > 0 && cross3 > 0) || (cross1 < 0 && cross2 < 0 && cross3 < 0)) {
            return true;
        }
        return false;
    }

    float Cross2D(Vector3 a, Vector3 b) {
        return a.z * b.x - a.x * b.z;
    }
}
