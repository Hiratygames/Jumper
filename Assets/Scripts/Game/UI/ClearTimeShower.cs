using System.Collections;
using TMPro;
using UnityEngine;

public class ClearTimeShower : MonoBehaviour
{
	[SerializeField]
	private TextMeshPro textLabel;
	[SerializeField]
	private TextMeshPro textClearTime;

	public void OnClearCallback(float time)
	{
		textClearTime.text = GetPlayTimeString(time);
	}
	public void FadeOut(float duration)
	{
		StartCoroutine(Fading(duration));
	}
	private IEnumerator Fading(float duration)
	{
		float timeCounter = 0f;
		textLabel.alpha = textClearTime.alpha = 1.0f;

		while (timeCounter < duration)
		{
			timeCounter += Time.deltaTime;
			textLabel.alpha = textClearTime.alpha = Mathf.Lerp(1.0f, 0.0f, timeCounter / duration);
			yield return null;
		}
		textLabel.alpha = textClearTime.alpha = 0.0f;
	}
	private string GetPlayTimeString(float time)
	{
		int minute = (int)time / 60;
		int second = (int)time % 60;
		if (minute == 0)
		{
			return second.ToString() + "•b";
		}
		return minute.ToString() + "•ª" + second.ToString() + "•b";
	}
}
