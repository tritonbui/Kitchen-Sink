using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public Transform cam;
    protected GameObject touchedPowerOrb = null;
    public GameObject touchedReceptacle = null;
    public Transform orbSpawnPoint;
    public GameObject _orb;
    private Rigidbody _rb;

    public Vector2 currentSpeed;
    
    private float threshold = 0.01f;
    private float maxSlopeAngle = 35f;
    private bool isReadyToJump = true;
    private float jumpCooldown = 0.25f;
    private float desiredX;
    private float x;
    private float y;
    
    public bool isGrounded;
    public bool hasPowerOrb = false;
    public LayerMask whatIsGround;
    private bool cancellingGrounded;

    [Header("Movement Stuff")]
    public float moveSpeed = 4500f;
    public float jumpForce = 550f;
    public float maxSpeed = 7f;
    public float counterMovement = 0.175f;
    public float airMultiplier = 0.5f;


    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    void Start()
    {
        
    }

    private void Update()
    {
        Movement();
        pickUpPutDown();
        MyInput();
        Look();
    }

    private void MyInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
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
        
        //If holding jump && ready to jump, then jump
        if (isReadyToJump && Input.GetButtonDown("Jump")) Jump();

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

        currentSpeed = new Vector2(xMag, yMag);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (LayerMask.NameToLayer("PowerOrb") == col.gameObject.layer)
        {
            touchedPowerOrb = col.gameObject;
        }

        if (col.gameObject.tag == "receptacleBlock")
        {
            touchedReceptacle = col.gameObject;
        }

        if (col.gameObject.tag == "endBox")
        {
            GameManager._instance.NextLevel();
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (LayerMask.NameToLayer("PowerOrb") == col.gameObject.layer)
        {
            touchedPowerOrb = null;
        }

        if (col.gameObject.tag == "receptacleBlock")
        {
            touchedReceptacle = null;
        }
    }

    public void pickUpPutDown()
    {
        if (Input.GetButtonDown("Interact") && !hasPowerOrb && touchedPowerOrb != null)
        {
            pickUp();
        }

        if (Input.GetButtonDown("Interact") && hasPowerOrb && touchedPowerOrb == null)
        {
            if(touchedReceptacle != null && !touchedReceptacle.GetComponent<receptacleBlock>().hasPowerOrb)
            {
                receptaclePutDown();
                return;
            }
            else if (touchedReceptacle == null)
            {
                putDown();
            }
        }

        if (Input.GetButtonDown("Interact") && !hasPowerOrb && touchedPowerOrb == null && touchedReceptacle != null)
        {
            if (touchedReceptacle.GetComponent<receptacleBlock>().hasPowerOrb && touchedReceptacle.GetComponent<receptacleBlock>().canTakePowerOrb)
            {
                receptaclePickUp();
            }
        }
    }

    public void pickUp()
    {
        hasPowerOrb = true;
        GameManager._instance.gameUI.pickUpOrb();
        Destroy(touchedPowerOrb);
    }

    public void receptaclePickUp()
    {
        hasPowerOrb = true;
        touchedReceptacle.GetComponent<receptacleBlock>().hasPowerOrb = false;
        touchedReceptacle.GetComponent<receptacleBlock>().startPowerDown();
        GameManager._instance.gameUI.pickUpOrb();
    }

    public void putDown()
    {
        hasPowerOrb = false;
        GameManager._instance.gameUI.putDownOrb();
        GameObject newPowerOrb = Instantiate(_orb, new Vector3(orbSpawnPoint.position.x, orbSpawnPoint.position.y, orbSpawnPoint.position.z), Quaternion.Euler(0, 0, 90));
    }

    public void receptaclePutDown()
    {
        hasPowerOrb = false;
        GameManager._instance.gameUI.putDownOrb();
        touchedReceptacle.GetComponent<receptacleBlock>().startPowerUp();
        touchedReceptacle.GetComponent<receptacleBlock>().hasPowerOrb = true;
    }

    private void Jump()
    {
        if(isGrounded && isReadyToJump)
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
