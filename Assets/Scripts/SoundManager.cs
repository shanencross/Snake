using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	private static SoundManager _instance;
	public static SoundManager instance {
		get {
			if (!_instance)
				Debug.Log("No Sound Manager in scene.");
			return _instance;
		}
	}

	public AudioClip movementSFX;
	public float movementVolume = 1;

	public AudioClip eatingSFX;
	public float eatingVolume = 1;

	public AudioClip collisionSFX;
	public float collisionVolume = 1;

	public AudioClip menuSelectSFX;
	public float menuSelectVolume = 1;

	public AudioClip menuSubmitSFX;
	public float menuSubmitVolume = 1;

	public AudioClip directionChangeSFX;
	public float directionChangeVolume = 1;

	private AudioSource _audio;

	void Awake() {
		if (_instance == null) {
			_instance = this;
		} 
		else if (_instance != this) {
			Destroy(gameObject);
			return;
		}

		_audio = GetComponent<AudioSource>();

		if (_audio == null) {
			Debug.Log("No Audio Source component attached to Snake Game Object. Adding one.");
			_audio = gameObject.AddComponent<AudioSource>();
		}

		if (movementSFX == null)
			Debug.Log("Movement SFX not assigned on SoundEffects script component.");
		if (eatingSFX == null)
			Debug.Log("Eating SFX not assigned on SoundEffects script component.");
		if (collisionSFX == null)
			Debug.Log("Collision SFX not assigned on SoundEffects script component.");
		if (menuSelectSFX == null)
			Debug.Log("Menu Select SFX not assigned on SoundEffects script component.");
		if (menuSubmitSFX == null)
			Debug.Log("Menu Submit SFX not assigned on SoundEffects script component.");
		if (directionChangeSFX == null)
			Debug.Log("Direction Change SFX not assigned on SoundEffects script component.");

		DontDestroyOnLoad(gameObject);
	}
		
	public void PlaySound(string soundName) {
		if (soundName == "movement" && movementSFX != null)
			_audio.PlayOneShot(movementSFX, movementVolume);
		else if (soundName == "eating" && eatingSFX != null)
			_audio.PlayOneShot(eatingSFX, eatingVolume);
		else if (soundName == "collision" && collisionSFX != null)
			_audio.PlayOneShot(collisionSFX, collisionVolume);
		else if (soundName == "menuSelect" && menuSelectSFX != null)
			_audio.PlayOneShot(menuSelectSFX, menuSelectVolume);
		else if (soundName == "menuSubmit" && menuSubmitSFX != null)
			_audio.PlayOneShot(menuSubmitSFX, menuSelectVolume);
		else if (soundName == "directionChange" && directionChangeSFX != null)
			_audio.PlayOneShot(directionChangeSFX, directionChangeVolume);
		else
			Debug.LogError("No SFX named " + soundName + " set up in Sound Manager.");
	}

	public void PlaySound(AudioClip audioClip) {
		if (audioClip != null)
			_audio.PlayOneShot(audioClip);
	}
}
