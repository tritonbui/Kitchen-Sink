using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mpPower : MonoBehaviour
{
    public bool isPowered = false;
    public bool doesConductPower;
    public float checkDist = 0.5f;

    //below is all the raycast stuff for a block to know it's surrounding blocks (startPoint)
    public Transform _frontRaySP;
    protected GameObject _frontSP = null;

    public Transform _backRaySP;
    protected GameObject _backSP = null;

    public Transform _leftRaySP;
    protected GameObject _leftSP = null;

    public Transform _rightRaySP;
    protected GameObject _rightSP = null;

    //below is all the raycast stuff for a block to know it's surrounding blocks (endPoint)
    public Transform _frontRayEP;
    protected GameObject _frontEP = null;

    public Transform _backRayEP;
    protected GameObject _backEP = null;

    public Transform _leftRayEP;
    protected GameObject _leftEP = null;

    public Transform _rightRayEP;
    protected GameObject _rightEP = null;

    public void Start()
    {   
        if (this.tag == "conductsPower")
        {
            doesConductPower = true; //automatically sets conductability based on block's tag
        }
        else
        {
            doesConductPower = false;
        }

        assignAdjBlocks(); //sets adjacent blocks with raycasts
    }

    public void FixedUpdate()
    {
        PowerCheck();
    }

    public void assignAdjBlocks() //using the raycast points set above, this function checks for blocks immediately adjacent to the current block, and assigns them to gameobject variables for later use
    {
        LayerMask ground = LayerMask.GetMask("Ground");
        
        RaycastHit hit; 

        if(Physics.Raycast(_frontRaySP.position, _frontRaySP.forward, out hit, checkDist, ground))   
        {
            _frontSP = hit.transform.gameObject;
        }     

        if(Physics.Raycast(_backRaySP.position, -_backRaySP.forward, out hit, checkDist, ground))   
        {
            _backSP = hit.transform.gameObject;
        }     

        if(Physics.Raycast(_leftRaySP.position, -_leftRaySP.right, out hit, checkDist, ground))   
        {
            _leftSP = hit.transform.gameObject;
        }     

        if(Physics.Raycast(_rightRaySP.position, _rightRaySP.right, out hit, checkDist, ground))   
        {
            _rightSP = hit.transform.gameObject;
        }

        if(Physics.Raycast(_frontRayEP.position, _frontRayEP.forward, out hit, checkDist, ground))   
        {
            _frontEP = hit.transform.gameObject;
        }     

        if(Physics.Raycast(_backRayEP.position, -_backRayEP.forward, out hit, checkDist, ground))   
        {
            _backEP = hit.transform.gameObject;
        }     

        if(Physics.Raycast(_leftRayEP.position, -_leftRayEP.right, out hit, checkDist, ground))   
        {
            _leftEP = hit.transform.gameObject;
        }     

        if(Physics.Raycast(_rightRayEP.position, _rightRayEP.right, out hit, checkDist, ground))   
        {
            _rightEP = hit.transform.gameObject;
        }
    }

    public void powerOn() //powering block function
    {
        isPowered = true; //sets block to powered

        if(doesConductPower) //starts power On function in adjacent blocks given the current block conducts power
        {
            _frontSP?.GetComponent<baseBlock>().powerOn();
            _backSP?.GetComponent<baseBlock>().powerOn();
            _leftSP?.GetComponent<baseBlock>().powerOn();
            _rightSP?.GetComponent<baseBlock>().powerOn();
            _frontEP?.GetComponent<baseBlock>().powerOn();
            _backEP?.GetComponent<baseBlock>().powerOn();
            _leftEP?.GetComponent<baseBlock>().powerOn();
            _rightEP?.GetComponent<baseBlock>().powerOn();
        }
    }

    public void powerOff() //powering block function
    {
        isPowered = false; //sets block to unpowered

        if(doesConductPower) //starts power Off function in adjacent blocks given the current block conducts power
        {
            _frontSP?.GetComponent<baseBlock>().powerOff();
            _backSP?.GetComponent<baseBlock>().powerOff();
            _leftSP?.GetComponent<baseBlock>().powerOff();
            _rightSP?.GetComponent<baseBlock>().powerOff();
            _frontEP?.GetComponent<baseBlock>().powerOff();
            _backEP?.GetComponent<baseBlock>().powerOff();
            _leftEP?.GetComponent<baseBlock>().powerOff();
            _rightEP?.GetComponent<baseBlock>().powerOff();
        }
    }

    public void PowerCheck()
    {
        /*if (isPowered && !_frontSP?.GetComponent<baseBlock>().isPowered && !_backSP?.GetComponent<baseBlock>().isPowered && !_leftSP?.GetComponent<baseBlock>().isPowered && !_rightSP?.GetComponent<baseBlock>().isPowered && !_frontEP?.GetComponent<baseBlock>().isPowered && !_backEP?.GetComponent<baseBlock>().isPowered && !_leftEP?.GetComponent<baseBlock>().isPowered && !_rightEP?.GetComponent<baseBlock>().isPowered)
        {
            powerOff();
        }*/
        
        if (_frontSP != null && !isPowered && _frontSP.GetComponent<baseBlock>().isPowered)
        {
            powerOn();
        }

        if (_backSP != null && !isPowered && _backSP.GetComponent<baseBlock>().isPowered)
        {
            powerOn();
        }

        if (_leftSP != null && !isPowered && _leftSP.GetComponent<baseBlock>().isPowered)
        {
            powerOn();
        }

        if (_rightSP != null && !isPowered && _rightSP.GetComponent<baseBlock>().isPowered)
        {
            powerOn();
        }

        if (_frontEP != null && !isPowered && _frontEP.GetComponent<baseBlock>().isPowered)
        {
            powerOn();
        }

        if (_backEP != null && !isPowered && _backEP.GetComponent<baseBlock>().isPowered)
        {
            powerOn();
        }

        if (_leftEP != null && !isPowered && _leftEP.GetComponent<baseBlock>().isPowered)
        {
            powerOn();
        }

        if (_rightEP != null && !isPowered && _rightEP.GetComponent<baseBlock>().isPowered)
        {
            powerOn();
        }
    }



}
