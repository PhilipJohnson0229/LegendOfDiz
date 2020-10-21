using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimatorBool : StateMachineBehaviour
{

    public string targetBool;
    public string targetBoolB;
    public bool status;
    public bool statusB;

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(targetBool, status);
        if (targetBoolB == null)
        {
            return;
         
        }
        else 
        {
            animator.SetBool(targetBoolB, statusB);
        }
        
    }

}
