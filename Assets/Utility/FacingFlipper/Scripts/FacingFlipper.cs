using UnityEngine;

public class FacingFlipper : MonoBehaviour
{
	// ベースは右向き
	[SerializeField]
	private bool isReverse;
	[SerializeField]
	private bool isUseScale;
	private SpriteRenderer spriteRenderer;

	private bool curFacing;
	public bool IsRight { get { return curFacing; } }

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		curFacing = !isReverse;
	}
	public void FacingFlip(bool isRight)
	{
		if (curFacing == isRight)
		{
			return;
		}
		bool isFlip = !isRight;
		if (isReverse)
		{
			isFlip = !isFlip;
		}
		if (isUseScale)
		{
			transform.localScale = new Vector3(isFlip ? -1.0f : 1.0f, 1.0f, 1.0f);
		}
		else
		{
			spriteRenderer.flipX = isFlip;
		}
		curFacing = isRight;
	}
}
