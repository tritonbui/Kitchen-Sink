using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO; // The System.IO namespace contains functions related to loading and saving files

public class GameManager : MonoBehaviour 
{
	public static GameManager _instance;
	public GameUI gameUI;
	public levelChanger levelChanger;
	public UIManager uiManager;
	private GameData _gd;

	public string gameSceneName;
	public string menuSceneName;
	public string saveFileName = "data.json";
	public int currentLevel;
	public int powerOrb;

	private bool _playingGame;
	public bool isLevelFinished = false;
	public bool isPlayerDead = false;
	public bool isPaused = false;

	public static GameManager instance
	{
		get
		{
			return _instance;
		}
	} 

	public GameData gameData
	{
		get
		{
			return _gd;
		}
	}

	void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			if (_instance != this)
			Destroy(gameObject);
		}
	}
	
	void Start()
	{
		StartLoadMainMenu();
	}
	
	//new game start
	public void NewGame()
	{
		_gd = new GameData();
		LoadGameScene();
	}
		
	//Toggles between Game Menu and In Game
	public void ToggleGameMenu()
	{
		if (!isPlayerDead)
		{
			if(uiManager.gameMenu.activeInHierarchy)
			{
				uiManager.OnGame();
			}
			else
			{
				uiManager.OnGameMenu();
			}
		}
	}
	
	public void TogglePause() //time pausing function (for game menu)
	{
		if (Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
			isPaused = false;
		}
		else
		{
			Time.timeScale = 0f;
			isPaused = true;
		}
	}
		
	//Load Menu Scene
	public void LoadMainMenu()
	{
		levelChanger.FadeToLevel("mainMenu");
		Time.timeScale = 1f;
	}

	public void MainMenuLoad()
	{
		uiManager.OnMainMenu();
		isPaused = false;
	}

	public void LevelLoad()
	{
		uiManager.OnGame();
		isLevelFinished = false;
		isPaused = false;
	}

	public void FinishToMainMenu()
	{
		levelChanger.FadeToLevel("mainMenu");
		Time.timeScale = 1f;
	}

	public void LoadLevelOne()
	{
		levelChanger.FadeToLevel("levelOne");
	}

	public void LoadLevelTwo()
	{
		levelChanger.FadeToLevel("levelTwo");
	}

	public void LoadLevelThree()
	{
		levelChanger.FadeToLevel("levelThree");
	}

	public void LoadLevelFour()
	{
		levelChanger.FadeToLevel("levelFour");
	}

	public void LoadLevelFive()
	{
		levelChanger.FadeToLevel("levelFive");
	}

	public void LoadLevelSix()
	{
		levelChanger.FadeToLevel("levelSix");
	}

	public void TestLevels()
	{
		uiManager.OnTestLevelSelect();
	}

	public void LevelSelect()
	{
		levelChanger.FadeToLevel("levelSelect");
		Time.timeScale = 1f;
	}
	
	public void LoadLukeLevel()
	{
		levelChanger.FadeToLevel("lukeLevel");
	}

	public void LoadRhysLevel()
	{
		levelChanger.FadeToLevel("rhysLevel");
	}

	public void LoadTristonLevel()
	{
		levelChanger.FadeToLevel("tristonLevel");
	}

	public void LoadTashLevel()
	{
		levelChanger.FadeToLevel("tashLevel");
	}

	public void LoadZharaLevel()
	{
		levelChanger.FadeToLevel("zharaLevel");
	}

	public void ReturnToMainMenu()
	{
		uiManager.OnMainMenu();
	}

	public void StartLoadMainMenu()
	{
		SceneManager.LoadScene(0);
		uiManager.OnMainMenu();
	}

	public void ResumeGame()
	{
		ToggleGameMenu();
		TogglePause();
	}
	//Loads Game Scene
	public void LoadGameScene()
	{
		SceneManager.LoadScene(_gd.currentLevel);
		uiManager.OnGame();
	}
	//Quits Game
	public void QuitGame () //works in both Unity Editor & Application
   	{
       	/*
		if (UnityEditor.EditorApplication.isPlaying)
       	{
           	UnityEditor.EditorApplication.isPlaying = false;
       	}*/
        	Application.Quit();
    }
	
	//Below is unused, but planned saving and starting functionality.

	/*
	void StartGameAt(GameData g)
	{
		_gd = g; //set the game data
		Debug.Log("Would start game here at " + gameData.currentLevel);
		LoadGameScene();
	}

	public void LoadGameData()
	{
		string filePath = Path.Combine(Application.streamingAssetsPath, saveFileName);
		Debug.Log(filePath);
		if(File.Exists(filePath))
		{
			string dataAsJson = File.ReadAllText(filePath); 
			GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);

			StartGameAt(loadedData);
		}
		else
		{
			Debug.Log("Cannot load game data!");
		}
    
	}

	public void SaveGameData()
	{
		Debug.Log("Save");
		GameData gd = _gd;
		string dataAsJson = JsonUtility.ToJson (gd);
		Debug.Log(dataAsJson);
		string filePath = Application.streamingAssetsPath + "/" +  saveFileName;
		File.WriteAllText (filePath, dataAsJson);
	}*/
}