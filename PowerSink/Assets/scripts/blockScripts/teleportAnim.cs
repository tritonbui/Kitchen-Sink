using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportAnim : MonoBehaviour
{
    public Animator animator;
    public string OnAnimation;
    public string OffAnimation;
    public baseBlock baseBlock;
    public bool isLightOn = false;

    public void Update()
    {
        if (baseBlock.isPowered && !isLightOn)
        {
            animator.Play(OnAnimation, 0, 0f);
            isLightOn = true;
        }

        if (!baseBlock.isPowered && isLightOn)
        {
            animator.Play(OffAnimation, 0, 0f);
            isLightOn = false;
        }
    }
}
