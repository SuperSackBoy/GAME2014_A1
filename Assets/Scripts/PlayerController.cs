using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
	[SerializeField] private InputActionReference moveAction;
	[SerializeField] private InputActionReference shootAction;
	[SerializeField] private float shootCooldown;
	[SerializeField] private GameObject laserPrefab;
	[SerializeField] private Transform laserSpawnPoint;
	[SerializeField] private ParticleSystem rThrustEffect;
	[SerializeField] private ParticleSystem lThrustEffect;
	[SerializeField] private float turnSpeed;
	
	private float currentShootCooldown = 0.0f;
	private bool shootButtonHeld = false;

	private float currentAngle = 0;
	
	private void Start() {
		moveAction.action.Enable();
		shootAction.action.Enable();
	}

	private void FixedUpdate() {
		Vector2 moveActionValue = moveAction.action.ReadValue<Vector2>();
		if (moveActionValue.magnitude > 0.2f) {
			float desiredAngle = Mathf.Atan2(moveActionValue.x, moveActionValue.y) * Mathf.Rad2Deg;
			float dir = Mathf.DeltaAngle(currentAngle, desiredAngle);
			if (dir > 0.5f) {
				if (rThrustEffect.isPlaying) rThrustEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
				if (!lThrustEffect.isPlaying) lThrustEffect.Play(true);
			}
			else if (dir < -0.5f) {
				if (!rThrustEffect.isPlaying) rThrustEffect.Play(true);
				if (lThrustEffect.isPlaying) lThrustEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
			}
			else {
				if (rThrustEffect.isPlaying) rThrustEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
				if (lThrustEffect.isPlaying) lThrustEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
			}
			
			currentAngle = Mathf.MoveTowardsAngle(currentAngle, desiredAngle, turnSpeed * Time.fixedDeltaTime);
			
			transform.rotation = Quaternion.AngleAxis(currentAngle, Vector3.back);
		}
		else {
			if (rThrustEffect.isPlaying) rThrustEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
			if (lThrustEffect.isPlaying) lThrustEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
		}
		
		currentShootCooldown -= Time.fixedDeltaTime;
		if (shootAction.action.IsPressed()) Shoot();
		else shootButtonHeld = false;
	}

	private void Shoot() {
		if (currentShootCooldown > 0 || shootButtonHeld) return;
		currentShootCooldown = shootCooldown;
		
		Instantiate(laserPrefab, laserSpawnPoint.position, transform.rotation);
		shootButtonHeld = true;
	}
	
	public void TakeDamage() {
		GameManager.Instance.TakeLife();
	}
}