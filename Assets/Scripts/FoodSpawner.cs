using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FoodSpawner : MonoBehaviour
{
	public int pelletCount = 1;
	public float xMin = -19.5f;
	public float xMax = 19.5f;
	public float yMin = -17.5f;
	public float yMax = 17.5f;
	public GameObject foodPelletPrefab;

	[SerializeField]
	List<Transform> foodPellets = new List<Transform>();

	void Awake() {
		if (foodPelletPrefab == null)
			Debug.LogError("Food Pellet Prefab not assigned.");

		SpawnFood();
	}

	public void SpawnFood() {
		for (int i = 0; i < pelletCount; i++) {

			Vector2 location;
			bool spaceOccupied;
			int whileLoopCount = 0;
			do {
				float unroundedRandomX = Random.Range(xMin, xMax);
				float unroundedRandomY = Random.Range(yMin, yMax);

				float randomX = SnapToGrid(unroundedRandomX);
				float randomY = SnapToGrid(unroundedRandomY);

				location = new Vector2(randomX, randomY);
				spaceOccupied = Physics2D.OverlapBox(location, Vector2.one, 0);

				whileLoopCount++;
				if (whileLoopCount > 10) {
					Debug.LogError("While loop has looped " + whileLoopCount + " times. Something is wrong. Exiting loop.");
					break;
				}

			} while (spaceOccupied); 

			GameObject foodPellet = (GameObject)Instantiate(foodPelletPrefab, location, Quaternion.identity, transform);
			foodPellets.Add(foodPellet.transform);


		}
	}
		
	float SnapToGrid(float num) {
		return Mathf.Sign(num) * (Mathf.Abs((int)(num)) + 0.5f);
	}

}

