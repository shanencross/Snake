using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_alt3 : MonoBehaviour {

	[Range(0, 100)]
	public float speed  = 1;

	public enum Direction {Up, Down, Left, Right, Neutral};

	public Direction direction = Direction.Neutral;

	[SerializeField]
	private int length = 0;

	[SerializeField]
	private Vector2 speedVector;

	public Transform head;
	public Transform tail;
	public List<Transform> body = new List<Transform>();

	[SerializeField]
	private Vector2 unsnappedPosition;

	[SerializeField]
	private Vector2 directionVector;

	void Awake() {
		updateDirectionAndSpeedVectors();

		transform.position = SnapToGrid(transform.position);
		unsnappedPosition = transform.position;

		foreach (Transform child in transform) {

			if (child.gameObject.activeSelf) {
				if (child.CompareTag("SnakeHead")) {
					head = child;
					length++;
				} else if (child.CompareTag("SnakeTail")) {
					tail = child;
					length++;
				} else if (child.CompareTag("SnakeBody")) {
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

	void Update() {
		checkInput();

		updateDirectionAndSpeedVectors();

		if (transform.hasChanged)
			unsnappedPosition = SnapToGrid(transform.position);

		Move();

		transform.hasChanged = false;
	}

	void updateDirectionAndSpeedVectors() {
		directionVector = getDirectionVector();
		speedVector = directionVector * speed;
	}

	private bool checkInput() {
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
		Vector2 distanceVector = speedVector * Time.deltaTime;
		unsnappedPosition += distanceVector;
		transform.position = SnapToGrid(unsnappedPosition);
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

	private Vector2 SnapToGrid(Vector2 position) {
		Vector2 snappedPosition = new Vector2(SnapToGrid(position.x), SnapToGrid(position.y));
		return snappedPosition;
	}

	private float SnapToGrid(float num) {
		float snappedNum = Mathf.Sign(num) * (Mathf.Abs((int)(num)) + 0.5f);
		return snappedNum;
	}
}
