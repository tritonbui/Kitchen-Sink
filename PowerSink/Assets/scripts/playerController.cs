using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public Transform cam;
    public AudioSource receptacleSound;
    protected GameObject touchedPowerOrb = null;
    protected GameObject heldPowerOrb = null;
    public GameObject touchedReceptacle = null;
    public GameObject touchedToggleSwitch = null;
    public Transform orbSpawnPoint;
    public Transform playerSpawnPoint;
    public GameObject _orb;
    private Rigidbody _rb;
        
    private float threshold = 0.01f;
    private float maxSlopeAngle = 35f;
    private bool isReadyToJump = true;
    private bool isTouchingToggle = false;
    private float jumpCooldown = 0.25f;
    private float desiredX;
    private float x;
    private float y;
    
    public bool isGrounded {get; protected set;}
    public bool hasPowerOrb {get; set;} = false;
    public bool canPlaceOrb {get; set;} = true;
    public LayerMask whatIsGround;
    private bool cancellingGrounded;

    [Range (0, 180)]
    public float angleTolerance = 45f;

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
        canPlaceOrb = true;
    }

    private void Update()
    {
        Movement();
        pickUpPutDown();
        toggleSwitch();
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
    }

    private void toggleSwitch()
    {
        if (Input.GetButtonDown("Interact 2") && touchedToggleSwitch != null && isTouchingToggle)
        {
            float angle = Vector3.SignedAngle(Vector3.Scale(new Vector3(1, 0, 1), touchedToggleSwitch.transform.position - transform.position).normalized, transform.forward, Vector3.up);

            if (angle < angleTolerance && angle > -angleTolerance)
            {
                touchedToggleSwitch.GetComponent<toggleSwitch>().Toggle();
            }
        }

        if (isTouchingToggle)
        {
            float angle = Vector3.SignedAngle(Vector3.Scale(new Vector3(1, 0, 1), touchedToggleSwitch.transform.position - transform.position).normalized, transform.forward, Vector3.up);

            if (angle < angleTolerance && angle > -angleTolerance)
            {
                GameManager._instance.gameUI.lookAtSwitch();
            }
            
        }
        else if (touchedPowerOrb == null)
        {
            if (touchedReceptacle != null)
            {
                float angle = Vector3.SignedAngle(Vector3.Scale(new Vector3(1, 0, 1), touchedReceptacle.transform.position - transform.position).normalized, transform.forward, Vector3.up);

                if (angle > angleTolerance && angle < -angleTolerance)
                {
                    GameManager._instance.gameUI.lookAtNothing();
                }

            }
            else if (touchedReceptacle == null)
            {
                GameManager._instance.gameUI.lookAtNothing();
            }
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (LayerMask.NameToLayer("PowerOrb") == col.gameObject.layer)
        {
            touchedPowerOrb = col.gameObject;
        }

        if (col.gameObject.tag == "receptacleBlock" || col.gameObject.tag == "psuedoReceptacle")
        {
            touchedReceptacle = col.gameObject;
        }

        if (col.gameObject.tag == "endBox")
        {
            if (SceneManager.GetActiveScene().name == "levelOne")
            {
                GameManager._instance.LoadLevelTwo();
            }
            
            if (SceneManager.GetActiveScene().name == "levelTwo")
            {
                GameManager._instance.LoadLevelThree();
            }
            
            if (SceneManager.GetActiveScene().name == "levelThree")
            {
                GameManager._instance.LoadLevelFour();
            }
            
            if (SceneManager.GetActiveScene().name == "levelFour" || SceneManager.GetActiveScene().name == "lukeLevel")
            {
                GameManager._instance.FinishToMainMenu();
            }
        }

        if (LayerMask.NameToLayer("deathBarrier") == col.gameObject.layer)
        {
            Respawn();
        }

        if (LayerMask.NameToLayer("toggleSwitch") == col.gameObject.layer)
        {
            touchedToggleSwitch = col.gameObject;
            isTouchingToggle = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (LayerMask.NameToLayer("PowerOrb") == col.gameObject.layer)
        {
            touchedPowerOrb = null;
        }

        if (col.gameObject.tag == "receptacleBlock" || col.gameObject.tag == "psuedoReceptacle")
        {
            touchedReceptacle = null;
        }

        if (LayerMask.NameToLayer("toggleSwitch") == col.gameObject.layer)
        {
            isTouchingToggle = false;
        }
    }

    public void Respawn()
    {
        GameObject _pf;
        _pf = GameObject.Find("playerFollow");

        GameObject _cam;
        _cam = GameObject.Find("Main Camera");

        _cam.transform.position = playerSpawnPoint.position + new Vector3(0, 5, -17);
        _pf.transform.position = playerSpawnPoint.position;
        transform.position = playerSpawnPoint.position;
        _rb.velocity = Vector3.zero;

        if (heldPowerOrb != null)
        {
            GameManager._instance.gameUI.putDownOrb();
            heldPowerOrb.SetActive(true);
            heldPowerOrb.GetComponent<powerOrb>().Die();
            heldPowerOrb = null;
            hasPowerOrb = false;
        }

        if (touchedToggleSwitch != null)
        {
            touchedToggleSwitch.GetComponent<toggleSwitch>().StateA();
        }
    }

    public void pickUpPutDown()
    {
        if (touchedReceptacle != null && hasPowerOrb)
        {
            if (!touchedReceptacle.GetComponent<receptacleBlock>().hasPowerOrb)
            {
                float angle = Vector3.SignedAngle(Vector3.Scale(new Vector3(1, 0, 1), touchedReceptacle.transform.position - transform.position).normalized, transform.forward, Vector3.up);

                if (angle < angleTolerance && angle > -angleTolerance)
                {
                    GameManager._instance.gameUI.lookAtReceptacle();
                }
            }
        }

        if (touchedReceptacle != null && !hasPowerOrb)
        {
            if (touchedReceptacle.GetComponent<receptacleBlock>().hasPowerOrb && touchedReceptacle.GetComponent<receptacleBlock>().canTakePowerOrb)
            {
                float angle = Vector3.SignedAngle(Vector3.Scale(new Vector3(1, 0, 1), touchedReceptacle.transform.position - transform.position).normalized, transform.forward, Vector3.up);

                if (angle < angleTolerance && angle > -angleTolerance)
                {
                    GameManager._instance.gameUI.lookAtOrb();
                    
                }
            }
        }

        if (touchedPowerOrb != null)
        {
            float angle = Vector3.SignedAngle(Vector3.Scale(new Vector3(1, 0, 1), touchedPowerOrb.transform.position - transform.position).normalized, transform.forward, Vector3.up);

            if (angle < angleTolerance && angle > -angleTolerance)
            {
                GameManager._instance.gameUI.lookAtOrb();
            }
        }

        if (Input.GetButtonDown("Interact") && !hasPowerOrb && touchedPowerOrb != null)
        {
            pickUp();
            if (!canPlaceOrb)
            {
                canPlaceOrb = true;
            }
            
            return;
        }

        if (Input.GetButtonDown("Interact") && hasPowerOrb && touchedPowerOrb == null)
        {
            if(touchedReceptacle != null && !touchedReceptacle.GetComponent<receptacleBlock>().hasPowerOrb)
            {
                receptaclePutDown();
                return;
            }
            else if (touchedReceptacle == null && canPlaceOrb)
            {
                putDown();
            }
            else if (touchedReceptacle != null && canPlaceOrb)
            {
                float angle = Vector3.SignedAngle(Vector3.Scale(new Vector3(1, 0, 1), touchedReceptacle.transform.position - transform.position).normalized, transform.forward, Vector3.up);

                if (angle > angleTolerance || angle < -angleTolerance)
                {
                    putDown();
                }

            }
        }

        if (Input.GetButtonDown("Interact") && !hasPowerOrb && touchedPowerOrb == null && touchedReceptacle != null)
        {
            if (touchedReceptacle.GetComponent<receptacleBlock>().hasPowerOrb && touchedReceptacle.GetComponent<receptacleBlock>().canTakePowerOrb)
            {
                receptaclePickUp();
                touchedReceptacle.GetComponent<receptacleBlock>().receptacleOff();
            }
        }
    }

    public void pickUp()
    {
        float angle = Vector3.SignedAngle(Vector3.Scale(new Vector3(1, 0, 1), touchedPowerOrb.transform.position - transform.position).normalized, transform.forward, Vector3.up);

        if (isGrounded && angle < angleTolerance && angle > -angleTolerance)
        {
            hasPowerOrb = true;
            receptacleSound.Play();
            GameManager._instance.gameUI.pickUpOrb();
            heldPowerOrb = touchedPowerOrb;
            touchedPowerOrb = null;
            heldPowerOrb.SetActive(false);
        }
    }

    public void receptaclePickUp()
    {   
        float angle = Vector3.SignedAngle(Vector3.Scale(new Vector3(1, 0, 1), touchedReceptacle.transform.position - transform.position).normalized, transform.forward, Vector3.up);

        if (isGrounded && angle < angleTolerance && angle > -angleTolerance)
        {
            hasPowerOrb = true;
            receptacleSound.Play();
            heldPowerOrb = touchedReceptacle.GetComponent<receptacleBlock>().insertedPowerOrb;
            touchedReceptacle.GetComponent<receptacleBlock>().insertedPowerOrb = null;
            touchedReceptacle.GetComponent<receptacleBlock>().hasPowerOrb = false;
            touchedReceptacle.GetComponent<receptacleBlock>().startPowerDown();
            GameManager._instance.gameUI.pickUpOrb();
        }
    }

    public void putDown()
    {
        if(isGrounded)
        {
            hasPowerOrb = false;
            receptacleSound.Play();
            GameManager._instance.gameUI.putDownOrb();
            heldPowerOrb.transform.position = orbSpawnPoint.position;
            heldPowerOrb.SetActive(true);
            heldPowerOrb = null;
        }
    }

    public void receptaclePutDown()
    {
        float angle = Vector3.SignedAngle(Vector3.Scale(new Vector3(1, 0, 1), touchedReceptacle.transform.position - transform.position).normalized, transform.forward, Vector3.up);

        if (isGrounded && angle < angleTolerance && angle > -angleTolerance)
        {
            hasPowerOrb = false;
            receptacleSound.Play();
            touchedReceptacle.GetComponent<receptacleBlock>().insertedPowerOrb = heldPowerOrb;
            heldPowerOrb = null;
            GameManager._instance.gameUI.putDownOrb();
            touchedReceptacle.GetComponent<receptacleBlock>().startPowerUp();
            touchedReceptacle.GetComponent<receptacleBlock>().hasPowerOrb = true;
        }
        else
        {
            putDown();
        }
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
