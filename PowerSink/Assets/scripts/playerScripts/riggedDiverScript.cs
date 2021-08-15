using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class riggedDiverScript : MonoBehaviour
{
    public Animator animator;
    public bool isRunning;
    private Rigidbody _rb;
    private float x;
    private float y; 

    public void MyInput(InputAction.CallbackContext context)
    {
        if (!GameManager._instance.isPaused)
        {
            x = context.ReadValue<Vector2>().x;
            y = context.ReadValue<Vector2>().y;
        }
        else
        {
            x = 0f;
            y = 0f;
        }
    }
    
    //the following is a series of animation state management functions

    public void isFalling()
    {
        animator.SetBool("isFalling", true);
    }

    public void isNotFalling()
    {
        animator.SetBool("isFalling", false);
    }

    public void isGrounded()
    {
        animator.SetBool("isGrounded", true);
    }

    public void isNotGrounded()
    {
        animator.SetBool("isGrounded", false);
    }

    public void isJumping()
    {
        animator.SetBool("isJumping", true);
        Invoke("isNotJumping", 0.1f);
    }

    public void isNotJumping()
    {
        animator.SetBool("isJumping", false);
    }

    public void hasPowerOrb()
    {
        animator.SetBool("hasOrb", true);
    }

    public void hasNothing()
    {
        animator.SetBool("hasOrb", false);
    }

    public void FixedUpdate()
    {
        InputCheck();
    }

    public void InputCheck()
    {
        if (x != 0 || y != 0)
        {
            isRunning = true;
            animator.SetBool("isRunning", true);
        }
        else
        {
            isRunning = false;
            animator.SetBool("isRunning", false);
        }
    }
}
