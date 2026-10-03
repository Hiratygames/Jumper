using UnityEngine;
using UnityEngine.Events;

public class GroundChecker : MonoBehaviour
{
	[SerializeField]
	private Vector2Event OnLanding;
    [SerializeField]
    private UnityEvent OnGroundEnter;
	[SerializeField]
	private UnityEvent OnGroundExit;

	private bool isGround = false;
	public bool IsGround { get { return isGround; } }


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Field"))
		{
			isGround = true;
			OnGroundEnter?.Invoke();
			OnLanding?.Raise(transform.position);
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Field"))
		{
			isGround = false;
			OnGroundExit?.Invoke();
		}
	}
}
