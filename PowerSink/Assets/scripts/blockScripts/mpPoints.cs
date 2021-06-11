using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mpPoints : MonoBehaviour
{
    public baseBlock endBaseBlock;
    public baseBlock startBaseBlock;
    public GameObject startPoint;
    public GameObject endPoint;
    public GameObject movingPlatform;
    public bool isStartPoint;
    private bool isStartPowered;
    private bool isEndPowered;

    public void Start()
    {
        if (startPoint == this.gameObject)
        {
            isStartPoint = true;
        }
        else if (endPoint == this.gameObject)
        {
            isStartPoint = false;
        }

        isStartPowered = startBaseBlock.isPowered;
        isEndPowered = endBaseBlock.isPowered;
    }

    public void FixedUpdate()
    {
        Power();
    }

    public void Power()
    {   
        if (isStartPowered != startBaseBlock.isPowered)
        {
            if (startBaseBlock.isPowered)
            {
                endBaseBlock.powerOn();
                movingPlatform.GetComponent<movingPlatform>().isPowered = true;
            }
            else
            {
                endBaseBlock.powerOff();
                movingPlatform.GetComponent<movingPlatform>().isPowered = false;
            }
        }

        if (isEndPowered != endBaseBlock.isPowered)
        {
            if (endBaseBlock.isPowered)
            {
                startBaseBlock.powerOn();
                movingPlatform.GetComponent<movingPlatform>().isPowered = true;
            }
            else
            {
                startBaseBlock.powerOff();
                movingPlatform.GetComponent<movingPlatform>().isPowered = false;
            }
        }

        isStartPowered = startBaseBlock.isPowered;
        isEndPowered = endBaseBlock.isPowered;

    }
}
