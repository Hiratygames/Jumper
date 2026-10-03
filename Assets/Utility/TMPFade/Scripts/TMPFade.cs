using TMPro;
using UnityEngine;
using System.Collections;

public class TMPFade : MonoBehaviour
{
	[SerializeField] private float showTime = 2f;   // 表示しておく時間
	[SerializeField] private float fadeInTime = 1f;   // フェードインにかける時間
	[SerializeField] private float fadeOutTime = 1f;   // フェードアウトにかける時間

	private TextMeshProUGUI tmp;
	private Coroutine fadeCoroutine;

	private void Awake()
	{
		tmp = GetComponent<TextMeshProUGUI>();
	}

	public void SetText(string str)
	{
		tmp.text = str;
	}
	public void SetColor(Color color)
	{
		tmp.color = color;
	}
	public void StartFade()
	{
		fadeCoroutine = StartCoroutine(FadeRoutine());
	}

	public void CancelFade()
	{
		if (fadeCoroutine != null)
		{
			StopCoroutine(fadeCoroutine);
			fadeCoroutine = null;
		}
	}

	private IEnumerator FadeRoutine()
	{
		// フェードイン
		float t = 0f;
		Color c = tmp.color;
		c.a = 0;
		while (t < fadeInTime)
		{
			t += Time.deltaTime;
			float alpha = Mathf.Lerp(0f, 1f, t / fadeInTime);
			tmp.color = new Color(c.r, c.g, c.b, alpha);
			yield return null;
		}

		// 表示時間待つ
		yield return new WaitForSeconds(showTime);

		// フェードアウト
		t = 0f;
		c = tmp.color;

		while (t < fadeOutTime)
		{
			t += Time.deltaTime;
			float alpha = Mathf.Lerp(1f, 0f, t / fadeOutTime);
			tmp.color = new Color(c.r, c.g, c.b, alpha);
			yield return null;
		}

		// 完全に透明に
		tmp.color = new Color(c.r, c.g, c.b, 0f);
		fadeCoroutine = null;
	}
}
