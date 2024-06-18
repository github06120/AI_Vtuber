using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacialExpressionManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    public void setFacialExpression(int i)
    {
        animator.SetInteger("Face Key", i);
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.A))
        {
            setFacialExpression(1);
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            setFacialExpression(2);
        }else if(Input.GetKeyDown(KeyCode.D))
        {
            setFacialExpression(3);
        }
    }
}
