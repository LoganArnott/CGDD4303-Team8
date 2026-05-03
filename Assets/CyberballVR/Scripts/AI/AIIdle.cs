using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdle : StateMachineBehaviour
{
   // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      animator.SetInteger("Idle Num", UnityEngine.Random.Range(0,6));
      // animator.SetBool("Idle Bool", false);
   }

   // OnStateExit is called before OnStateExit is called on any state inside this state machine
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      // animator.SetBool("Idle Bool", true);
   }
}
