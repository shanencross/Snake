using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPellet : MonoBehaviour {

	public int lengthIncrease;
	public float movementFrequencyIncrease;

	public GameObject foodSpawnerObject;

	public FoodSpawner foodSpawner;

//	void Awake() {
//		Initialize();
//	}

	public void Initialize(GameObject spawner = null, int lengthInc = 1, float moveFreqInc = 1) {
		lengthIncrease = lengthInc;
		movementFrequencyIncrease = moveFreqInc;
		foodSpawnerObject = spawner;

		if (lengthIncrease < 0)
			lengthIncrease = 0;

		if (movementFrequencyIncrease < 0)
			movementFrequencyIncrease = 0;

		if (foodSpawner == null) {
			foodSpawnerObject = GameObject.FindWithTag("FoodSpawner");
		}

		transform.parent = foodSpawnerObject.transform;
		foodSpawner = foodSpawnerObject.GetComponent<FoodSpawner>();
	}
		
	public void CollideWithSnakeHead(Snake snake) {
		snake.IncreaseLength(lengthIncrease);
		snake.increaseMovementFrequency(movementFrequencyIncrease);

		DestroyFoodPellet();
	}

	void DestroyFoodPellet() {

		if (foodSpawner == null) {
			Destroy(gameObject);
		} 
		else {
			foodSpawner.DestroyFoodPellet(transform);
		}

	}
}
