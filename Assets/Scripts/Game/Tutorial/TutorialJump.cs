using TMPro;
using UnityEngine;

public class TutorialJump : TutorialBase
{
	override public void OnJumpChargeStart()
	{
		CancelInvoke();
		JumpEnd();
		texts[0].color = Color.red;
	}
	override public void OnJumpCharging(Vector2 vec)
	{
		if (vec.y < -100f)
		{
			texts[1].color = Color.red;
		}
		else
		{
			texts[1].color = Color.white;
		}
	}
	override public void OnJump(Vector2 vec)
	{
		texts[2].color = Color.red;

		Invoke("JumpEnd", 1.0f);
	}
}
