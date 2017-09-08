using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	private static MusicManager _instance;
	public static MusicManager instance { 
		get {
			if (!_instance)
				Debug.LogError("No Music Manager in scene.");
			return _instance;
		}
	}

	private AudioSource _audioSource;

	void Awake() {
		if (_instance == null) {
			_instance = this;
		} 
		else if (_instance != this) {
			Destroy(gameObject);
			return;
		}

		_audioSource = GetComponent<AudioSource>();
		if (_audioSource == null)
			Debug.LogError("No Audio Source component attached to Music Manager. Adding one.");
		_audioSource = gameObject.AddComponent<AudioSource>();

		DontDestroyOnLoad(gameObject);
	}
}
