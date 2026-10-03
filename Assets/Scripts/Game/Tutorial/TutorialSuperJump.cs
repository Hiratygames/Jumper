using TMPro;
using UnityEngine;

public class TutorialSuperJump : TutorialBase
{
	[SerializeField]
	private SwipeParameter swipeParam;
	private bool isSuperJumpCharge = false;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	override protected void Awake()
	{
		base.Awake();
		isSuperJumpCharge = false;
	}

	override public void OnJumpChargeStart()
	{
		CancelInvoke();
		JumpEnd();
	}
	override public void OnJumpCharging(Vector2 vec)
	{
		float raito = swipeParam.Raito(vec);
		if (vec.y < -100f && raito >= 0.9f)
		{
			texts[0].color = Color.red;
			isSuperJumpCharge = true;
		}
		else
		{
			texts[0].color = Color.white;
			isSuperJumpCharge = false;
		}
	}
	override public void OnJump(Vector2 vec)
	{
		if (isSuperJumpCharge)
		{
			texts[1].color = Color.red;
			Invoke("JumpEnd", 2.0f);
		}
		else
		{
			JumpEnd();
		}
	}

	override protected void JumpEnd()
	{
		isSuperJumpCharge = false;
		base.JumpEnd();
	}
}
