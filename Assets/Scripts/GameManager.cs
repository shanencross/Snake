using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;
	public static GameManager instance {
		get {
			if (!_instance)
				Debug.LogError("No GameManager instance in scene.");
			return _instance;
		}
	}

	public int score = 0;
	public int highScore = 0;

	public bool gameIsOver = false;

	public GameObject snakeObject;
	private Snake _snake;

	public string nextSceneAfterGameOver = "Level";


	void Awake() {
		if (_instance == null) {
			_instance = this;
		} 
		else if (_instance != this) {
			Destroy(gameObject);
		}

		SetUp();
	}

	void SetUp() {
		if (score < 0)
			score = 0;

		if (highScore < 0)
			highScore = 0;

		if (snakeObject == null)
			Debug.LogError("Snake GameObject not set up on GameManager.");
		else
			_snake = snakeObject.GetComponent<Snake>();

		if (_snake == null)
			Debug.LogError("No Snake script attached to Snake object.");

		if (PlayerPrefs.HasKey("highScore")) {
			highScore = PlayerPrefs.GetInt("highScore");
		}

	}

	public void AddToScore(int amount) {
		score += amount;

		UpdateHighScore();
	}

	void UpdateHighScore() {
		if (score > highScore) {
			highScore = score;
			PlayerPrefs.SetInt("highScore", highScore);
		}
	}

	public void EndGame() {
		gameIsOver = true;
		MenuManager.instance.ActivateGameOverUI();
	}

	public void ResetGame() {
		score = 0;
		LoadScene(nextSceneAfterGameOver);
	}

	public void LoadScene(string sceneName) {
		SceneManager.LoadScene(sceneName);
	}


}

