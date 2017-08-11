using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadCollision : MonoBehaviour {

	public void CollideWithSnakeHead(Snake snake) {
		StopSnake(snake);
	}

	public void StopSnake(Snake snake) {
		if (snake == null)
			return;

		snake.direction = Snake.Direction.Neutral;
		Debug.Log("Collision!");
	}
}
