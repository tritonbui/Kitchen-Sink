using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public Transform cam;
    private Rigidbody _rb;
    private float xRotation;
    private float sensitivity = 75f;
    private float sensMultiplier = 1f;
    public float moveSpeed = 4500f;
    public float maxSpeed = 20f;
    public bool isGrounded;
    public LayerMask whatIsGround;
    public float counterMovement = 0.175f;
    private float threshold = 0.01f;
    public float maxSlopeAngle = 35f;
    private bool isReadyToJump = true;
    private float jumpCooldown = 0.25f;
    public float jumpForce = 550f;
    private float desiredX;
    private bool cancellingGrounded;

    float x, y;
    bool jumping, sprinting;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        MyInput();
        Look();
    }

    private void MyInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        jumping = Input.GetButton("Jump");
    }

    public void Movement()
    {
        //Extra gravity
        //_rb.AddForce(Vector3.down * Time.deltaTime * 10);
        
        //Find actual velocity relative to where player is looking
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;

        //Counteractsloppy movement
        CounterMovement(x, y, mag);
        
        //If holding jump && ready to jump, then jump
        if (isReadyToJump && jumping) Jump();

        //Set max speed
        float maxSpeed = this.maxSpeed;
        
        //If speed is larger than maxspeed, cancel out the input so you don't go over max speed
        if (x > 0 && xMag > maxSpeed) x = 0;
        if (x < 0 && xMag < -maxSpeed) x = 0;
        if (y > 0 && yMag > maxSpeed) y = 0;
        if (y < 0 && yMag < -maxSpeed) y = 0;

        //Some multipliers
        float multiplier = 1f, multiplierV = 1f;
        
        // Movement in air
        if (!isGrounded) 
        {
            multiplier = 0.5f;
            multiplierV = 0.5f;
        }

        //Apply forces to move player
        _rb.AddForce(orientation.transform.forward * y * moveSpeed * Time.deltaTime * multiplier * multiplierV);
        _rb.AddForce(orientation.transform.right * x * moveSpeed * Time.deltaTime * multiplier);
    }

    private void Jump()
    {
        if(isGrounded && isReadyToJump)
        {
            isReadyToJump = false;

            _rb.AddForce(Vector2.up * jumpForce * 1.5f);

            Vector3 velocity = _rb.velocity;
            if (_rb.velocity.y < 0.5f)
            {
                _rb.velocity = new Vector3(velocity.x, 0, velocity.z);
            }
            else if (_rb.velocity.y > 0f)
            {
                _rb.velocity = new Vector3(velocity.x, velocity.y / 2, velocity.z);
            }
            
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ResetJump()
    {
        isReadyToJump = true;
    }

    public void Look()
    {
        float targetAngle = cam.eulerAngles.y;


        player.transform.localRotation = Quaternion.Euler(0, targetAngle, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, targetAngle, 0);
    }

    private void CounterMovement(float x, float y, Vector2 mag)
    {
        if (!isGrounded || jumping) return;

        //Counter movement
        if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0)) 
        {
            _rb.AddForce(moveSpeed * orientation.transform.right * Time.deltaTime * -mag.x * counterMovement);
        }

        if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0)) 
        {
            _rb.AddForce(moveSpeed * orientation.transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }
        
        //Limit diagonal running.
        if (Mathf.Sqrt((Mathf.Pow(_rb.velocity.x, 2) + Mathf.Pow(_rb.velocity.z, 2))) > maxSpeed) 
        {
            float fallspeed = _rb.velocity.y;
            Vector3 n = _rb.velocity.normalized * maxSpeed;
            _rb.velocity = new Vector3(n.x, fallspeed, n.z);
        }
    }

    public Vector2 FindVelRelativeToLook() 
    {
        float lookAngle = orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(_rb.velocity.x, _rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = _rb.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);
        
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
    }

}
