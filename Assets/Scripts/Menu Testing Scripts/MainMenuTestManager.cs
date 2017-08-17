using UnityEngine;
using System.Collections;
using UnityEngine.UI; // include UI namespace since references UI Buttons directly
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems; // include EventSystems namespace so can set initial input for controller support

public class MainMenuTestManager : MonoBehaviour {

	public int startLives=3; // how many lives to start the game with on New Game

	// references to Submenus
	public GameObject _MainMenu;
//	public GameObject _AboutMenu;

	// references to Button GameObjects
	public GameObject MenuDefaultButton;
//	public GameObject AboutDefaultButton;

	// store the initial title so we can set it back
	private string _mainTitle;

	// init the menu
	void Awake()
	{
		// Show the proper menu
		ShowMenu("MainMenu");
	}

	// Public functions below that are available via the UI Event Triggers, such as on Buttons.

	// Show the proper menu
	public void ShowMenu(string name)
	{
		// turn all menus off
		_MainMenu.SetActive (false);
//		_AboutMenu.SetActive(false);

		// turn on desired menu and set default selected button for controller input
		switch(name) {
		case "MainMenu":
			_MainMenu.SetActive (true);
			EventSystem.current.SetSelectedGameObject (MenuDefaultButton);
			break;
//		case "About":
//			_AboutMenu.SetActive(true);
//			EventSystem.current.SetSelectedGameObject (AboutDefaultButton);
//			break;
		}
	}

	// load the specified Unity level
	public void loadLevel(string levelToLoad)
	{
		Debug.Log("Loading Level " + levelToLoad);
	}
		
}
