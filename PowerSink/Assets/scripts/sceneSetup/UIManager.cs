using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class UIManager : MonoBehaviour 
{
	public GameObject mainMenu;
	public GameObject gameMenu;
	public GameObject testLevelSelect;
	public GameObject levelSelect;
	public GameUI gameUI;
	public GameObject FirstLevel;
	public GameObject FirstTestLevel;
	public GameObject FirstMainMenu;
	public GameObject FirstGameMenu;
			
	public void OnGameMenu()
	{
		gameMenu.SetActive(true);
		mainMenu.SetActive(false);
		testLevelSelect.SetActive(false);
		levelSelect.SetActive(false);
		gameUI.gameObject.SetActive(false);

		EventSystem.current.SetSelectedGameObject(FirstGameMenu);

		Cursor.lockState = CursorLockMode.None;
       	Cursor.visible = true;
	}

	public void OnMainMenu()
	{
		gameMenu.SetActive(false);
		mainMenu.SetActive(true);
		testLevelSelect.SetActive(false);
		levelSelect.SetActive(false);
		gameUI.gameObject.SetActive(false);

		EventSystem.current.SetSelectedGameObject(FirstMainMenu);

		Cursor.lockState = CursorLockMode.None;
       	Cursor.visible = true;
	}

	public void OnTestLevelSelect()
	{
		gameMenu.SetActive(false);
		mainMenu.SetActive(false);
		testLevelSelect.SetActive(true);
		levelSelect.SetActive(false);
		gameUI.gameObject.SetActive(false);

		EventSystem.current.SetSelectedGameObject(FirstTestLevel);

		Cursor.lockState = CursorLockMode.None;
       	Cursor.visible = true;
	}

	public void OnLevelSelect()
	{
		gameMenu.SetActive(false);
		mainMenu.SetActive(false);
		testLevelSelect.SetActive(false);
		levelSelect.SetActive(true);
		gameUI.gameObject.SetActive(false);

		EventSystem.current.SetSelectedGameObject(FirstLevel);

		Cursor.lockState = CursorLockMode.None;
       	Cursor.visible = true;
	}

	public void OnGame()
	{
		gameMenu.SetActive(false);
		mainMenu.SetActive(false);
		testLevelSelect.SetActive(false);
		levelSelect.SetActive(false);
		gameUI.gameObject.SetActive(true);
		gameUI.putDownOrb();

		Cursor.lockState = CursorLockMode.Locked;
       	Cursor.visible = false;
	}
}