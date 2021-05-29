using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conductorBlock : MonoBehaviour
{
    public baseBlock baseBlock;
    public Animator animator;
    public AudioSource powerUp;
    public bool isLightOn = false;

    public void Update() //manages updating the conductor's state based on power information from the baseBlock script & current status (meaning it wont play animations again and again)
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
