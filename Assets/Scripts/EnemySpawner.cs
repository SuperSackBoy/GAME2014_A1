using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour {
	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private float maxSpawnCooldown;
	[SerializeField] private float minSpawnCooldown;
	[SerializeField] private float spawnMargin;
	
	private float currentSpawnCooldown;
	private float screenHeight;
	private float screenWidth;
	
	
	private void Start() {
		currentSpawnCooldown = maxSpawnCooldown;
		screenHeight = ScreenBoundsChecker.Instance.GetMaxY();
		screenWidth = ScreenBoundsChecker.Instance.GetMaxX();
	}

	private void Update() {
		currentSpawnCooldown -= Time.deltaTime;
		if (currentSpawnCooldown < 0) {
			SpawnEnemy();
		}
	}

	private void SpawnEnemy() {
		currentSpawnCooldown = Random.Range(minSpawnCooldown, maxSpawnCooldown);
		
		float spawnOnTop = Random.value > 0.5f ? 1 : -1;
		float spawnX = Random.Range(-screenWidth, screenWidth);

		float spawnY = (screenHeight + spawnMargin) * spawnOnTop;
		
		Instantiate(enemyPrefab, new Vector3(spawnX, spawnY, 0), Quaternion.identity);
	}
}