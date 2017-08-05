using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadCollision : MonoBehaviour {

	private BoxCollider2D _collider;

	void Awake() {
		_collider = GetComponent<BoxCollider2D>();
	}


}
