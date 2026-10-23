using System;
using TMPro;
using UnityEngine;

public class EndScreenScoreLoader : MonoBehaviour {
	private void Start() {
		GetComponent<TMP_Text>().text = "SCORE: " + GameManager.GetLastScore().ToString();
	}
}