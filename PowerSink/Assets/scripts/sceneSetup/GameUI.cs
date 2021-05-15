using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameUI : MonoBehaviour 
{
	public TextMeshProUGUI levelText;
	public GameObject switchKM;
	public GameObject pickUpKM;
	public GameObject putDownKM;
	public GameObject switchGP;
	public GameObject pickUpGP;
	public GameObject putDownGP;
	public GameObject powerOrbImage;

	// Use this for initialization
	void Start () 
	{
		powerOrbImage.SetActive(false);
		switchKM.SetActive(false);
		pickUpKM.SetActive(false);
		putDownKM.SetActive(false);
	}
	
	public void SetGameUI(int level)
	{
		levelText.text = "Level: " + level;
	}

	public void pickUpOrb()
	{
		powerOrbImage.SetActive(true);
	}

	public void putDownOrb()
	{
		powerOrbImage.SetActive(false);
	}

	public void lookAtSwitchKM()
	{
		switchKM.SetActive(true);
		pickUpKM.SetActive(false);
		putDownKM.SetActive(false);
		switchGP.SetActive(false);
		pickUpGP.SetActive(false);
		putDownGP.SetActive(false);
	}

	public void lookAtSwitchGP()
	{
		switchKM.SetActive(false);
		pickUpKM.SetActive(false);
		putDownKM.SetActive(false);
		switchGP.SetActive(true);
		pickUpGP.SetActive(false);
		putDownGP.SetActive(false);
	}

	public void lookAtOrbKM()
	{
		switchKM.SetActive(false);
		pickUpKM.SetActive(true);
		putDownKM.SetActive(false);
		switchGP.SetActive(false);
		pickUpGP.SetActive(false);
		putDownGP.SetActive(false);
	}

	public void lookAtOrbGP()
	{
		switchKM.SetActive(false);
		pickUpKM.SetActive(false);
		putDownKM.SetActive(false);
		switchGP.SetActive(false);
		pickUpGP.SetActive(true);
		putDownGP.SetActive(false);
	}

	public void lookAtReceptacleKM()
	{
		switchKM.SetActive(false);
		pickUpKM.SetActive(false);
		putDownKM.SetActive(true);
		switchGP.SetActive(false);
		pickUpGP.SetActive(false);
		putDownGP.SetActive(false);
	}

	public void lookAtReceptacleGP()
	{
		switchKM.SetActive(false);
		pickUpKM.SetActive(false);
		putDownKM.SetActive(false);
		switchGP.SetActive(false);
		pickUpGP.SetActive(false);
		putDownGP.SetActive(true);
	}

	public void lookAtNothing()
	{
		switchKM.SetActive(false);
		pickUpKM.SetActive(false);
		putDownKM.SetActive(false);
		switchGP.SetActive(false);
		pickUpGP.SetActive(false);
		putDownGP.SetActive(false);
	}
}
