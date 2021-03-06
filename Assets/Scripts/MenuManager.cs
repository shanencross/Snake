﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour {
	private static MenuManager _instance;
	public static MenuManager instance {
		get {
			if (!_instance)
				Debug.LogError("No MenuManager instance in scene.");
			return _instance;
		}
	}

	public GameObject gameOverPanel;
	public GameObject defaultButton;
	public string mainMenuScene = "Main Menu";

	[HideInInspector]
	public bool firstSelectionDone = false;


	public void Awake() {
		if (_instance == null) {
			_instance = this;
		}
		else if (_instance != this) {
			Destroy(gameObject);
			return;
		}

		SetUp();
	}

	void SetUp() {
		if (gameOverPanel == null)
			Debug.LogError("Game Over Panel GameObject not set up on Menu Manager.");
		else
			gameOverPanel.SetActive(false);

		if (defaultButton == null)
			Debug.LogError("Default Button GameObject not set up on Menu Manager.");
	}

	public void ActivateGameOverUI() {
		if (gameOverPanel != null)
			gameOverPanel.SetActive(true);
		EventSystem.current.SetSelectedGameObject(defaultButton);
		firstSelectionDone = true;
	}

	public void Retry() {
		GameManager.instance.ResetGame();
	}

	public void ReturnToMenu() {
		GameManager.instance.LoadScene(mainMenuScene);
	}
}
