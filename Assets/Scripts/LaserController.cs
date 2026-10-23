using System;
using UnityEngine;

enum LaserType {
	PlayerLaser,
	EnemyLaser
}

public class LaserController : MonoBehaviour {
	[SerializeField] LaserType laserType;
	[SerializeField] private float speed;

	private Rigidbody2D rb;
	
	private void Start() {
		rb = GetComponent<Rigidbody2D>();
		float spdMult = (laserType == LaserType.PlayerLaser) ? 1 : -1;
		
		rb.linearVelocity = transform.up * speed * spdMult;
		
		SoundManager.Instance.PlayShootSound();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (laserType == LaserType.EnemyLaser) {
			if (other.gameObject.CompareTag("Player")) {
				other.gameObject.GetComponent<PlayerController>().TakeDamage();
				Destroy(gameObject);
			} else if (other.gameObject.CompareTag("Shield")) {
				SoundManager.Instance.PlayShieldHitSound();
				Destroy(gameObject);
			}
		}
		else if (other.gameObject.CompareTag("Enemy") && laserType == LaserType.PlayerLaser) {
			Destroy(other.gameObject);
			PickupManager.Instance.SpawnPickup(other.transform.position);
			Destroy(gameObject);
		}
	}

	private void FixedUpdate() {
		if (ScreenBoundsChecker.Instance.IsOffscreen(transform.position)) {
			Destroy(gameObject);
		}
	}
}