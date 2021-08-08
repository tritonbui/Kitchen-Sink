using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conductorBlock : MonoBehaviour
{
    public baseBlock baseBlock;
    public Animator animator;
    public AudioSource powerUp;
    public bool isLightOn = false;
    public GameObject LCL;
    public GameObject LCB;
    public GameObject LCR;
    public GameObject LCF;

    public void Start()
    {
        lightConeCheck();
    }

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

    public void lightConeCheck()
    {
        if (baseBlock._right)
        {
            LCR.SetActive(false);
        }

        if (baseBlock._left)
        {
            LCL.SetActive(false);
        }

        if (baseBlock._front)
        {
            LCF.SetActive(false);
        }

        if (baseBlock._back)
        {
            LCB.SetActive(false);
        }
    }
}
