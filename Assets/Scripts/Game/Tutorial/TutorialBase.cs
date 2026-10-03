using TMPro;
using UnityEngine;

abstract public class TutorialBase : MonoBehaviour
{
	[SerializeField]
	protected TextMeshPro[] texts;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	virtual protected void Awake()
	{
		foreach (TextMeshPro textMeshPro in texts)
		{
			textMeshPro.enabled = false;
		}
	}
	// Player‚ª“ü‚Á‚Ä‚«‚½‚Æ‚«
	private void OnTriggerEnter2D(Collider2D collision)
	{

		if (collision.gameObject.CompareTag("PlayerCameraCollider"))
		{
			foreach (TextMeshPro textMeshPro in texts)
			{
				textMeshPro.color = Color.white;
				textMeshPro.enabled = true;
			}
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("PlayerCameraCollider"))
		{
			CancelInvoke();
			foreach (TextMeshPro textMeshPro in texts)
			{
				textMeshPro.color = Color.white;
				textMeshPro.enabled = false;
			}
		}
	}

	abstract public void OnJumpChargeStart();
	abstract public void OnJumpCharging(Vector2 vec);
	abstract public void OnJump(Vector2 vec);
	virtual public void OnJumpCancel()
	{
		JumpEnd();
	}

	virtual protected void JumpEnd()
	{
		foreach (TextMeshPro textMeshPro in texts)
		{
			textMeshPro.color = Color.white;
		}
	}
}
