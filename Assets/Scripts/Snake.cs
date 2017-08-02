using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

	[Range(0, 100)]
	public float speed  = 1;

	public enum Direction {Up, Down, Left, Right};

	public Direction direction = Direction.Right;

	[SerializeField]
	private Vector2 speedVector;

	public Transform head;
	public Transform tail;
	public List<Transform> body = new List<Transform>();

	[SerializeField]
	private Vector2 unsnappedPosition;

	[SerializeField]
	private Vector2 directionVector;

	private bool verticalAxisPressed = false;
	private bool horizontalAxisPressed = false;

	void Awake() {
		directionVector = getDirectionVector();
		speedVector = directionVector * speed;

		transform.position = SnapToGrid(transform.position);
		unsnappedPosition = transform.position;

		foreach (Transform child in transform) {
			if (child.CompareTag("SnakeHead")) {
				head = child;
			}
			else if (child.CompareTag("SnakeTail")) {
				tail = child;
			}
			else if (child.CompareTag("SnakeBody")) {
					body.Add(child);		
			}
		}

		if (head == null)
			Debug.LogError("Snake has no head");

		if (tail == null)
			Debug.LogError("Snake has no tail");

		if (body.Count == 0)
			Debug.Log("Snake has no body part.");
	}

	void Update() {

		bool directionChanged = checkInput();

		directionVector = getDirectionVector();
		speedVector = directionVector * speed;

		if (directionChanged) {
			unsnappedPosition = transform.position;
			Move(directionVector);
		}

	}

	void LateUpdate() {
		if (transform.hasChanged)
			unsnappedPosition = SnapToGrid(transform.position);

		Move();

		transform.hasChanged = false;
	}

	private bool checkInput() {
		float vertical = Input.GetAxisRaw("Vertical");
		float horizontal = Input.GetAxisRaw("Horizontal");

		bool directionChanged = false;

		if (vertical == 0) {
			verticalAxisPressed = false;
		}

		if (horizontal == 0) {
			horizontalAxisPressed = false;
		}

		if (vertical > 0 && !verticalAxisPressed) {
			if (direction != Direction.Up && direction != Direction.Down) {
				direction = Direction.Up;

				verticalAxisPressed = true;
				directionChanged = true;
			}
		}

		if (vertical < 0 && !verticalAxisPressed) {
			if (direction != Direction.Up && direction != Direction.Down) {
				direction = Direction.Down;

				verticalAxisPressed = true;
				directionChanged = true;
			}
		}

		if (horizontal > 0 && !horizontalAxisPressed) {
			if (direction != Direction.Left && direction != Direction.Right) {
				direction = Direction.Right;

				horizontalAxisPressed = true;
				directionChanged = true;
			}
		}

		if (horizontal < 0 && !horizontalAxisPressed) {
			if (direction != Direction.Left && direction != Direction.Right) {
				direction = Direction.Left;

				horizontalAxisPressed = true;
				directionChanged = true;
			}
		}

		return directionChanged;
	}

	private void Move() {
		Vector2 distanceVector = speedVector * Time.deltaTime;
		unsnappedPosition += distanceVector;
		transform.position = SnapToGrid(unsnappedPosition);
	}

	private void Move(Vector3 distance) {
		transform.position += distance;
		unsnappedPosition = transform.position;
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
		else {
			Debug.LogError("direction does not equal Up, Down, Left, or Right.");
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
