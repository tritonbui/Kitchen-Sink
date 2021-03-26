using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameUI : MonoBehaviour 
{
	public TextMeshProUGUI levelText;
	public TextMeshProUGUI powerOrbText;
	public GameObject powerOrbImage;

	// Use this for initialization
	void Start () 
	{
		powerOrbImage.SetActive(false);
	}
	
	public void SetGameUI(int level)
	{
		levelText.text = "Level: " + level;
	}

	public void ToggleOrb()
	{
		if(powerOrbImage.activeInHierarchy)
		{
			powerOrbImage.SetActive(false);
		}
		else
		{
			powerOrbImage.SetActive(true);
		}
	}
}
