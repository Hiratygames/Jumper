using UnityEngine;

public class SlimeScaler : MonoBehaviour
{
	[SerializeField]
	private SwipeParameter swipeParam;
	[SerializeField]
	private CircleCollider2D circleCollider;
	[SerializeField]
	private Vector2 maxJumpChargeScale;
	[SerializeField]
	private AnimationCurve animCurveJumpChargeScaleX;
	[SerializeField]
	private AnimationCurve animCurveJumpChargeScaleY;

	[SerializeField]
	private float landingEffMinSpeed = 1.0f;
	[SerializeField]
	private float landingEffMaxSpeed = 15.0f;

	private Vector2 collOffset;
	private float collRadius;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		collOffset = circleCollider.offset;
		collRadius = circleCollider.radius;
	}
	public void OnSwipeStart(Vector2 start)
	{
		CancelInvoke("ResetScale");
		ResetScale();
	}
	public void OnSwipePerform(Vector2 vec)
	{
		if (!swipeParam.IsSwipe(vec)) return;// ÉXÉèÉCÉvÇ∆ÇµÇƒàµÇÌÇ»Ç¢
		float raito = swipeParam.Raito(vec);
		ChangeScale(new Vector2(animCurveJumpChargeScaleX.Evaluate(raito), animCurveJumpChargeScaleY.Evaluate(raito)));
		ChangeCollider(animCurveJumpChargeScaleY.Evaluate(raito));
	}
	public void OnSwipeEnd()
	{
		ChangeScale(Vector2.one);
		ChangeCollider(1.0f);
	}

	public void OnJumping(Vector2 speed)
	{

	}

	public void OnLanding(float speed)
	{
		float raito = (Mathf.Clamp(speed, landingEffMinSpeed, landingEffMaxSpeed) - landingEffMinSpeed) / (landingEffMaxSpeed - landingEffMinSpeed);
		ChangeScale(new Vector2(animCurveJumpChargeScaleX.Evaluate(raito), animCurveJumpChargeScaleY.Evaluate(raito)));
		ChangeCollider(1.0f);
		Invoke("ResetScale", 0.5f * raito);
	}
	public void OnFlying()
	{
		CancelInvoke("ResetScale");
		ChangeCollider(1.0f);
		ResetScale();
	}

	private void ChangeCollider(float raito)
	{
		circleCollider.offset = new Vector2(collOffset.x, collOffset.y - collRadius * (1.0f - raito));
		circleCollider.radius = collRadius * raito;
	}

	private void ChangeScale(Vector2 scale)
	{
		transform.localScale = scale;
	}
	private void ResetScale()
	{
		transform.localScale = Vector2.one;
	}
}
