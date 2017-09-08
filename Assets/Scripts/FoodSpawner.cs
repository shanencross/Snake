using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FoodSpawner : MonoBehaviour
{
	public int pelletCount = 1;
	public int lengthIncrease = 1;
	public int movementFrequencyIncrease = 1;
	public float xMin = -19.5f;
	public float xMax = 19.5f;
	public float yMin = -17.5f;
	public float yMax = 17.5f;
	public GameObject foodPelletPrefab;

	public List<Transform> foodPellets = new List<Transform>();

	[SerializeField]
	private List<Transform> foodPelletTransformsToAdd = new List<Transform>();
	[SerializeField]
	private List<Transform> foodPelletTransformsToRemove = new List<Transform>();



	void Awake() {
		if (foodPelletPrefab == null)
			Debug.LogError("Food Pellet Prefab not assigned.");

		for (int i = 0; i < pelletCount; i++) {
			SpawnFood();
		}
	}

	public void SpawnFood() {
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

			GameObject foodPelletObject = (GameObject)Instantiate(foodPelletPrefab, location, Quaternion.identity, transform);

			foodPelletTransformsToAdd.Add(foodPelletObject.transform);
	}
		
	public void DestroyFoodPellet(Transform foodPelletTransform) {
		if (foodPelletTransform != null) {
			foodPelletTransformsToRemove.Add(foodPelletTransform);
			SpawnFood();
		}
	}

	public void UpdateFoodPellets() {

		foreach (Transform foodPelletTransform in foodPelletTransformsToRemove) {
			Debug.Log("Removing: " + foodPelletTransform.position);
			foodPellets.Remove(foodPelletTransform);
			Destroy(foodPelletTransform.gameObject);
		}
		foodPelletTransformsToRemove.Clear();

		foreach (Transform foodPelletTransform in foodPelletTransformsToAdd) {
			Debug.Log("Adding " + foodPelletTransform.position);
			foodPellets.Add(foodPelletTransform);

			FoodPellet foodPellet = foodPelletTransform.GetComponent<FoodPellet>();

			if (foodPellet != null)
				foodPellet.Initialize(gameObject, lengthIncrease, movementFrequencyIncrease);
		}

		foodPelletTransformsToAdd.Clear();
	}

	float SnapToGrid(float num) {
		// Round to a multiple of 1 with a 0.5 offset: ..., -2.5, -1.5, -0.5, 0.5, 1.5, 2.5, ...
		return Mathf.Sign(num) * (Mathf.Abs((int)(num)) + 0.5f);
	}

}

