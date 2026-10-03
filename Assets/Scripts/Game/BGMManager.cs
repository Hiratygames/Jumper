using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class BGMManager : MonoBehaviour
{
	public enum State
	{
		Stop,
		FadingIn,
		Idle,
		FadingOut,
		Max
	}
	[SerializeField]
	private AudioMixer mixer;
	[SerializeField]
	private StageDatabase stageDatabase;
	private AudioSource audioSource;
	private BGMFader fader;
	AudioClip pendingClip;

	private State state;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		fader = GetComponent<BGMFader>();
	}
	private void Start()
	{
		state = State.Stop;
		fader.SetVolume(0.0f);
	}

	public void StopBGM(float duration)
	{
		FadeOut(duration);
	}
	public void ChangeSnapshot(string stageId)
	{
		StageData stageData = stageDatabase.Get(stageId);
		mixer.FindSnapshot(stageData.audioSnapshot).TransitionTo(1.0f);
		Debug.Log("ChangeSnapshot : " + stageData.audioSnapshot);
	}
	public void ChangeBGM(string stageId)
	{
		StageData stageData = stageDatabase.Get(stageId);
		switch (state)
		{
			case State.Stop:
				FadeIn(stageData.bgm, 1.0f);
				break;
			case State.FadingIn:
				if (audioSource.clip == stageData.bgm)
				{// 現在のBGMと同じなら何もしない
				}
				else
				{// 異なるならフェードをキャンセルしてフェードアウトに移行、pendingClipにbgmをセット
					float time = fader.CancelFade();
					FadeOutAndIn(stageData.bgm, time, 1.0f);
				}
				break;
			case State.Idle:
				if (audioSource.clip == stageData.bgm)
				{// 現在のBGMと同じなら何もしない
				}
				else
				{// 異なるならフェードを行いBGMを切り替える
					FadeOutAndIn(stageData.bgm, 1.0f, 1.0f);
				}
				break;
			case State.FadingOut:
				if (audioSource.clip == stageData.bgm)
				{// 現在のBGMと同じものなら、FadeOutを中止してFadeInに戻す

					float time = fader.CancelFade();
					FadeIn(stageData.bgm, time);
				}
				else
				{// 異なるなら、pendingClipを差し替えるだけ
					pendingClip = stageData.bgm;
				}
				break;
		}
	}

	private void FadeIn(AudioClip bgm, float duration)
	{
		state = State.FadingIn;
		pendingClip = null;
		if (audioSource.clip != bgm || !audioSource.isPlaying)
		{
			audioSource.clip = bgm;
			audioSource.Play();
		}
		fader.FadeBGM(0.0f, AudioManager.Get().GetBGMBaseLinear(), duration, () => { state = State.Idle; });
	}
	private void FadeOut(float duration)
	{
		state = State.FadingOut;
		pendingClip = null;
		fader.FadeBGM(AudioManager.Get().GetBGMBaseLinear(), 0.0f, duration, () => { audioSource.Stop(); state = State.Stop; });
	}
	private void FadeOutAndIn(AudioClip bgm, float outDuration, float inDuration)
	{
		state = State.FadingOut;
		pendingClip = bgm;
		fader.FadeBGM(AudioManager.Get().GetBGMBaseLinear(), 0.0f, outDuration, 
			() => { audioSource.Stop(); FadeIn(pendingClip, inDuration); });
	}
}
