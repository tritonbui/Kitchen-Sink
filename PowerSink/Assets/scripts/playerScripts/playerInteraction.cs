using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerInteraction : MonoBehaviour
{
    public Transform orbSpawnPoint;
    public AudioSource receptacleSound;
    protected GameObject touchedPowerOrb = null;
    public GameObject heldPowerOrb = null;
    protected GameObject touchedReceptacle = null;
    public GameObject touchedToggleSwitch = null;
    public GameObject touchedTeleporter = null;
    public PlayerInput playerInput;
    public riggedDiverScript _rds;
    private Rigidbody _rb;

    public bool isGrounded {get; set;}
    public bool hasPowerOrb {get; set;} = false;
    public bool canPlaceOrb {get; set;} = true;

    [Range (0, 180)]
    public float angleTolerance = 45f;
    void Awake()
    {
        this._rb = this.GetComponent<Rigidbody>();
    }

    void Start()
    {
        canPlaceOrb = true;
    }

    private void FixedUpdate()
    {
        LookAtUI();
    }

    public void pauseMenuToggle(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && !GameManager._instance.isPlayerDead)
        {
            GameManager._instance.ResumeGame();
        }
    }

    public void toggleSwitch(InputAction.CallbackContext context)
    {
        if (context.performed && touchedToggleSwitch != null && !GameManager._instance.isPaused)
        {
            touchedToggleSwitch.GetComponent<toggleSwitch>().Toggle();
        }
    }

    public void teleport(InputAction.CallbackContext context)
    {
        if (context.performed && touchedTeleporter != null && !GameManager._instance.isPaused)
        {
            _rb.position = touchedTeleporter.GetComponent<teleportScript>().targetLocation.position;
            _rb.velocity = Vector3.zero;
        }
    }

    public void orbInteraction(InputAction.CallbackContext context) //all code related to Orb Interactions
    {
        if (context.performed && !hasPowerOrb && touchedPowerOrb != null && !GameManager._instance.isPaused)
        {
            pickUp();
            if (!canPlaceOrb)
            {
                canPlaceOrb = true;
            }
            
            return;
        }

        if (context.performed && hasPowerOrb && touchedPowerOrb == null && !GameManager._instance.isPaused)
        {
            if(touchedReceptacle != null && !touchedReceptacle.GetComponent<receptacleBlock>().hasPowerOrb)
            {
                receptaclePutDown();
                return;
            }
            else if (canPlaceOrb)
            {
                putDown();
            }
        }

        if (context.performed && !hasPowerOrb && touchedPowerOrb == null && touchedReceptacle != null && !GameManager._instance.isPaused)
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
        if (isGrounded)
        {
            hasPowerOrb = true;
            receptacleSound.Play();
            heldPowerOrb = touchedPowerOrb;
            touchedPowerOrb = null;
            heldPowerOrb.SetActive(false);
            _rds.hasPowerOrb();
        }
    }

    public void receptaclePickUp()
    {   
        if (isGrounded)
        {
            hasPowerOrb = true;
            heldPowerOrb = touchedReceptacle.GetComponent<receptacleBlock>().insertedPowerOrb;
            touchedReceptacle.GetComponent<receptacleBlock>().insertedPowerOrb = null;
            touchedReceptacle.GetComponent<receptacleBlock>().hasPowerOrb = false;
            receptacleSound.Play();
            touchedReceptacle.GetComponent<receptacleBlock>().startPowerDown();
            _rds.hasPowerOrb();
        }
    }

    public void putDown()
    {
        if(isGrounded)
        {
            hasPowerOrb = false;
            receptacleSound.Play();
            heldPowerOrb.transform.position = orbSpawnPoint.position;
            heldPowerOrb.SetActive(true);
            heldPowerOrb.transform.SetParent(null);
            heldPowerOrb = null;
            _rds.hasNothing();
        }
    }

    public void receptaclePutDown()
    {
        if (isGrounded)
        {
            hasPowerOrb = false;
            receptacleSound.Play();
            touchedReceptacle.GetComponent<receptacleBlock>().insertedPowerOrb = heldPowerOrb;
            heldPowerOrb = null;
            touchedReceptacle.GetComponent<receptacleBlock>().startPowerUp();
            touchedReceptacle.GetComponent<receptacleBlock>().hasPowerOrb = true;
            _rds.hasNothing();
        }
        else
        {
            putDown();
        }
    }

    private void LookAtUI() //All code related to appropriate UI control hints
    {
        if ((touchedPowerOrb != null || touchedReceptacle != null || touchedToggleSwitch != null) && isGrounded && touchedTeleporter == null)
        {
            if ((touchedPowerOrb != null && !hasPowerOrb) || (touchedReceptacle != null && !hasPowerOrb && touchedReceptacle.GetComponent<receptacleBlock>().hasPowerOrb && touchedReceptacle.GetComponent<receptacleBlock>().canTakePowerOrb))
            {
                if (playerInput.currentControlScheme == "Keyboard&Mouse")
                {
                    GameManager._instance.gameUI.lookAtOrbKM();
                }
                else if (playerInput.currentControlScheme == "Gamepad")
                {
                    GameManager._instance.gameUI.lookAtOrbGP();
                }
            }

            if (touchedToggleSwitch != null)
            {
                if (playerInput.currentControlScheme == "Keyboard&Mouse")
                {
                    GameManager._instance.gameUI.lookAtSwitchKM();
                }
                else if (playerInput.currentControlScheme == "Gamepad")
                {
                    GameManager._instance.gameUI.lookAtSwitchGP();
                }
            }

            if (touchedReceptacle != null && hasPowerOrb && !touchedReceptacle.GetComponent<receptacleBlock>().hasPowerOrb)
            {
                if (playerInput.currentControlScheme == "Keyboard&Mouse")
                {
                    GameManager._instance.gameUI.lookAtReceptacleKM();
                }
                else if (playerInput.currentControlScheme == "Gamepad")
                {
                    GameManager._instance.gameUI.lookAtReceptacleGP();
                }
            }
        }
        else if (touchedTeleporter != null)
        {
            if (playerInput.currentControlScheme == "Keyboard&Mouse")
            {
                GameManager._instance.gameUI.onTeleporterKM();
            }
            else if (playerInput.currentControlScheme == "Gamepad")
            {
                GameManager._instance.gameUI.onTeleporterGP();
            }
            
        }
        else
        {
            GameManager._instance.gameUI.lookAtNothing();
        }

        if (GameManager._instance.isLevelFinished && touchedToggleSwitch == null && touchedTeleporter == null)
        {
            GameManager._instance.gameUI.lookAtNothing();
        }
    }

    private void OnTriggerStay(Collider col) //Manages the interactions between player and all interactable objects in the scene
    {
        float angle = Vector3.SignedAngle(Vector3.Scale(new Vector3(1, 0, 1), col.gameObject.transform.position - transform.position).normalized, transform.forward, Vector3.up);
        float dist = Vector3.Distance(col.gameObject.transform.position, transform.position);
        
        if (angle < angleTolerance && angle > - angleTolerance)
        {
            if (LayerMask.NameToLayer("PowerOrb") == col.gameObject.layer)
            {
                touchedPowerOrb = col.gameObject;
            }

            if (col.gameObject.tag == "receptacleBlock" || col.gameObject.tag == "psuedoReceptacle")
            {
                touchedReceptacle = col.gameObject.transform.parent.gameObject;
            }

            if (LayerMask.NameToLayer("toggleSwitch") == col.gameObject.layer)
            {
                touchedToggleSwitch = col.gameObject;
            }
        }
        else
        {
            if (LayerMask.NameToLayer("PowerOrb") == col.gameObject.layer && touchedPowerOrb == col.gameObject)
            {
                touchedPowerOrb = null;
            }

            if ((col.gameObject.tag == "receptacleBlock" || col.gameObject.tag == "psuedoReceptacle") && touchedReceptacle == col.gameObject.transform.parent.gameObject)
            {
                touchedReceptacle = null;
            }

            if (LayerMask.NameToLayer("toggleSwitch") == col.gameObject.layer && touchedToggleSwitch == col.gameObject)
            {
                touchedToggleSwitch = null;
            }
        }

        if (col.gameObject.layer == LayerMask.NameToLayer("teleporter"))
        {
            touchedTeleporter = col.gameObject;
        }
    }

    private void OnTriggerExit (Collider col) //Manages disconnecting the player and all interactable objects being no longer interacted with (as a second layer of redundency)
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
            touchedToggleSwitch = null;
        }

        if (col.gameObject.layer == LayerMask.NameToLayer("teleporter"))
        {
            touchedTeleporter = null;
        }
        
    }
}
