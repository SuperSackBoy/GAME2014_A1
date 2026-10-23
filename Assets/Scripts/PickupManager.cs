
using UnityEngine;

public class PickupManager : MonoBehaviour {
	[SerializeField] GameObject pickupPrefab;
	[SerializeField] [Range(0, 1)] private float pickupSpawnChance;
	
	private static PickupManager instance;

	public static PickupManager Instance {
		get {
			if (instance == null) {
				instance = GameObject.FindAnyObjectByType<PickupManager>();
			}
			return instance;
		}
	}

	public void SpawnPickup(Vector2 position) {
		if (Random.value > pickupSpawnChance) return;
		GameObject go = Instantiate(pickupPrefab, position, Quaternion.identity);
		go.transform.parent = transform;
	}
}