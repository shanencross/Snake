using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_alt4 : MonoBehaviour {

	[Range(0, 100)]
	public float movementFrequency = 15; // movements per second

	[Range(0, 100)]
	public float movementDistance = 1; // units traversed per movement

	public enum Direction {Up, Down, Left, Right, Neutral};

	public Direction direction = Direction.Neutral;

	[SerializeField]
	private int length = 0; // length of the snake in number of squares

	[SerializeField]
	private Vector2 distanceVector;

	public Transform head;
	public Transform tail;
	public List<Transform> body = new List<Transform>();

	[SerializeField]
	private Vector2 directionVector;

	void Awake() {
		updateDirectionAndDistanceVectors();

		foreach (Transform child in transform) {

			if (child.gameObject.activeSelf) {
				if (child.CompareTag("SnakeHead")) {
					head = child;
					length++;
				} 
				else if (child.CompareTag("SnakeTail")) {
					tail = child;
					length++;
				} 
				else if (child.CompareTag("SnakeBody")) {
					body.Add(child);
					length++;
				}
			}
		}

		if (head == null)
			Debug.LogError("Snake has no head");

		if (tail == null)
			Debug.Log("Snake has no tail");

		if (body.Count == 0)
			Debug.Log("Snake has no body parts.");
	}

	void Start() {
		float movementPeriod = 1 / movementFrequency;
		InvokeRepeating("RunSnakeLoop", 0, movementPeriod);
	}

	void RunSnakeLoop() {
		Debug.Log("Running snake loop");
		updateDirection();

		updateDirectionAndDistanceVectors();

		Move();
	}

	void updateDirectionAndDistanceVectors() {
		directionVector = getDirectionVector();
		distanceVector = directionVector * movementDistance;
	}

	private bool updateDirection() {
		bool directionChanged = false;

		if (InputManager.instance.HasButtons()) {
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

		if (directionChanged)
			Debug.Log("direction changed to " + direction);

		return directionChanged;
	}
		
	private void Move() {
		transform.Translate(distanceVector);
	}

	private Vector2 getDirectionVector() {
		Vector2 directionVector;

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
}
