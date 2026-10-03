using TMPro;
using UnityEngine;

public class TutorialHorizontalJump : TutorialBase
{
	[SerializeField]
	private SwipeParameter swipeParam;
	private bool isHorizontalJumpCharge = false;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	override protected void Awake()
	{
		base.Awake();
		isHorizontalJumpCharge = false;
	}

	override public void OnJumpChargeStart()
	{
		CancelInvoke();
		JumpEnd();
	}
	override public void OnJumpCharging(Vector2 vec)
	{
		float angle = AngleFromDown(vec.normalized);
		if (angle >= 60 && angle <= 80)
		{
			texts[0].color = Color.red;
			isHorizontalJumpCharge = true;
		}
		else
		{
			texts[0].color = Color.white;
			isHorizontalJumpCharge = false;
		}
	}
	override public void OnJump(Vector2 vec)
	{
		if (isHorizontalJumpCharge)
		{
			texts[1].color = Color.red;
			Invoke("JumpEnd", 1.0f);
		}
		else
		{
			JumpEnd();
		}
	}

	override protected void JumpEnd()
	{
		isHorizontalJumpCharge = false;
		base.JumpEnd();
	}
	float AngleFromDown(Vector2 dir)
	{
		float angle = Mathf.Atan2(dir.x, -dir.y) * Mathf.Rad2Deg;
		if (angle < 0) angle += 360f; return angle;
	}
}
