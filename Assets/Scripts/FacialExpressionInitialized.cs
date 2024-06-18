using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacialExpressionInitialized : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("Face Key", 0);
    }
}
