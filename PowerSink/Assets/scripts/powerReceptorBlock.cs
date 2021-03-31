using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerReceptorBlock : MonoBehaviour
{
    public GameObject[] receptacles; //prefab
    public List<receptacleBlock> receptacleBlocks;
    public int receptacleNo;
    public int poweredReceptacles = 0;
    public bool isLevelFinished = false;

    public void Start()
    {
        receptacles = GameObject.FindGameObjectsWithTag("receptacleBlock");
        foreach (GameObject receptacle in receptacles)
        {
            if (receptacle.GetComponent<receptacleBlock>())
            {
                receptacleBlock receptacleBlock = receptacle.GetComponent<receptacleBlock>();
                receptacleBlocks.Add(receptacleBlock);
            }
        }
        receptacleNo = receptacles.Length;
    }

    public void Update()
    {
        if (receptacleNo == poweredReceptacles)
        {
            isLevelFinished = true;
            foreach (receptacleBlock blocks in receptacleBlocks)
            {
                blocks.canTakePowerOrb = false;
            }
        }
        else
        {
            isLevelFinished = false;
        }
    }

    public void IncreasePoweredReceptacles()
    {
        poweredReceptacles++;
    }

    public void DecreasePoweredReceptacles()
    {
        poweredReceptacles--;
    }
}
