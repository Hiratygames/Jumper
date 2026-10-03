using System.Collections;
using UnityEngine;

public class StopObserver : MonoBehaviour
{
	[SerializeField]
	private Vector2Event OnPlayerStopped;
	[SerializeField]
	private Slime slime;
	[SerializeField]
	private GroundChecker groundChecker;
	[SerializeField]
	private Rigidbody2D rb;

    private Coroutine coroutine;

    public void StartObserve()
    {
		coroutine = StartCoroutine(Observe());
	}
	IEnumerator Observe()
	{
		float timer = 0f;

		while (true)
		{
			// ínñ Ç©ÇÁó£ÇÍÇΩÇÁíÜé~
			if (!groundChecker.IsGround)
			{
				coroutine = null;
				yield break;
			}

			// í‚é~ÇµÇƒÇ¢ÇÈÇ©ÅH
			if (rb.linearVelocity.sqrMagnitude < 0.01f)
			{
				timer += Time.deltaTime;

				if (timer > 0.2f)
				{
					// í‚é~ÉCÉxÉìÉgî≠âŒ
					OnPlayerStopped.Raise(slime.transform.position);

					coroutine = null;
					yield break;
				}
			}
			else
			{
				timer = 0f;
			}

			yield return null;
		}
	}

	public void CancelObserve()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
		}
    }

}
