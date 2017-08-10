using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadCollision : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D otherCollider) {
//		if (otherCollider.gameObject.layer == LayerMask.NameToLayer("SnakeHead")) {
//			Snake snake = otherCollider.GetComponentInParent<Snake>();
//
//			StopSnake(snake);
//		}
	}

	public void StopSnake(Snake snake) {
		if (snake == null)
			return;

		snake.direction = Snake.Direction.Neutral;
		Debug.Log("Collision!");
	}
}
