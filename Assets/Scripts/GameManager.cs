using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	[SerializeField] private TMP_Text scoreText;
	[SerializeField] private TMP_Text livesText;
	[SerializeField] private int maxLives;

	private static GameManager instance;
	public static GameManager Instance {
		get {
			if (instance == null) {
				instance = GameObject.FindAnyObjectByType<GameManager>();
			}
			return instance;
		}
	}
	
	private int score;
	private int lives;

	private static int lastScore;
	
	
	
	private void Start() {
		score = 0;
		lives = maxLives;
		
		SetLifeText();
		SetScoreText();
		
		SoundManager.Instance.PlayGameMusic();
	}

	private void OnDestroy() {
		if (instance == this) {
			instance = null;
		}
	}

	public void TakeLife() {
		lives--;
		SetLifeText();
		if (lives == 0) {
			lastScore = score;
			SoundManager.Instance.PlayPlayerDeathSound();
			SoundManager.Instance.PlayMenuMusic();
			SceneManager.LoadScene("EndScene");
		}
		else {
			SoundManager.Instance.PlayHitSound();
		}
	}
	
	public void AddLife() {
		if (lives < maxLives) {
			lives++;
			SetLifeText();
		}
		SoundManager.Instance.PlayPowerupSound();
	}

	public void AddScore(int points) {
		score += points;
		SetScoreText();
	}

	private void SetLifeText() {
		livesText.text = lives.ToString();
	}

	private void SetScoreText() {
		scoreText.text = "Score: " + score.ToString();
	}

	public static int GetLastScore() {
		return lastScore;
	}
}