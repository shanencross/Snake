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

	public Transform head;
	public Transform tail;
	public List<Transform> body = new List<Transform>();

	private Vector2 directionVector;
	private Vector2 distanceVector;

	private float timeCounter = 0;

	void Awake() {
		if (length < 1)
			length = 1;

		foreach (Transform child in transform) {
			if (child.gameObject.activeSelf && child.CompareTag("SnakeHead")) {
				head = child;
				tail = child;
				body.Add(head);
			}
		}

		if (head == null)
			Debug.LogError("Snake has no head");

		UpdateDirectionAndDistanceVectors();
		InitializeBody();
	}
		
	void Update() {
		timeCounter += Time.deltaTime;
		float movementPeriod = 1 / movementFrequency;
		if (timeCounter >= movementPeriod) {
			UpdateMovement();
			int multiplesOfPeriod = (int) (timeCounter / movementPeriod);
			timeCounter -= multiplesOfPeriod * movementPeriod;
		}
	}

	private void InitializeBody() {

		Vector2 headPosition = head.transform.position;
		Vector2 destination = headPosition + new Vector2(-movementDistance, 0);

		for (int i = 0; i < length - 1; i++) {
			GameObject snakeBody = (GameObject)Instantiate(snakeBodyPrefab, destination, Quaternion.identity);
			snakeBody.transform.SetParent(transform);
			body.Add(snakeBody.transform);

			Vector2 bodyPartPosition = snakeBody.transform.position;
			destination = bodyPartPosition + new Vector2(-movementDistance, 0);
		}

		tail = body[length - 1];
	}

	void UpdateMovement() {
		Debug.Log("Running movement update");
		UpdateDirection();
		UpdateDirectionAndDistanceVectors();
		Move();
	}

	private void Move() {
		MoveBody();
		MoveHead();
	}

	private void MoveBody() {
		if (direction == Direction.Neutral)
			return;
		
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

	private bool UpdateDirection() {
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

	private Vector2 GetDirectionVector() {
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

	void UpdateDirectionAndDistanceVectors() {
		directionVector = GetDirectionVector();
		distanceVector = directionVector * movementDistance;
	}

}