using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameUI : MonoBehaviour 
{
	public TextMeshProUGUI levelText;
	public GameObject interact2;
	public GameObject interacta;
	public GameObject interactb;
	public GameObject powerOrbImage;

	// Use this for initialization
	void Start () 
	{
		powerOrbImage.SetActive(false);
		interact2.SetActive(false);
		interacta.SetActive(false);
		interactb.SetActive(false);
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

	public void lookAtSwitch()
	{
		interact2.SetActive(true);
		interacta.SetActive(false);
		interactb.SetActive(false);
	}

	public void lookAtOrb()
	{
		
		interact2.SetActive(false);
		interacta.SetActive(true);
		interactb.SetActive(false);
	}

	public void lookAtReceptacle()
	{
		interact2.SetActive(false);
		interacta.SetActive(false);
		interactb.SetActive(true);
	}

	public void lookAtNothing()
	{
		interact2.SetActive(false);
		interacta.SetActive(false);
		interactb.SetActive(false);
	}
}
