using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadImpactDetector : MonoBehaviour {

	private BoxCollider2D _collider;
	private Snake _snake;

	void Awake() {
		_collider = GetComponent<BoxCollider2D>();

		if (transform.parent != null)
			_snake = transform.parent.GetComponentInParent<Snake>();

		if (_snake == null) {
			Debug.LogError("SnakeHeadImpact detector does not have a SnakeHead parent with a Snake parent with a Snake script attached.");
		} 

		if (_collider == null) {
			Debug.LogError("No BoxCollider2D component attached to SnakeHead.");
		}
	}


	void Update() {
		Vector2 headPosition = transform.parent.position;
		transform.position = headPosition + _snake.distanceVector;
	}

	void OnTriggerEnter2D(Collider2D otherCollider) {
		if (otherCollider.CompareTag("SnakeBody") || otherCollider.CompareTag("Wall")) {

			// temporary
			// should be game over scenario trigger
			_snake.movementFrequency = 0;
		}
	}
}
