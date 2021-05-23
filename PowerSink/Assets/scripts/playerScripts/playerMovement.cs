using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public Transform cam;
    private Rigidbody _rb;
    public playerInteraction playerInteraction;
    public riggedDiverScript _rds;
        
    private float desiredX;
    private float x;
    private float y;
    
    public bool isGrounded {get; protected set;}
    public LayerMask whatIsGround;
    private bool cancellingGrounded;
    private bool isLevelLoading = false;

    [Header("Movement Properties")]
    public float moveSpeed;
    public float maxSpeed;
    public float airMultiplierA;
    public float airMultiplierB;
    public Vector2 maxAir;
    public float rotationSpeed;
    [Header("Jump Properties")]
    public float jumpForce;
    private float currentJumpForce;
    [Min(0)]
    public float jumpRedux;
    private bool isHoldingJump;



    void Awake()
    {
        this._rb = this.GetComponent<Rigidbody>();
        isLevelLoading = false;
    }

    private void Update()
    {
        Look();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public void MyInput(InputAction.CallbackContext context)
    {
        if (!GameManager._instance.isPaused)
        {
            x = context.ReadValue<Vector2>().x;
            y = context.ReadValue<Vector2>().y;
        }
    }

    public void Look()
    {
        Vector3 input_dir = new Vector3(x, 0f, y).normalized;
        Vector3 targ_dir = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;
        if (x != 0 || y != 0)
        {
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.LookRotation(Quaternion.LookRotation(targ_dir, Vector3.up) * input_dir, Vector3.up), Time.deltaTime * rotationSpeed);
        }
        orientation.transform.rotation = Quaternion.LookRotation(targ_dir, Vector3.up);
    }

    public void Movement()
    {
        FallingCheck();
        JumpUpdate();

        //Target Direction Angle
        Quaternion targetDir = Quaternion.LookRotation(Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized, Vector3.up);

        //finds target velocity
        Vector3 targetVel = targetDir * new Vector3(x, 0f, y).normalized * moveSpeed;

        //applies forces to player
        if(isGrounded)
        {
            targetVel += Vector3.up * _rb.velocity.y;
            _rb.AddForce(targetVel - _rb.velocity, ForceMode.Impulse);
        }
        else
        {
            if((FindVelRelativeToLook().x < maxAir.x && FindVelRelativeToLook().y < maxAir.y) || (FindVelRelativeToLook().x < -maxAir.x && FindVelRelativeToLook().y < -maxAir.y))
            {
                _rb.AddForce(targetVel * airMultiplierA);
            }
            else
            {
                _rb.AddForce(targetVel * airMultiplierB);
            }
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            isHoldingJump = true;
            _rds.isJumping();
            currentJumpForce = jumpForce;
        }

        if (context.canceled)
        {
            isHoldingJump = false;
        }
    }

    public void JumpUpdate()
    {
        if (isHoldingJump)
        {
            _rb.AddForce(Vector3.up * currentJumpForce);
            currentJumpForce -= jumpRedux;
            
            if(currentJumpForce < 0)
            {
                currentJumpForce = 0;
            }
        }
    }

    public Vector2 FindVelRelativeToLook() 
    {
        float lookAngle = orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(_rb.velocity.x, _rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitude = _rb.velocity.magnitude;
        float yMag = magnitude * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitude * Mathf.Cos(v * Mathf.Deg2Rad);
        
        return new Vector2(xMag, yMag);
    }

    //Ground Collision detection
    private void OnTriggerStay(Collider col)
    {
        if (LayerMask.NameToLayer("Ground") == col.gameObject.layer)
        {
            isGrounded = true;
            _rds.isGrounded();
            playerInteraction.isGrounded = true;
            cancellingGrounded = false;
            CancelInvoke(nameof(StopGrounded));
        }

        float delay = 3f;
        if (!cancellingGrounded) 
        {
            cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * delay);
        }
    }

    private void StopGrounded() 
    {
        isGrounded = false;
        _rds.isNotGrounded();
        playerInteraction.isGrounded = false;
    }

    public void FallingCheck()
    {
        if (_rb.velocity.y < -1f && !isGrounded && Time.timeSinceLevelLoad > 5f)
        {
            _rds.isFalling();
        }
        else
        {
            _rds.isNotFalling();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "endBox" && !isLevelLoading)
        {
            if (SceneManager.GetActiveScene().name == "levelOne")
            {
                GameManager._instance.LoadLevelTwo();
                isLevelLoading = true;
            }
            
            if (SceneManager.GetActiveScene().name == "levelTwo")
            {
                GameManager._instance.LoadLevelThree();
                isLevelLoading = true;
            }
            
            if (SceneManager.GetActiveScene().name == "levelThree")
            {
                GameManager._instance.LoadLevelFour();
                isLevelLoading = true;
            }
            
            if (SceneManager.GetActiveScene().name == "levelFour" || SceneManager.GetActiveScene().name == "lukeLevel" || SceneManager.GetActiveScene().name == "rhysLevel" || SceneManager.GetActiveScene().name == "tristonLevel" || SceneManager.GetActiveScene().name == "tashLevel" || SceneManager.GetActiveScene().name == "zharaLevel")
            {
                GameManager._instance.FinishToMainMenu();
                isLevelLoading = true;
            }
        }
    }
}
