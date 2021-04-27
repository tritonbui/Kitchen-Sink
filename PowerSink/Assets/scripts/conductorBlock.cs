using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conductorBlock : MonoBehaviour
{
    public baseBlock baseBlock;
    public bool isLightOn = true;
    public GameObject lightOn;
    public GameObject lightOff;

    public void Update()
    {
        if (baseBlock.isPowered && !isLightOn)
        {
            lightOn.SetActive(true);
            lightOff.SetActive(false);
            isLightOn = true;
        }
        
        if (!baseBlock.isPowered && isLightOn)
        {
            lightOn.SetActive(false);
            lightOff.SetActive(true);
            isLightOn = false;
        }
    }
}
