using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System;

public class BGMFader : MonoBehaviour
{
	[SerializeField]
	private AudioMixer mixer;
	private AudioSource audioSource;
	private Action fadeEndCallback;
	Coroutine fadeCoroutine = null;
	private float timeCounter = 0.0f;
	const string BGM_PARAM = "BGMVolume";

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		fadeEndCallback = null;
	}
	public void SetVolume(float volume)
	{
		Debug.Log("SetVolume:" + LinearToDecibel(volume));
		mixer.SetFloat(BGM_PARAM, LinearToDecibel(volume));
	}
	public void FadeBGM(float startVolume, float targetVolume, float duration, Action callback = null)
	{
		timeCounter = 0.0f;
		fadeEndCallback = callback;
		fadeCoroutine = StartCoroutine(FadeCoroutine(startVolume, targetVolume, duration));
	}
	public float CancelFade()
	{
		if (fadeCoroutine != null)
		{
			StopCoroutine(fadeCoroutine);
			fadeCoroutine = null;
			fadeEndCallback = null;
			float time = timeCounter;
			timeCounter = 0.0f;
			return time;
		}
		return 0.0f;
	}

	IEnumerator FadeCoroutine(float startVolume, float targetVolume, float duration)
	{
		timeCounter = 0f;
		while (timeCounter < duration)
		{
			timeCounter += Time.deltaTime;
			float v = Mathf.Lerp(startVolume, targetVolume, timeCounter / duration);
			mixer.SetFloat(BGM_PARAM, LinearToDecibel(v));
			yield return null;
		}
		mixer.SetFloat(BGM_PARAM, LinearToDecibel(targetVolume));
		timeCounter = duration;
		if (fadeEndCallback != null)
		{
			fadeEndCallback();
			fadeEndCallback = null;
		}
		timeCounter = 0.0f;
		fadeCoroutine = null;
	}
	float LinearToDecibel(float linear)
	{
		return Mathf.Log10(Mathf.Clamp(linear, 0.0001f, 1f)) * 20f;
	}
}
