using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneButton : MonoBehaviour {
	[SerializeField] private string sceneToLoad;

	private void Start() {
		GetComponent<Button>().onClick.AddListener(OnClick);
	}

	private void OnClick() {
		SceneManager.LoadScene(sceneToLoad);
		SoundManager.Instance.PlayUIClickSound();
	}
}
