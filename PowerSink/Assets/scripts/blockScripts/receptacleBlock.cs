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
    public bool orbCheck = true;
    public bool canTakePowerOrb = true;

    public void Start()
    {
        _sb = GameObject.Find("signalBlock").GetComponent<signalBlock>();
        powerOrbAsset = transform.GetChild(1).gameObject;
        canTakePowerOrb = true;
    }
    
    public void Update() //manages state of receptacle based on power orb and powered status
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

        if (this.tag == "receptacleBlock" && hasPowerOrb && orbCheck)
        {
            _sb.IncreasePoweredReceptacles();
            orbCheck = false;
        }
        else if (this.tag == "receptacleBlock" && !hasPowerOrb && !orbCheck)
        {
            _sb.DecreasePoweredReceptacles();
            orbCheck = true;
        }
    }

    public void receptacleOff()
    {
        animator.Play("receptacleOff", 0, 0f);
        check = false;
    }

    public void startPowerUp() //powers surrounding blocks and tells signal block that it is powered
    {
        baseBlock.receptaclePowerOn();
    }

    public void startPowerDown() //powers down surrounding blocks and tells signal block that it is no longer powered
    {
        baseBlock.receptaclePowerOff();
    }
}
