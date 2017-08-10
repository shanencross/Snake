using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CollisionCheck : MonoBehaviour
{
	public List<Transform> walls = new List<Transform>();
	public Transform FoodSpawner;

	void Awake()
	{
		if (walls.Count == 0) {
			Debug.LogError("No wall transforms set for collision check script");
		}

		if (FoodSpawner == null) {
			Debug.LogError("No Food Spawner Transform set.");
		}
	}


	public void CheckCollision(Snake snake) {
		CheckWallCollision(snake);
		CheckSnakeBodyCollision(snake);
		CheckFoodPelletCollision(snake);
	}

	void CheckWallCollision(Snake snake) {
		foreach (Transform wall in walls) {
			bool overlap = Physics2D.OverlapBox(snake.head.transform.position, new Vector2(1, 1), 0f, LayerMask.GetMask("Wall"));
			Debug.Log("overlap: " + overlap);
			if (overlap) {
				SnakeHeadCollision snakeHeadCollision = wall.GetComponent<SnakeHeadCollision>();
				if (snakeHeadCollision != null) {
					snakeHeadCollision.StopSnake(snake);
				}
			}
		}
	}

	void CheckSnakeBodyCollision(Snake snake) {
		foreach (Transform bodyPart in snake.body) {
			if (snake.head.transform.position == bodyPart.transform.position) {
				SnakeHeadCollision snakeHeadCollision = bodyPart.GetComponent<SnakeHeadCollision>();
				if (snakeHeadCollision != null) {
					snakeHeadCollision.StopSnake(snake);
				}
			}

		}
	}

	void CheckFoodPelletCollision(Snake snake) {
		foreach (Transform food in FoodSpawner) {
			if (food.CompareTag("Food")) {
				if (snake.head.transform.position == food.transform.position) {
					FoodPellet foodPellet = food.GetComponent<FoodPellet>();
					if (foodPellet != null) {
						foodPellet.CollideWithSnakeHead(snake);
					}

				}
			}
		}
		
	}
}

