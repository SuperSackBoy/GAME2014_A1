using UnityEngine;
using UnityEngine.UI;

public class SafeZone : MonoBehaviour {
	
	
	void Start() {
		RectTransform rectTransform = GetComponent<RectTransform>();
		
		Vector2 screenSize = new Vector2(Screen.width, Screen.height);
		Vector2 minSafeArea = Screen.safeArea.min / screenSize;
		Vector2 maxSafeArea = Screen.safeArea.max / screenSize;


		rectTransform.anchorMin = minSafeArea;
		rectTransform.anchorMax = maxSafeArea;

		Canvas.ForceUpdateCanvases();
		
		for (int i = 0; i < transform.childCount; i++) {
			RectTransform child = transform.GetChild(i).GetComponent<RectTransform>();
			LayoutRebuilder.MarkLayoutForRebuild(child);
		}
		
	}
}