using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour {
	[SerializeField] private float maxSpeed;
	[SerializeField] private float minSpeed;
	[SerializeField] private float minShootCooldown;
	[SerializeField] private float maxShootCooldown;
	[SerializeField] [Range(0, 1)] private float hpDropChance;
	[SerializeField] private GameObject healthPrefab;
	
	[SerializeField] GameObject laserPrefab;
	[SerializeField] Transform laserSpawnPoint;
	
	private Rigidbody2D rb;
	private float speed;
	private float currentShootCooldown;
	
	
	private void Start() {
		rb = GetComponent<Rigidbody2D>();
		Vector2 posNormal = new Vector2(transform.position.x, transform.position.y).normalized;
		float angle = Mathf.Atan2(posNormal.x, posNormal.y) * Mathf.Rad2Deg;
		
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
		
		speed = Random.Range(minSpeed, maxSpeed);
		
		rb.linearVelocity = transform.up * -speed;
		currentShootCooldown = minShootCooldown;
	}

	private void Update() {
		currentShootCooldown -= Time.deltaTime;
		if (currentShootCooldown < 0 && transform.position.magnitude > 0.12f) {
			Instantiate(laserPrefab, laserSpawnPoint.position, transform.rotation);
			currentShootCooldown = Random.Range(minShootCooldown, maxShootCooldown);
			if (maxShootCooldown > minShootCooldown) {
				maxShootCooldown -= 0.03f;
				maxShootCooldown = Mathf.Max(maxShootCooldown, minShootCooldown);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player")) {
			other.gameObject.GetComponent<PlayerController>().TakeDamage();
			Destroy(gameObject);
		}
	}

	private bool isQuitting = false;
	private void OnApplicationQuit() {
		isQuitting = true;
	}
	
	private void OnDestroy() {
		if (isQuitting) return;
		GameManager.Instance?.AddScore(25);
		SoundManager.Instance?.PlayEnemyDeathSound();
		TrySpawnHealth();
	}

	private void TrySpawnHealth() {
		if (Random.value > hpDropChance) return;
		Instantiate(healthPrefab, transform.position, Quaternion.identity);
	}
}