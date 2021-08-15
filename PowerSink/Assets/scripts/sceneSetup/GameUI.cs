using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameUI : MonoBehaviour 
{
	public GameObject teleporterKM;
	public GameObject switchKM;
	public GameObject pickUpKM;
	public GameObject putDownKM;
	public GameObject teleporterGP;
	public GameObject switchGP;
	public GameObject pickUpGP;
	public GameObject putDownGP;

	// Use this for initialization
	void Start () 
	{
		lookAtNothing();
	}


	public void onTeleporterKM()
	{
		lookAtNothing();
		teleporterKM.SetActive(true);
	}

	public void onTeleporterGP()
	{
		lookAtNothing();
		teleporterGP.SetActive(true);
	}

	public void lookAtSwitchKM()
	{
		lookAtNothing();
		switchKM.SetActive(true);
	}

	public void lookAtSwitchGP()
	{
		lookAtNothing();
		switchGP.SetActive(true);
	}

	public void lookAtOrbKM()
	{
		lookAtNothing();
		pickUpKM.SetActive(true);
	}

	public void lookAtOrbGP()
	{
		lookAtNothing();
		pickUpGP.SetActive(true);
	}

	public void lookAtReceptacleKM()
	{
		lookAtNothing();
		putDownKM.SetActive(true);
	}

	public void lookAtReceptacleGP()
	{
		lookAtNothing();
		putDownGP.SetActive(true);
	}

	public void lookAtNothing()
	{
		teleporterKM.SetActive(false);
		switchKM.SetActive(false);
		pickUpKM.SetActive(false);
		putDownKM.SetActive(false);
		teleporterGP.SetActive(false);
		switchGP.SetActive(false);
		pickUpGP.SetActive(false);
		putDownGP.SetActive(false);
	}
}
