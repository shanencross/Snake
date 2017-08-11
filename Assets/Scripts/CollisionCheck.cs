using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CollisionCheck : MonoBehaviour
{
	public List<Transform> walls = new List<Transform>();
	public Transform foodSpawnerTransform;

	private FoodSpawner foodSpawner;
	void Awake()
	{
		if (walls.Count == 0) {
			Debug.LogError("No wall transforms set for collision check script");
		}

		if (foodSpawnerTransform == null) {
			Debug.LogError("No Food Spawner Transform set.");
		}
		else {
			foodSpawner = foodSpawnerTransform.GetComponent<FoodSpawner>();
		}

		if (foodSpawner == null) {
			Debug.LogError("No Food Spawner Script Component attached to Food Spawner.");
		}
	}
		
	public void CheckCollisions(Snake snake) {
		CheckWallCollision(snake);
		CheckSnakeBodyCollision(snake);
		CheckFoodPelletCollision(snake);
	}
		
	void CheckWallCollision(Snake snake) {
		CheckCollision(snake, walls, "Wall");
	}

	void CheckSnakeBodyCollision(Snake snake) {
		CheckCollision(snake, snake.body, "SnakeBody");
	}

	void CheckFoodPelletCollision(Snake snake) {
		if (foodSpawner != null) {
			CheckCollision(snake, foodSpawner.foodPellets, "Food");

			foodSpawner.UpdateFoodPellets();
		}
	}

	void CheckCollision(Snake snake, List<Transform> colliderTransforms, string tag) {
		foreach (Transform colliderTransform in colliderTransforms) {
			if (colliderTransform.CompareTag(tag)) {
				if (isColliding(snake, colliderTransform.gameObject)) {
					colliderTransform.SendMessage("CollideWithSnakeHead", snake);
				}
			}
		}
	}

	bool isColliding(Snake snake, GameObject collidingObject) {
		int layer = collidingObject.layer;
		string layerName = LayerMask.LayerToName(layer);
		int layerMask = LayerMask.GetMask(layerName); 

		bool colliding = false;

		Collider2D layerOverlap = Physics2D.OverlapBox(snake.head.transform.position, Vector2.one, 0, layerMask);

		if (layerOverlap) {
			if (GameObject.ReferenceEquals(layerOverlap.gameObject, collidingObject))
				colliding = true;
		}

		return colliding;
	}
}

