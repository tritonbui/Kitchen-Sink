using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conductorBlock : MonoBehaviour
{
    public baseBlock baseBlock;
    public AudioSource powerUp;
    public bool isLightOn = true;
    public GameObject lightOn;
    public GameObject lightOff;

    public void Update()
    {
        if (baseBlock.isPowered && !isLightOn)
        {
            lightOn.SetActive(true);
            lightOff.SetActive(false);
            powerUp.Play();
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
