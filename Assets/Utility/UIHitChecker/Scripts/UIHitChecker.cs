using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIHitChecker : MonoBehaviour
{
	public GraphicRaycaster raycaster;
	public EventSystem eventSystem;

	/// <summary>
	/// 指定したスクリーン座標が UI の上かどうか判定
	/// </summary>
	public bool IsPointerOverUI(Vector2 screenPos)
	{
		var pointerData = new PointerEventData(eventSystem)
		{
			position = screenPos
		};

		var results = new List<RaycastResult>();
		raycaster.Raycast(pointerData, results);

		return results.Count > 0;
	}
}
