using UnityEngine;

[ExecuteAlways]
public class SafeAreaFitter : MonoBehaviour
{
	void Update()
	{
		var safe = Screen.safeArea;
		var anchorMin = safe.position;
		var anchorMax = safe.position + safe.size;

		anchorMin.x /= Screen.width;
		anchorMin.y /= Screen.height;
		anchorMax.x /= Screen.width;
		anchorMax.y /= Screen.height;

		var t = GetComponent<RectTransform>();
		t.anchorMin = anchorMin;
		t.anchorMax = anchorMax;
	}
}
