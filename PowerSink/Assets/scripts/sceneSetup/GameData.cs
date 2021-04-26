using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
	public int currentLevel; //current level when saved
	
	public GameData()
	{
		currentLevel = 1; // current level
	}

}
