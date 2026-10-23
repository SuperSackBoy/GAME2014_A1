using System;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	[SerializeField] private AudioClip gameMusic;
	[SerializeField] private AudioClip menuMusic;
	[SerializeField] private AudioClip laserShootSound;
	[SerializeField] private AudioClip hitSound;
	[SerializeField] private AudioClip shieldHitSound;
	[SerializeField] private AudioClip enemyDeathSound;
	[SerializeField] private AudioClip playerDeathSound;
	[SerializeField] private AudioClip uiClickSound;
	[SerializeField] private AudioClip powerupSound;
	
	private AudioSource audioSource;
	
	private static SoundManager instance;
	public static SoundManager Instance {
		get {
			if (instance == null) {
				instance = FindAnyObjectByType<SoundManager>();
			}
			return instance;
		}
	}

	private void Start() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}
		
		DontDestroyOnLoad(gameObject);
		audioSource = GetComponent<AudioSource>();
		audioSource.loop = true;
		
		PlayMenuMusic();
		
	}

	public void PlayMenuMusic() {
		audioSource.clip = menuMusic;
		audioSource.Play();
	}

	public void PlayGameMusic() {
		audioSource.clip = gameMusic;
		audioSource.Play();
	}
	
	public void PlayShootSound() {
		audioSource.PlayOneShot(laserShootSound);
	}

	public void PlayHitSound() {
		audioSource.PlayOneShot(hitSound);
	}
	
	public void PlayShieldHitSound() {
		audioSource.PlayOneShot(shieldHitSound);
	}

	public void PlayEnemyDeathSound() {
		audioSource.PlayOneShot(enemyDeathSound);
	}
	
	public void PlayPlayerDeathSound() {
		audioSource.PlayOneShot(playerDeathSound);
	}

	public void PlayUIClickSound() {
		audioSource.PlayOneShot(uiClickSound);
	}

	public void PlayPowerupSound() {
		audioSource.PlayOneShot(powerupSound);
	}
}