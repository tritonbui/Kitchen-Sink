using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class receptacleBlock : MonoBehaviour
{
    public baseBlock baseBlock;
    public bool hasPowerOrb = false;

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
    }

    public void startPowerDown()
    {
        baseBlock.powerOff();
    }
}
