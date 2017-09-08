using System.Collections;
using UnityEngine;

public class FoodPellet : MonoBehaviour {

	public int points = 1;
	public int lengthIncrease = 1;
	public float movementFrequencyIncrease = 0;

	public GameObject foodSpawnerObject;

	public FoodSpawner foodSpawner;

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
		SoundManager.instance.PlaySound("eating");

		if (GameManager.instance) {
			GameManager.instance.AddToScore(points);
		}

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
