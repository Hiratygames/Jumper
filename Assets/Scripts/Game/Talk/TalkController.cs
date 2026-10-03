using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Events;

public class TalkController : MonoBehaviour
{
	[SerializeField] TextMeshPro text;
	[SerializeField] float interval = 0.05f;
	[SerializeField] float deleteWaitTime = 2.0f;

	public event UnityAction OnFinish;

	string fullText;
	Coroutine routine;

	void Awake()
	{
		if (text == null) text = GetComponent<TextMeshPro>();
		text.text = "";
	}

	public void Play(string message)
	{
		fullText = message;
		if (routine != null) StopCoroutine(routine);
		routine = StartCoroutine(TypeRoutine());
	}
	public void Stop()
	{
		if (routine != null) StopCoroutine(routine);
		routine = null;
	}

	IEnumerator TypeRoutine()
	{
		text.text = "";
		var count = 0;

		while (count < fullText.Length)
		{
			count++;
			text.text = fullText.Substring(0, count);
			yield return new WaitForSeconds(interval);
		}

		yield return new WaitForSeconds(deleteWaitTime);
		text.text = "";
		OnFinish?.Invoke();
		routine = null;
	}
}
