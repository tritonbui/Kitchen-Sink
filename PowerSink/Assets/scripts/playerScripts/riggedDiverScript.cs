using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class riggedDiverScript : MonoBehaviour
{
    public Animator animator;

    public void playRun()
    {
        animator.SetTrigger("startRun");
    }

    public void playIdle()
    {
        animator.SetTrigger("endRun");
    }
}
