using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	private MainMenuManager _instance;
	public MainMenuManager instance {
		get {
			if (!_instance)
				Debug.LogError("No Main Menu Manager in scene.");
			
			return _instance;
		}
	}

	public string sceneToLoad;

	void Awake() {
		if (sceneToLoad == null)
			Debug.LogError("Scene To Load is not set on Game Manager.");
	}

	void Update() {

		if (Input.GetButtonDown("Submit")) {
			SceneManager.LoadScene(sceneToLoad);
		}
	}
}
