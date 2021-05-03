using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class receptacleBlock : MonoBehaviour
{
    public baseBlock baseBlock;
    public signalBlock _sb;
    public GameObject powerOrbAsset;
    public GameObject insertedPowerOrb = null;
    public bool hasPowerOrb = false;
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

        if (hasPowerOrb && !powerOrbAsset.activeSelf)
        {
            powerOrbAsset.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        
        if (!hasPowerOrb && powerOrbAsset.activeSelf)
        {
            powerOrbAsset.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
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
