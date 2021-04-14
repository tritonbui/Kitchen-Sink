using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleSwitch : MonoBehaviour
{
    public bool isSwitchedOn = true;
    public GameObject[] toggleABlocks; //prefab
    public GameObject[] toggleBBlocks;

    public void Start()
    {
        toggleABlocks = GameObject.FindGameObjectsWithTag("toggleA");
        toggleBBlocks = GameObject.FindGameObjectsWithTag("toggleB");

        foreach (GameObject toggleABlock in toggleABlocks)
        {
            toggleABlock.SetActive(true);
        }

        foreach (GameObject toggleBBlock in toggleBBlocks)
        {
            toggleBBlock.SetActive(false);
        }
    }

    public void Toggle()
    {
        if (isSwitchedOn)
        {
            TurnOffToggleAs();
            TurnOnToggleBs();
            isSwitchedOn = false;
        }
        else
        {
            TurnOnToggleAs();
            TurnOffToggleBs();
            isSwitchedOn = true;
        }
    }

    public void TurnOnToggleAs()
    {
        foreach (GameObject toggleABlock in toggleABlocks)
        {
            toggleABlock.SetActive(true);
        }
    }

    public void TurnOffToggleAs()
    {
        foreach (GameObject toggleABlock in toggleABlocks)
        {
            toggleABlock.SetActive(false);
        }
    }

    public void TurnOnToggleBs()
    {
        foreach (GameObject toggleBBlock in toggleBBlocks)
        {
            toggleBBlock.SetActive(true);
        }
    }

    public void TurnOffToggleBs()
    {
        foreach (GameObject toggleBBlock in toggleBBlocks)
        {
            toggleBBlock.SetActive(false);
        }
    }
}
