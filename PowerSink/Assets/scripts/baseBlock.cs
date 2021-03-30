using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseBlock : MonoBehaviour
{
    public bool isPowered = false;
    public bool doesConductPower;
    public float checkDist = 0.5f;

    public Transform _topRay;
    protected GameObject _top = null;

    public Transform _botRay;    
    protected GameObject _bot = null;

    public Transform _frontRay;
    protected GameObject _front = null;

    public Transform _backRay;
    protected GameObject _back = null;

    public Transform _leftRay;
    protected GameObject _left = null;

    public Transform _rightRay;
    protected GameObject _right = null;

    public void Start()
    {   
        if (this.tag == "conductsPower")
        {
            doesConductPower = true;
        }
        else
        {
            doesConductPower = false;
        }

        assignAdjBlocks();
    }

    public void assignAdjBlocks()
    {
        LayerMask ground = LayerMask.GetMask("Ground");
        
        RaycastHit hit;

        if(Physics.Raycast(_topRay.position, _topRay.up, out hit, checkDist, ground))   
        {
            _top = hit.transform.gameObject;
        }

        if(Physics.Raycast(_botRay.position, -_botRay.up, out hit, checkDist, ground))   
        {
            _bot = hit.transform.gameObject;
        }     

        if(Physics.Raycast(_frontRay.position, _frontRay.forward, out hit, checkDist, ground))   
        {
            _front = hit.transform.gameObject;
        }     

        if(Physics.Raycast(_backRay.position, -_backRay.forward, out hit, checkDist, ground))   
        {
            _back = hit.transform.gameObject;
        }     

        if(Physics.Raycast(_leftRay.position, -_leftRay.right, out hit, checkDist, ground))   
        {
            _left = hit.transform.gameObject;
        }     

        if(Physics.Raycast(_rightRay.position, _rightRay.right, out hit, checkDist, ground))   
        {
            _right = hit.transform.gameObject;
        }
    }

    public void powerOn()
    {
        if(!isPowered)
        {
            isPowered = true;

            if(doesConductPower)
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

    public void powerOff()
    {
        if(isPowered)
        {
            isPowered = false;

            if(doesConductPower)
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
}
