using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionInitialized : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("Motion Key", 0);
    }
}
