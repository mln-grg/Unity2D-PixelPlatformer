using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_king_run : StateMachineBehaviour
{
    public Transform player;
    Boss_King_Controller king_Controller;

     
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        king_Controller = animator.GetComponent<Boss_King_Controller>();
    }

     
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

     
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    
}
