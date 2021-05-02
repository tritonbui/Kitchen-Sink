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
            isSwitchedOn = false;
        }
        else
        {
            StateA();
            isSwitchedOn = true;
        }
    }

    public void StateA()
    {
        TurnOnToggleAs();
        TurnOffToggleBs();
    }

    public void StateB()
    {
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
