using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_alt : MonoBehaviour {

	public float movementDistance = 1; // distance units traveled per movement

	public float movementFrequency = 2; // movements per seconds

	private float timeAccumulated = 0; // time since the last movement would ideally have taken place
	private float timeSinceLastMovement = 0; // time since the last movement ACTUALLY took place

	public bool roundedMode = false;

	void Update () {
		//float positionIncrement = Mathf.Round(speed * Time.deltaTime);

		if (movementFrequency < 0)
			return;

		timeAccumulated += Time.deltaTime;

		float movementPeriod = 1 / movementFrequency;

		if (!roundedMode) {
			timeSinceLastMovement += Time.deltaTime;

			if (timeAccumulated >= movementPeriod) {
				float speed = movementDistance * movementFrequency; // distance per second
				float distance = speed * timeSinceLastMovement;
				transform.Translate(distance, 0, 0);

				int count = 0;
				int maxWhileLoopCount = 100;
				while (timeAccumulated >= movementPeriod) {
					timeAccumulated -= movementPeriod;
					Debug.Log("subtracted: " + timeAccumulated);
					count++;

					if (count > maxWhileLoopCount) {
						Debug.LogError("While loop has executed over " + maxWhileLoopCount + " time(s). Breaking.");
						break;
					}
				}
				timeSinceLastMovement = 0;
			} 
		}

		else if (roundedMode) {
			int count = 0;
			int maxWhileLoopCount = 100;
			while (timeAccumulated >= movementPeriod) {
				transform.Translate(movementDistance, 0, 0);
				timeAccumulated -= movementPeriod;
				count++;

				if (count > maxWhileLoopCount) {
					Debug.LogError("While loop has executed over " + maxWhileLoopCount + " time(s). Breaking.");
					break;
				}
			}
		}
	}
		
}
