using TMPro;
using UnityEngine;

public class TutorialMiniJump : TutorialBase
{
	[SerializeField]
	private SwipeParameter swipeParam;
	private bool isMiniJumpCharge = false;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	override protected void Awake()
	{
		base.Awake();
		isMiniJumpCharge = false;
	}

	override public void OnJumpChargeStart()
	{
		CancelInvoke();
		JumpEnd();
	}
	override public void OnJumpCharging(Vector2 vec)
	{
		float raito = swipeParam.Raito(vec);
		if (vec.y < -30f && raito >= 0.5f && raito <= 0.75f)
		{
			texts[0].color = Color.red;
			isMiniJumpCharge = true;
		}
		else
		{
			texts[0].color = Color.white;
			isMiniJumpCharge = false;
		}
	}
	override public void OnJump(Vector2 vec)
	{
		if (isMiniJumpCharge)
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
		isMiniJumpCharge = false;
		base.JumpEnd();
	}
}
