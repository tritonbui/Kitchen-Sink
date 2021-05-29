using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleSwitch : MonoBehaviour
{
    public Animator animator;
    public bool isSwitchedOn = true;
    public GameObject[] toggleABlocks; 
    public GameObject[] toggleBBlocks; 

    public void Start()
    {
        toggleABlocks = GameObject.FindGameObjectsWithTag("toggleA"); //finds all toggle A Blocks
        toggleBBlocks = GameObject.FindGameObjectsWithTag("toggleB"); //finds all toggle B blocks

        foreach (GameObject toggleABlock in toggleABlocks)
        {
            //defaults toggle A Blocks to being activated
            toggleABlock.GetComponent<BoxCollider>().enabled = true;
            toggleABlock.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        }

        foreach (GameObject toggleBBlock in toggleBBlocks)
        {
            //defaults toggle B Blocks to being deactivated
            toggleBBlock.GetComponent<BoxCollider>().enabled = false;
            toggleBBlock.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }
    }

    public void Toggle()
    {
        if (isSwitchedOn)
        {
            StateB(); //turns A blocks on, and B blocks off
        }
        else
        {
            StateA(); //turns A blocks off, and B blocks on
        }
    }

    public void StateA()
    {
        animator.Play("toggle_On", 0, 0f);
        isSwitchedOn = true;
        TurnOnToggleAs();
        TurnOffToggleBs();
    }

    public void StateB()
    {
        animator.Play("toggle_Off", 0, 0f);
        isSwitchedOn = false;
        TurnOffToggleAs();
        TurnOnToggleBs();
    }

    public void TurnOnToggleAs() //self explanatory from here
    {
        foreach (GameObject toggleABlock in toggleABlocks)
        {
            toggleABlock.GetComponent<BoxCollider>().enabled = true;
            toggleABlock.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        }
    }

    public void TurnOffToggleAs()
    {
        foreach (GameObject toggleABlock in toggleABlocks)
        {
            toggleABlock.GetComponent<BoxCollider>().enabled = false;
            toggleABlock.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }
    }

    public void TurnOnToggleBs()
    {
        foreach (GameObject toggleBBlock in toggleBBlocks)
        {
            toggleBBlock.GetComponent<BoxCollider>().enabled = true;
            toggleBBlock.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        }
    }

    public void TurnOffToggleBs()
    {
        foreach (GameObject toggleBBlock in toggleBBlocks)
        {
            toggleBBlock.GetComponent<BoxCollider>().enabled = false;
            toggleBBlock.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }
    }
}
