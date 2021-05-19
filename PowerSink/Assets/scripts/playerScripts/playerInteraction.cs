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
    public PlayerInput playerInput;

    public bool isGrounded {get; set;}
    public bool hasPowerOrb {get; set;} = false;
    public bool canPlaceOrb {get; set;} = true;

    [Range (0, 180)]
    public float angleTolerance = 45f;
    
    [Range (0, 10)]
    public float distanceTolerance = 5f;

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

    public void orbInteraction(InputAction.CallbackContext context)
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
            GameManager._instance.gameUI.pickUpOrb();
            heldPowerOrb = touchedPowerOrb;
            touchedPowerOrb = null;
            heldPowerOrb.SetActive(false);
        }
    }

    public void receptaclePickUp()
    {   
        if (isGrounded)
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
        if (isGrounded)
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

    private void LookAtUI()
    {
        if ((touchedPowerOrb != null || touchedReceptacle != null || touchedToggleSwitch != null) && isGrounded)
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
        else
        {
            GameManager._instance.gameUI.lookAtNothing();
        }
    }

    private void OnTriggerStay(Collider col)
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
                touchedReceptacle = col.gameObject;
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

            if ((col.gameObject.tag == "receptacleBlock" || col.gameObject.tag == "psuedoReceptacle") && touchedReceptacle == col.gameObject)
            {
                touchedReceptacle = null;
            }

            if (LayerMask.NameToLayer("toggleSwitch") == col.gameObject.layer && touchedToggleSwitch == col.gameObject)
            {
                touchedToggleSwitch = null;
            }
        }
    }

    private void OnTriggerExit (Collider col)
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
    }
}
