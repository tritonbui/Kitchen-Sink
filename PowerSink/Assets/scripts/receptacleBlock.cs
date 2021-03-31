using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class receptacleBlock : MonoBehaviour
{
    public baseBlock baseBlock;
    public powerReceptorBlock _prb;
    public bool hasPowerOrb = false;
    public bool canTakePowerOrb = true;

    public void Start()
    {
        _prb = GameObject.Find("powerReceptorBlock").GetComponent<powerReceptorBlock>();
        canTakePowerOrb = true;
    }
    
    public void Update()
    {
        if (hasPowerOrb && !baseBlock.isPowered)
        {
            startPowerUp();
        }
    }
    
    public void startPowerUp()
    {
        baseBlock.powerOn();
        _prb.IncreasePoweredReceptacles();
    }

    public void startPowerDown()
    {
        baseBlock.powerOff();
        _prb.DecreasePoweredReceptacles();
    }
}
