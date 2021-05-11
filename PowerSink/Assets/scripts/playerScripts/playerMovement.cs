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
        
    private float threshold = 0.01f;
    private float maxSlopeAngle = 35f;
    private bool isReadyToJump = true;
    private float jumpCooldown = 0.25f;
    private float desiredX;
    private float x;
    private float y;
    
    public bool isGrounded {get; protected set;}
    public LayerMask whatIsGround;
    private bool cancellingGrounded;
    private bool isLevelLoading = false;

    [Header("Movement Stuff")]
    public float moveSpeed = 4500f;
    public float jumpForce = 550f;
    public float maxSpeed = 7f;
    public float counterMovement = 0.175f;
    public float airMultiplier = 0.5f;


    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        isLevelLoading = false;
    }

    private void Update()
    {
        Look();
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

    public void Animations(InputAction.CallbackContext context)
    {
        if (context.performed && (context.ReadValue<Vector2>().x >= 0.1f || context.ReadValue<Vector2>().x <= -0.1f || context.ReadValue<Vector2>().y >= 0.1f || context.ReadValue<Vector2>().y <= -0.1f))
        {
            _rds.playRun();
        }

        if (context.canceled && (context.ReadValue<Vector2>().x <= 0.1f && context.ReadValue<Vector2>().x >= -0.1f && context.ReadValue<Vector2>().y <= 0.1f && context.ReadValue<Vector2>().y >= -0.1f))
        {
            _rds.playIdle();
        }
    }

    public void Look()
    {
        float targetAngle = cam.eulerAngles.y;
        float moveAngle = Mathf.Atan2(_rb.velocity.x, _rb.velocity.z) * Mathf.Rad2Deg;
    
        if (x != 0 || y != 0)
        {
            player.transform.localRotation = Quaternion.Euler(0, moveAngle, 0);
        }

        orientation.transform.localRotation = Quaternion.Euler(0, targetAngle, 0);
    }

    public void Movement()
    {
        //Extra gravity
        _rb.AddForce(Vector3.down * Time.deltaTime * 10);
        
        //Find actual velocity relative to where player is looking
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;

        //Counteractsloppy movement
        CounterMovement(x, y, mag);

        //Some airMultipliers
        float aMultiplier;
        aMultiplier = isGrounded ? 1f : airMultiplier;

        //Apply forces to move player
        if(xMag < maxSpeed && x > 0)
        {
            _rb.AddForce(orientation.transform.right * x * moveSpeed * Time.deltaTime * aMultiplier);
        }

        if(xMag > -maxSpeed && x < 0)
        {
            _rb.AddForce(orientation.transform.right * x * moveSpeed * Time.deltaTime * aMultiplier);
        }

        if(yMag < maxSpeed && y > 0)
        {
            _rb.AddForce(orientation.transform.forward * y * moveSpeed * Time.deltaTime * aMultiplier);
        }

        if(yMag > -maxSpeed && y < 0)
        {
            _rb.AddForce(orientation.transform.forward * y * moveSpeed * Time.deltaTime * aMultiplier);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && isGrounded && isReadyToJump && !GameManager._instance.isPaused)
        {
            isReadyToJump = false;

            _rb.AddForce(Vector2.up * jumpForce);
            
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ResetJump()
    {
        isReadyToJump = true;
    }

    private void CounterMovement(float x, float y, Vector2 mag)
    {
        if (isGrounded && x == 0)
        {
            if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0)) 
            {
                _rb.AddForce(moveSpeed * orientation.transform.right * Time.deltaTime * -mag.x * counterMovement);
            }
        }

        if (isGrounded && y == 0)
        {
            if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0)) 
            {
                _rb.AddForce(moveSpeed * orientation.transform.forward * Time.deltaTime * -mag.y * counterMovement);
            }
        }
        
        if (isGrounded)
        {
            //Limit diagonal running.
            if (Mathf.Sqrt((Mathf.Pow(_rb.velocity.x, 2) + Mathf.Pow(_rb.velocity.z, 2))) > maxSpeed) 
            {
                float fallspeed = _rb.velocity.y;
                Vector3 n = _rb.velocity.normalized * maxSpeed;
                _rb.velocity = new Vector3(n.x, fallspeed, n.z);
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

    private bool IsFloor(Vector3 v) 
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    // Handle ground detection
    private void OnCollisionStay(Collision other) 
    {
        //Make sure we are only checking for walkable layers
        int layer = other.gameObject.layer;
        if (whatIsGround != (whatIsGround | (1 << layer))) return;

        //Iterate through every collision in a physics update
        for (int i = 0; i < other.contactCount; i++) 
        {
            Vector3 normal = other.contacts[i].normal;
            //FLOOR
            if (IsFloor(normal)) 
            {
                isGrounded = true;
                playerInteraction.isGrounded = true;
                cancellingGrounded = false;
                //normalVector = normal;
                CancelInvoke(nameof(StopGrounded));
            }
        }

        //Invoke ground/wall cancel, since we can't check normals with CollisionExit
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
        playerInteraction.isGrounded = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "endBox" && !isLevelLoading)
        {
            if (SceneManager.GetActiveScene().name == "levelOne")
            {
                GameManager._instance.LoadLevelTwo();
                isLevelLoading = true;
                Debug.Log("Level 2 Loaded");
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
