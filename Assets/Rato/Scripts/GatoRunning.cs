using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatoRunning : StateMachineBehaviour
{
    private Transform target;
    private Rigidbody2D rb;
    [SerializeField] private float Speed;
    private Gato gato;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {    
        this.target = GameObject.FindGameObjectWithTag("Player").transform;
        this.rb = animator.GetComponent<Rigidbody2D>();
        gato = animator.GetComponent<Gato>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gato.LookAtPlayer();
        rb.position = Vector2.MoveTowards(rb.position, new Vector2(target.position.x, rb.position.y), this.Speed * Time.deltaTime);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
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
