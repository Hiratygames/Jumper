using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PointerBlocker : MonoBehaviour
{
	public static bool IsPointerOverUI()
	{
		// タッチ端末
		if (Touchscreen.current != null)
		{
			foreach (var touch in Touchscreen.current.touches)
			{
				if (touch.press.isPressed &&
					EventSystem.current.IsPointerOverGameObject(touch.touchId.ReadValue()))
				{
					return true;
				}
			}
			return false;
		}

		// マウス端末
		return EventSystem.current.IsPointerOverGameObject(-1);
	}
	public GraphicRaycaster raycaster;
	public EventSystem eventSystem;

	private static bool IsPointerOverUI(EventSystem eventSystem, GraphicRaycaster raycaster, Vector2 screenPos)
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
