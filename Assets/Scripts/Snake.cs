using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

	[Range(0, 100)]
	public float movementFrequency = 15; // movements per second

	[Range(0, 100)]
	public float movementDistance = 1; // units traversed per movement

	public enum Direction {Up, Down, Left, Right, Neutral};

	public Direction direction = Direction.Neutral;

	public int length = 1; // length of the snake in number of squares

	public GameObject snakeBodyPrefab;
	public LayerMask collisionLayer;

	public Transform head;
	public Transform tail;
	public List<Transform> body = new List<Transform>();

	[HideInInspector]
	public Vector2 directionVector;
	[HideInInspector]
	public Vector2 distanceVector;

	private float _timeCounter = 0;

	void Awake() {
		if (length < 1)
			length = 1;

		if (snakeBodyPrefab == null)
			Debug.LogError("Snake Body Prefab not assigned.");

		if (collisionLayer == null)
			Debug.LogError("Collision Layer not assigned.");

		InitializeHead();
		UpdateDirectionAndDistanceVectors();
		InitializeBody();

		// set tail reference to last body part in array
		tail = body[length - 1];
	}
		
	void Update() {
		_timeCounter += Time.deltaTime;
		float movementPeriod = 1 / movementFrequency;
		if (_timeCounter >= movementPeriod) {
			UpdateMovement();
			int multiplesOfPeriod = (int) (_timeCounter / movementPeriod);
			_timeCounter -= multiplesOfPeriod * movementPeriod;
		}
	}

	private void InitializeHead() {
		if (head == null || !head.CompareTag("SnakeHead")) {
			head = null;

			foreach (Transform child in transform) {
				if (child.gameObject.activeSelf && child.CompareTag("SnakeHead")) {
					head = child;
					break;
				}
			}
		}

		if (head == null)
			Debug.LogError("Snake has no head");
		else {
			body.Add(head);
		}
	}

	private void InitializeBody() {
		// Create and place snake body squares in positions relative to head position.
		// By default, place body squares one to the left of head.
		// Squares are separated from each other by the movementDistance unit.

		Vector2 headPosition = head.transform.position;
		Vector2 destination = headPosition + new Vector2(-movementDistance, 0);

		for (int i = 0; i < length - 1; i++) {
			GameObject snakeBody = (GameObject)Instantiate(snakeBodyPrefab, destination, Quaternion.identity);
			snakeBody.transform.SetParent(transform);
			body.Add(snakeBody.transform);

			Vector2 bodyPartPosition = snakeBody.transform.position;
			destination = bodyPartPosition + new Vector2(-movementDistance, 0);
		}
	}

	void UpdateMovement() {
		UpdateDirection();
		Move();
	}

	private void Move() {
		// Don't move anything if the snake is not moving in a direction.
		if (direction == Direction.Neutral)
			return;

		bool impactCheck = CheckForImpact();
		if (impactCheck)
			return;

		MoveBody();
		MoveHead();
	}

	private void MoveBody() {
		Vector2 destination = head.transform.position;

		foreach (Transform bodyPart in body) {
			Vector2 oldPosition = bodyPart.position;
			bodyPart.position = destination;
			destination = oldPosition;
		}
	}

	private void MoveHead() {
		head.transform.Translate(distanceVector);
	}

	private bool CheckForImpact() {
		Vector2 headPosition = head.transform.position;
		Vector2 headDestination = headPosition + distanceVector;

		bool impact = Physics2D.BoxCast(headPosition, movementDistance * Vector2.one, 0f, directionVector, movementDistance, collisionLayer);

		return impact;
	}

	private bool UpdateDirection() {
		// Set the next movement direction by getting the next button input
		// from the input queue.
		// Return whether or not the direction was updated.

		bool directionChanged = false;

		if (InputManager.initialized && InputManager.instance.HasButtons()) {
			string button = InputManager.instance.GetNextButton();

			if (button == "Down" && (length == 1 || direction != Direction.Up)) {
				direction = Direction.Down;
			}
			if (button == "Up" && (length == 1 || direction != Direction.Down)) {
				direction = Direction.Up;
			}
			if (button == "Left" && (length == 1 || direction != Direction.Right)) {
				direction = Direction.Left;
			}
			if (button == "Right" && (length == 1 || direction != Direction.Left)) {
				direction = Direction.Right;
			}

			directionChanged = true;
		}

		if (directionChanged) {
			Debug.Log("direction changed to " + direction);
			UpdateDirectionAndDistanceVectors();
		}

		return directionChanged;
	}

	private void UpdateDirectionAndDistanceVectors() {
		UpdateDirectionVector();
		UpdateDistanceVector();
	}

	private Vector2 UpdateDirectionVector() {
		if (direction == Direction.Up)
			directionVector = new Vector2(0, 1);
		else if (direction == Direction.Down)
			directionVector = new Vector2(0, -1);
		else if (direction == Direction.Left)
			directionVector = new Vector2(-1, 0);
		else if (direction == Direction.Right)
			directionVector = new Vector2(1, 0);
		else { // if direction == Direction.Neutral
			directionVector = new Vector2(0, 0);
		}

		return directionVector;
	}

	void UpdateDistanceVector() {
		distanceVector = directionVector * movementDistance;
	}

}