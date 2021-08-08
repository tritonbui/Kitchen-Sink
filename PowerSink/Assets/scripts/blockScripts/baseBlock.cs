using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseBlock : MonoBehaviour
{
    public bool isPowered = false;
    public bool doesConductPower;
    public float checkDist = 0.5f;

    //below is all the raycast stuff for a block to know it's surrounding blocks
    public Transform _topRay;
    public GameObject _top {get; set;} = null;

    public Transform _botRay;    
    public GameObject _bot {get; set;} = null;

    public Transform _frontRay;
    public GameObject _front {get; set;} = null;

    public Transform _backRay;
    public GameObject _back {get; set;} = null;

    public Transform _leftRay;
    public GameObject _left {get; set;} = null;

    public Transform _rightRay;
    public GameObject _right {get; set;} = null;

    public void Awake()
    {   
        if (this.tag == "conductsPower" || this.tag == "mpPoints")
        {
            doesConductPower = true; //automatically sets conductability based on block's tag
        }
        else
        {
            doesConductPower = false;
        }

        assignAdjBlocks(); //sets adjacent blocks with raycasts
    }

    public void assignAdjBlocks() //using the raycast points set above, this function checks for blocks immediately adjacent to the current block, and assigns them to gameobject variables for later use
    {
        LayerMask powerSys = LayerMask.GetMask("powerSys");
        
        RaycastHit hit;

        if(Physics.Raycast(_topRay.position, _topRay.up, out hit, checkDist, powerSys))   
        {
            if(hit.transform.gameObject.ToString() == "signalBlock" || hit.transform.gameObject.layer == LayerMask.NameToLayer("powerSys"))
            {
                _top = hit.transform.gameObject;
            }
        }

        if(Physics.Raycast(_botRay.position, -_botRay.up, out hit, checkDist, powerSys))   
        {
            _bot = hit.transform.gameObject;
        }     

        if(Physics.Raycast(_frontRay.position, _frontRay.forward, out hit, checkDist, powerSys))   
        {
            _front = hit.transform.gameObject;
        }     

        if(Physics.Raycast(_backRay.position, -_backRay.forward, out hit, checkDist, powerSys))   
        {
            _back = hit.transform.gameObject;
        }     

        if(Physics.Raycast(_leftRay.position, -_leftRay.right, out hit, checkDist, powerSys))   
        {
            _left = hit.transform.gameObject;
        }     

        if(Physics.Raycast(_rightRay.position, _rightRay.right, out hit, checkDist, powerSys))   
        {
            _right = hit.transform.gameObject;
        }
    }

    public void powerOn() //powering block function
    {
        if(!isPowered)
        {
            isPowered = true; //sets block to powered

            if(doesConductPower) //starts power On function in adjacent blocks given the current block conducts power
            {
                _top?.GetComponent<baseBlock>().powerOn();
                _bot?.GetComponent<baseBlock>().powerOn();
                _front?.GetComponent<baseBlock>().powerOn();
                _back?.GetComponent<baseBlock>().powerOn();
                _left?.GetComponent<baseBlock>().powerOn();
                _right?.GetComponent<baseBlock>().powerOn();
            }
        }
    }

    public void powerOff() //depowering block function
    {
        if(isPowered)
        {
            isPowered = false; //sets block to unpowered

            if(doesConductPower) //starts powering down function in adjacent blocks given the current block conducts power
            {
                _top?.GetComponent<baseBlock>().powerOff();
                _bot?.GetComponent<baseBlock>().powerOff();
                _front?.GetComponent<baseBlock>().powerOff();
                _back?.GetComponent<baseBlock>().powerOff();
                _left?.GetComponent<baseBlock>().powerOff();
                _right?.GetComponent<baseBlock>().powerOff();
            }
        }
    }

    public void receptaclePowerOn()
    {
        isPowered = true; //sets block to powered

        _top?.GetComponent<baseBlock>().powerOn();
        _bot?.GetComponent<baseBlock>().powerOn();
        _front?.GetComponent<baseBlock>().powerOn();
        _back?.GetComponent<baseBlock>().powerOn();
        _left?.GetComponent<baseBlock>().powerOn();
        _right?.GetComponent<baseBlock>().powerOn();
    }

    public void receptaclePowerOff()
    {
        isPowered = false; //sets block to unpowered

        _top?.GetComponent<baseBlock>().powerOff();
        _bot?.GetComponent<baseBlock>().powerOff();
        _front?.GetComponent<baseBlock>().powerOff();
        _back?.GetComponent<baseBlock>().powerOff();
        _left?.GetComponent<baseBlock>().powerOff();
        _right?.GetComponent<baseBlock>().powerOff();
    }
}
