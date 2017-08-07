using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPellet : MonoBehaviour {

	public int lengthIncreaseSize = 1;

	private FoodSpawner foodSpawner;

	void Awake() {
		GameObject foodSpawnerObject = GameObject.FindWithTag("FoodSpawner");
		transform.parent = foodSpawnerObject.transform;
		foodSpawner = foodSpawnerObject.GetComponent<FoodSpawner>();
	}

	void OnTriggerEnter2D(Collider2D otherCollider) {
		Debug.Log(otherCollider.gameObject.layer + ", " + gameObject.layer);
		if (otherCollider.gameObject.layer == gameObject.layer) {
			Snake snake = otherCollider.GetComponentInParent<Snake>();
			snake.ChangeLength(lengthIncreaseSize);

			DestroyFoodPellet();
		}
	}

	void DestroyFoodPellet() {
		foodSpawner.SpawnFood();
		Destroy(gameObject);
	}
}
