using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRun : StateMachineBehaviour
{
    //Used https://www.youtube.com/watch?v=AD4JIXQDw0s For help here
    
    public Transform _player;
    public Rigidbody2D _rb;
    public float _speed = 1.5f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (_player.transform.position.x > _rb.transform.position.x){
            _rb.velocity = new Vector2(_speed, 0);
        }else{
            _rb.velocity = new Vector2(-_speed, 0); 
        }
        animator.GetComponent<Boss>().LookTowardsPlayer();
        if(Vector2.Distance(_player.position, _rb.position) <= 2){
            _rb.velocity = Vector2.zero;
            animator.SetTrigger("Attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

    
}
