using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signalBlock : MonoBehaviour
{
    public Animator animator;
    public AudioSource levelFinishSound;
    public AudioSource doorOpenSound;
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
            levelFinishSound.Play();
            doorOpenSound.Play();
            animator.Play("signal_On", 0, 0f);
            GameManager._instance.isLevelFinished = true;
            levelTramDoor.SetActive(false);
            foreach (receptacleBlock blocks in receptacleBlocks)
            {
                blocks.canTakePowerOrb = false;
            }
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
