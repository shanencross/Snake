using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadCollision : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D otherCollider) {
		if (otherCollider.gameObject.layer == gameObject.layer) {
			Snake snake = otherCollider.GetComponentInParent<Snake>();

			if (snake == null)
				return;

			snake.direction = Snake.Direction.Neutral;
			Debug.Log("Collision!");
		}
	}
}
