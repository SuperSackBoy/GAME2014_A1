using UnityEngine;

public class ScreenBoundsChecker : MonoBehaviour {

	private float maxX;
	private float maxY;
	
	private static ScreenBoundsChecker instance;
	public static ScreenBoundsChecker Instance {
		get {
			if (instance == null) {
				CreateInstance();
			}
			return instance;
		}
	}

	private static void CreateInstance() {
		if (instance != null) return;
		GameObject go = new GameObject("ScreenBoundsChecker");
		instance = go.AddComponent<ScreenBoundsChecker>();
		instance.Init();
		DontDestroyOnLoad(go);
		
	}

	private void Init() {
		Vector2 screenSize = new Vector2(Screen.width, Screen.height);
		Camera cam = Camera.main;

		Vector2 max = cam.ScreenToWorldPoint(screenSize);
		maxX = max.x;
		maxY = max.y;
	}

	public bool IsOffscreen(Vector2 position, float margin = 0) {
		return Mathf.Abs(position.x) >= maxX + margin || Mathf.Abs(position.y) >= maxY + margin;
	}
	
	public float GetMaxY() {
		return maxY;
	}
	
	public float GetMaxX() {
		return maxX;
	}
	
}