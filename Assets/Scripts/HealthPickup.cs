using System;
using UnityEngine;


public class HealthPickup : MonoBehaviour {
	[SerializeField] private float speed;

	private Rigidbody2D rb;
	private void Start() {
		rb = GetComponent<Rigidbody2D>();
		Vector2 posNormal = new Vector2(transform.position.x, transform.position.y).normalized;
		
		rb.linearVelocity = posNormal * -speed;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player")) {
			GameManager.Instance.AddLife();
			Destroy(gameObject);
		}
	}
}
