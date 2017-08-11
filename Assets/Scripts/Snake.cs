using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

	public float movementFrequency = 15; // movements per second

	public float movementDistance = 1; // units traversed per movement

	public enum Direction {Up, Down, Left, Right, Neutral};

	public Direction direction = Direction.Neutral;

	public int length = 1; // length of the snake in number of squares

	public GameObject snakeBodyPrefab;

	public Transform head;
	public Transform tail;
	public List<Transform> body = new List<Transform>();

	[HideInInspector]
	public Vector2 directionVector;
	[HideInInspector]
	public Vector2 distanceVector;

	float _timeCounter = 0;

	// extra body parts that need to be added due to snake length increase
	int _bodyPartsToAdd = 0;

	public enum TimeKeepingSystem {TimeCounter, InvokeRepeating}

	[SerializeField]
	TimeKeepingSystem timeKeepingSystem = TimeKeepingSystem.InvokeRepeating;

	CollisionCheck _collisionCheck;

	void Awake() {
		if (length < 1)
			length = 1;

		if (snakeBodyPrefab == null)
			Debug.LogError("Snake Body Prefab not assigned.");

		_collisionCheck = GetComponent<CollisionCheck>();

		if (_collisionCheck == null)
			Debug.LogError("CollisionCheck script component is missing.");

		InitializeHead();
		UpdateDirectionAndDistanceVectors();
		InitializeBody();

		// set tail reference to last body part in array
		tail = body[length - 1];

		if (timeKeepingSystem == TimeKeepingSystem.InvokeRepeating) {
			float movementPeriod = 1 / movementFrequency;
			InvokeRepeating("UpdateMovement", 0f, movementPeriod);
		}
	}
		
	void Update() {
		if (timeKeepingSystem == TimeKeepingSystem.TimeCounter) {
			_timeCounter += Time.deltaTime;
			float movementPeriod = 1 / movementFrequency;
			if (_timeCounter >= movementPeriod) {
				int multiplesOfPeriod = (int)(_timeCounter / movementPeriod);
//				if (multiplesOfPeriod != 1)
//				Debug.Log("_timeCounter: " + _timeCounter + ", Time.deltaTime: " + Time.deltaTime + ", multiples: " + multiplesOfPeriod);
		
				for (int i = 0; i < multiplesOfPeriod; i++) {
					UpdateMovement();
					_collisionCheck.CheckCollisions(this);
					_timeCounter -= movementPeriod;
				}
//				_timeCounter -= multiplesOfPeriod * movementPeriod;

//				Debug.Log("subtracted _timeCounter: " + _timeCounter);
			}
		}
	}

	void InitializeHead() {
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

	void InitializeBody() {
		// Create and place snake body squares in positions relative to head position.
		// By default, place body squares one to the left of head.
		// Squares are separated from each other by the movementDistance unit.

		Vector2 headPosition = head.transform.position;
		Vector2 destination = headPosition + new Vector2(-movementDistance, 0);

		for (int i = 0; i < length - 1; i++) {
			GameObject bodyPart = CreateNewBodyPart(destination);

			Vector2 bodyPartPosition = bodyPart.transform.position;
			destination = bodyPartPosition + new Vector2(-movementDistance, 0);
		}
	}

	void UpdateMovement() {
		UpdateDirection();
		Move();
	}

	void Move() {			
		// Don't move anything if the snake is not moving in a direction.
		if (direction == Direction.Neutral)
			return;
		
		MoveBody();
		MoveHead();
	}

	void MoveBody() {
		// move first body part to what will be the old head position
		Vector2 destination = head.transform.position;

		// move each body part to the old position of the previous body part
		foreach (Transform bodyPart in body) {
			Vector2 oldPosition = bodyPart.position;
			bodyPart.position = destination;
			destination = oldPosition;
		}

		// Add extra body part to old tail position, if necessary due to snake length having increased
		if (_bodyPartsToAdd > 0) {
			CreateNewBodyPart(destination);
			_bodyPartsToAdd--;
		}
	}

	void MoveHead() {
		head.transform.Translate(distanceVector);
	}

	bool UpdateDirection() {
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
			UpdateDirectionAndDistanceVectors();
		}

		return directionChanged;
	}

	void UpdateDirectionAndDistanceVectors() {
		UpdateDirectionVector();
		UpdateDistanceVector();
	}

	Vector2 UpdateDirectionVector() {
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

	GameObject CreateNewBodyPart(Vector3 destination) {
		GameObject bodyPart = (GameObject)Instantiate(snakeBodyPrefab, destination, Quaternion.identity, transform);
		body.Add(bodyPart.transform);

		// set tail reference to last body part in array
		tail = body[body.Count - 1];

		return bodyPart;
	}

	public void IncreaseLength(int lengthIncrease) {
		if (lengthIncrease > 0) {
			length += lengthIncrease;
			_bodyPartsToAdd += lengthIncrease;
		}
	}

	public void increaseMovementFrequency(float movementFrequencyIncrease) {
		if (movementFrequencyIncrease > 0) {
			
			movementFrequency += movementFrequencyIncrease;

			if (timeKeepingSystem == TimeKeepingSystem.InvokeRepeating) {
				CancelInvoke();
				float movementPeriod = 1 / movementFrequency;
				InvokeRepeating("UpdateMovement", 0, movementPeriod);
			}
		}
	}
}