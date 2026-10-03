using UnityEngine;
using UnityEngine.Events;

public class SlimeJumper : MonoBehaviour
{
	[SerializeField]
	private SwipeParameter swipeParam;
	[SerializeField]
	private UnityEvent<Vector2> OnJumpChargeStartCallback;
	[SerializeField]
	private UnityEvent<Vector2> OnJumpCallback;
	[SerializeField]
	private UnityEvent<Vector2> OnJumpChargingCallback;
	[SerializeField]
	private Vector2Event OnJumpChargeStart;
	[SerializeField]
	private Vector2Event OnJumpCharging;
	[SerializeField]
	private Vector2Event OnJump;
	[SerializeField]
	private GameEventVoid OnJumpCancel;

	[SerializeField]
	private GroundChecker groundChecker;

	private bool isJumpCharging = false;

	public void OnSwipeStart(Vector2 start)
	{
		if (groundChecker.IsGround)
		{
			isJumpCharging = true;
			OnJumpChargeStartCallback?.Invoke(start);
			OnJumpChargeStart.Raise(start);
		}
	}
	public void OnSwipePerform(Vector2 start, Vector2 vec)
	{
		if (isJumpCharging == false)
		{
			return;
		}
		OnJumpChargingCallback?.Invoke(vec);
		OnJumpCharging.Raise(vec);
	}
	public void OnSwipeEnd(Vector2 start, Vector2 vec)
	{
		if (isJumpCharging == false)
		{
			return;
		}
		if (!swipeParam.IsSwipe(vec))
		{
			// ÉXÉèÉCÉvÇ∆ÇµÇƒàµÇÌÇ»Ç¢
			OnJumpCancel.Raise();
			return;
		}
		OnJumpCallback?.Invoke(vec);
		OnJump.Raise(vec);
		isJumpCharging = false;
	}
}
