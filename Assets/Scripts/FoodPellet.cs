using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPellet : MonoBehaviour {

	public int lengthIncreaseSize = 1;

	void OnTriggerEnter2D(Collider2D otherCollider) {
		if (otherCollider.CompareTag("SnakeHead")) {
			Snake snake = otherCollider.GetComponentInParent<Snake>();
			snake.ChangeLength(lengthIncreaseSize);
		}
	}
}
