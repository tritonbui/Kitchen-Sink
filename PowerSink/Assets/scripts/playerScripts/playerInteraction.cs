using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class playerInteraction : MonoBehaviour
{
    public Transform orbSpawnPoint;
    public AudioSource receptacleSound;
    protected GameObject touchedPowerOrb = null;
    public GameObject heldPowerOrb = null;
    protected GameObject touchedReceptacle = null;
    public GameObject touchedToggleSwitch = null;

    private bool isTouchingToggle = false;
    public bool isGrounded {get; set;}
    public bool hasPowerOrb {get; set;} = false;
    public bool canPlaceOrb {get; set;} = true;

    [Range (0, 180)]
    public float angleTolerance = 45f;

    void Start()
    {
        canPlaceOrb = true;
    }

    private void Update()
    {
        LookAtUI();
        toggleSwitch();
        orbInteraction();
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
    }

    public void orbInteraction()
    {

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
                Debug.Log("picked up R");
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
            Debug.Log("picked up");
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

    private void LookAtUI()
    {
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
}
