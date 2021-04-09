using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



	public class UIManager : MonoBehaviour 
	{
		public GameObject mainMenu;
		public GameObject gameMenu;
		public GameUI gameUI;
				
		public void OnGameMenu()
		{
			gameMenu.SetActive(true);
			mainMenu.SetActive(false);
			gameUI.gameObject.SetActive(false);

			Cursor.lockState = CursorLockMode.None;
        	Cursor.visible = true;
		}

		public void OnMainMenu()
		{
			gameMenu.SetActive(false);
			mainMenu.SetActive(true);
			gameUI.gameObject.SetActive(false);

			Cursor.lockState = CursorLockMode.None;
        	Cursor.visible = true;
		}

		public void OnGame()
		{
			gameMenu.SetActive(false);
			mainMenu.SetActive(false);
			gameUI.gameObject.SetActive(true);
			gameUI.putDownOrb();

			Cursor.lockState = CursorLockMode.Locked;
        	Cursor.visible = false;
		}

		public void Dead()
		{
			gameMenu.SetActive(false);
			mainMenu.SetActive(false);
			gameUI.gameObject.SetActive(false);

			Cursor.lockState = CursorLockMode.None;
        	Cursor.visible = true;
		}
	}

