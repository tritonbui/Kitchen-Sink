using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleSwitch : MonoBehaviour
{
    public Animator animator;
    public bool isSwitchedOn = true;
    public GameObject[] toggleABlocks; //prefab
    public GameObject[] toggleBBlocks;

    public void Start()
    {
        toggleABlocks = GameObject.FindGameObjectsWithTag("toggleA");
        toggleBBlocks = GameObject.FindGameObjectsWithTag("toggleB");

        foreach (GameObject toggleABlock in toggleABlocks)
        {
            toggleABlock.GetComponent<BoxCollider>().enabled = true;
            toggleABlock.transform.GetChild(0).gameObject.SetActive(true);
            toggleABlock.transform.GetChild(1).gameObject.SetActive(false);
        }

        foreach (GameObject toggleBBlock in toggleBBlocks)
        {
            toggleBBlock.GetComponent<BoxCollider>().enabled = false;
            toggleBBlock.transform.GetChild(0).gameObject.SetActive(false);
            toggleBBlock.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void Toggle()
    {
        if (isSwitchedOn)
        {
            StateB();
        }
        else
        {
            StateA();
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

    public void TurnOnToggleAs()
    {
        foreach (GameObject toggleABlock in toggleABlocks)
        {
            toggleABlock.GetComponent<BoxCollider>().enabled = true;
            toggleABlock.transform.GetChild(0).gameObject.SetActive(true);
            toggleABlock.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void TurnOffToggleAs()
    {
        foreach (GameObject toggleABlock in toggleABlocks)
        {
            toggleABlock.GetComponent<BoxCollider>().enabled = false;
            toggleABlock.transform.GetChild(0).gameObject.SetActive(false);
            toggleABlock.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void TurnOnToggleBs()
    {
        foreach (GameObject toggleBBlock in toggleBBlocks)
        {
            toggleBBlock.GetComponent<BoxCollider>().enabled = true;
            toggleBBlock.transform.GetChild(0).gameObject.SetActive(true);
            toggleBBlock.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void TurnOffToggleBs()
    {
        foreach (GameObject toggleBBlock in toggleBBlocks)
        {
            toggleBBlock.GetComponent<BoxCollider>().enabled = false;
            toggleBBlock.transform.GetChild(0).gameObject.SetActive(false);
            toggleBBlock.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
