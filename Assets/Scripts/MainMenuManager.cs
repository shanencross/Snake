using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	private static MainMenuManager _instance;
	public static MainMenuManager instance {
		get {
			if (!_instance)
				Debug.LogError("No Main Menu Manager in scene.");
			
			return _instance;
		}
	}

	public string sceneToLoad;

	void Awake() {
		if (_instance == null) {
			_instance = this;
		}
		else if (_instance != this) {
			Destroy(gameObject);
			return;
		}
			
		if (sceneToLoad == null)
			Debug.LogError("Scene To Load is not set on Game Manager.");
	}

	void Update() {

		if (Input.GetButtonDown("Submit")) {
			SoundManager.instance.PlaySound("menuSubmit");
			SceneManager.LoadScene(sceneToLoad);
		}
	}
}
