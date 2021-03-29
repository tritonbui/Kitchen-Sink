using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseBlock : MonoBehaviour
{
    public bool isPowered = false;
    public bool doesConductPower;
    public float checkDist = 0.1f;
    public Transform _topRay;
    public GameObject _top;

    public Transform _botRay;    
    public GameObject _bot;

    public Transform _frontRay;
    public GameObject _front;

    public Transform _backRay;
    public GameObject _back;

    public Transform _leftRay;
    public GameObject _left;

    public Transform _rightRay;
    public GameObject _right;

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

        //powerOn();
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
                if(_top != null)
                {
                    _top.GetComponent<baseBlock>().powerOn();
                }

                if(_bot != null)
                {
                    _bot.GetComponent<baseBlock>().powerOn();
                }

                if(_front != null)
                {
                    _front.GetComponent<baseBlock>().powerOn();
                }

                if(_back != null)
                {
                    _back.GetComponent<baseBlock>().powerOn();
                }

                if(_left != null)
                {
                    _left.GetComponent<baseBlock>().powerOn();
                }

                if(_right != null)
                {
                    _right.GetComponent<baseBlock>().powerOn();
                }
            }
        }
    }
}
