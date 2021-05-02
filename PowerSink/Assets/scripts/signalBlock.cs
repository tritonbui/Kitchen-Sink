using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signalBlock : MonoBehaviour
{
    public Animator animator;
    public GameObject levelTramDoor;
    public GameObject[] receptacles; //prefab
    public List<receptacleBlock> receptacleBlocks;
    public int receptacleNo;
    public int poweredReceptacles = 0;
    public bool isLevelFinished = false;

    public void Start()
    {
        ReceptacleStuff();
    }

    public void Update()
    {
        LevelFinish();
    }

    public void ReceptacleStuff()
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

    public void LevelFinish()
    {
        if (receptacleNo == poweredReceptacles && !isLevelFinished)
        {
            isLevelFinished = true;
            animator.SetTrigger("Signal On");
            GameManager._instance.isLevelFinished = true;
            levelTramDoor.SetActive(false);
            foreach (receptacleBlock blocks in receptacleBlocks)
            {
                blocks.canTakePowerOrb = false;
            }
        }
        else
        {
            isLevelFinished = false;
            GameManager._instance.isLevelFinished = false;
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
