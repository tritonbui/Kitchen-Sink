using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class receptacleBlock : MonoBehaviour
{
    public baseBlock baseBlock;
    public Animator animator;
    public signalBlock _sb;
    public GameObject powerOrbAsset;
    public GameObject insertedPowerOrb = null;
    public bool hasPowerOrb = false;
    public bool check = false;
    public bool canTakePowerOrb = true;

    public void Start()
    {
        _sb = GameObject.Find("signalBlock").GetComponent<signalBlock>();
        powerOrbAsset = transform.GetChild(2).gameObject;
        canTakePowerOrb = true;
    }
    
    public void Update()
    {
        if (hasPowerOrb && !baseBlock.isPowered)
        {
            startPowerUp();
        }

        if (hasPowerOrb && !powerOrbAsset.activeSelf && !check)
        {
            animator.Play("receptacleOn", 0, 0f);
            check = true;
        }
    }

    public void receptacleOff()
    {
        animator.Play("receptacleOff", 0, 0f);
        check = false;
    }

    public void startPowerUp()
    {
        baseBlock.powerOn();
        if (this.tag == "receptacleBlock")
        {
            _sb.IncreasePoweredReceptacles();
        }
    }

    public void startPowerDown()
    {
        baseBlock.powerOff();
        if (this.tag == "receptacleBlock")
        {
            _sb.DecreasePoweredReceptacles();
        }
    }
}
