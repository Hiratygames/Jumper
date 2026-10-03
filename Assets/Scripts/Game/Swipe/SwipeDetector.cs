using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class SwipeDetector : MonoBehaviour
{
	[SerializeField]
	private UIHitChecker hitChecker;
	[SerializeField]
	private RectTransform targetRect;
	[SerializeField]
	private InputAction pressAction;
	[SerializeField]
	private InputAction swipeAction;
	private Vector2 startPos;
	private Vector2 endPos;

	[SerializeField]
	private UnityEvent<Vector2> OnSwipeStartCallback;
	[SerializeField]
	private UnityEvent<Vector2, Vector2> OnSwipeEndCallback;
	[SerializeField]
	private UnityEvent<Vector2, Vector2> OnSwipePerformCallback;

	private bool isSwipeStarted = false;

	private void OnEnable()
	{
		pressAction.started += OnSwipeStart;
		pressAction.canceled += OnSwipeEnd;
		swipeAction.performed += OnSwipePerform;
		pressAction.Enable();
		swipeAction.Enable();
	}
	private void OnDisable()
	{
		pressAction.started -= OnSwipeStart;
		pressAction.canceled -= OnSwipeEnd;
		swipeAction.performed -= OnSwipePerform;
		pressAction.Disable();
		swipeAction.Disable();
	}
	private void OnSwipeStart(InputAction.CallbackContext ctx)
	{
		if (hitChecker.IsPointerOverUI(swipeAction.ReadValue<Vector2>()))
		{
			// UI を触っている間はゲームの pointer 処理を無効化
			return;
		}

		// ここからゲームの pointer 処理

		isSwipeStarted = true;
		startPos = GetUIPosition(swipeAction.ReadValue<Vector2>());
		OnSwipeStartCallback?.Invoke(startPos);
	}
	private void OnSwipePerform(InputAction.CallbackContext ctx)
	{
		if (isSwipeStarted == false)
		{
			return;
		}
		endPos = GetUIPosition(ctx.ReadValue<Vector2>());
		Vector2 vec = endPos - startPos;
		OnSwipePerformCallback?.Invoke(startPos, vec);
	}
	private void OnSwipeEnd(InputAction.CallbackContext ctx)
	{
		if (isSwipeStarted == false)
		{
			return;
		}
		endPos = GetUIPosition(swipeAction.ReadValue<Vector2>());
		Vector2 vec = endPos - startPos;
		OnSwipeEndCallback?.Invoke(startPos, vec);
		DetectSwipe();
		isSwipeStarted = false;
	}
	private void DetectSwipe()
	{
		Vector2 diff = endPos - startPos;
		if (diff.magnitude < 50f) return;// スワイプとして扱わない
		if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
		{
			if (diff.x > 0) Debug.Log("→ 右スワイプ");
			else Debug.Log("← 左スワイプ");
		} else {
			if (diff.y > 0)
				Debug.Log("↑ 上スワイプ"); else Debug.Log("↓ 下スワイプ");
		}
	}

	private Vector2 GetUIPosition(Vector2 pos)
	{
		Vector2 uiPos;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(targetRect, pos, null, out uiPos);
		return uiPos;
	}
}