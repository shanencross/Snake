using System.Collections;
using UnityEngine;

public class SnakeHeadCollision : MonoBehaviour {

	public void CollideWithSnakeHead(Snake snake) {
		StopSnake(snake);
		SoundManager.instance.PlaySound("collision");

		if (GameManager.instance) {
			GameManager.instance.EndGame();
		}
	}

	public void StopSnake(Snake snake) {
		if (snake == null)
			return;

		snake.direction = Snake.Direction.Neutral;
		Debug.Log("Collision!");
	}
}
