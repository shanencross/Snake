using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPellet : MonoBehaviour {

	public int lengthIncrease = 1;
	public float movementFrequencyIncrease = 0;

	private FoodSpawner foodSpawner;

	void Awake() {
		if (lengthIncrease < 0)
			lengthIncrease = 0;

		if (movementFrequencyIncrease < 0)
			movementFrequencyIncrease = 0;

		GameObject foodSpawnerObject = GameObject.FindWithTag("FoodSpawner");
		transform.parent = foodSpawnerObject.transform;
		foodSpawner = foodSpawnerObject.GetComponent<FoodSpawner>();
	}

	void OnTriggerEnter2D(Collider2D otherCollider) {
//		if (otherCollider.gameObject.layer == LayerMask.NameToLayer("SnakeHead")) {
//			Snake snake = otherCollider.GetComponentInParent<Snake>();
//			CollideWithSnakeHead(snake);	
//		}
	}

	public void CollideWithSnakeHead(Snake snake) {

		snake.IncreaseLength(lengthIncrease);
		snake.increaseMovementFrequency(movementFrequencyIncrease);

		DestroyFoodPellet();
	}

	void DestroyFoodPellet() {
		foodSpawner.SpawnFood();
		Destroy(gameObject);
	}
}
