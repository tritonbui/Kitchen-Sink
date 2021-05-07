using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conductorBlock : MonoBehaviour
{
    public baseBlock baseBlock;
    public Animator animator;
    public AudioSource powerUp;
    public bool isLightOn = false;

    public void Update()
    {
        if (baseBlock.isPowered && !isLightOn)
        {
            animator.Play("conductorOn", 0, 0f);
            powerUp.Play();
            isLightOn = true;
        }
        
        if (!baseBlock.isPowered && isLightOn)
        {
            animator.Play("conductorOff", 0, 0f);
            isLightOn = false;
        }
    }
}
