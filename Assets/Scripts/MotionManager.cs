using UnityEditor;
using UnityEngine;

public class MotionManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    
    public void setFacialExpression(int i)
    {
        animator.SetInteger("Motion Key", i);
    }
}