using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	private static InputManager _instance;
	public static InputManager instance {
		get {
			if (!_instance)
				Debug.LogError("No InputManager in this scene.");
			return _instance;
		}
	}

	public static bool initialized = false;

	private string[] buttons = {"Up", "Down", "Left", "Right", "Submit"};
	private Queue<string> buttonQueue;

	void Awake() {
		if (_instance == null) {
			_instance = this;
		}
		else if (_instance != this) {
			Destroy(gameObject);
			return;
		}

		buttonQueue = new Queue<string>();
		initialized = true;
	}

	void Update() {
		foreach (string button in buttons) {
			if (Input.GetButtonDown(button)) {
				buttonQueue.Enqueue(button);
//				Debug.Log("Enqueuing " + button);
			}
		}
	}

	public string GetNextButton() {
		string button = buttonQueue.Dequeue();
//		Debug.Log("Dequeuing " + button);
		return button;
	}

	public bool HasButtons() {
		if (buttonQueue.Count > 0) {
			return true;
		} 
		else {
			return false;
		}
	}
}