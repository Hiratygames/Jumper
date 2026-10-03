using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
	private PlayableDirector playableDirector;
	[SerializeField]
	private Animator animFade;
	[SerializeField]
	private BGMFader bgmFader;
	private bool isLoading = false;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		playableDirector = GetComponent<PlayableDirector>();
		bgmFader.SetVolume(AudioManager.Get().GetBGMBaseLinear());
	}

    // Update is called once per frame
    void Update()
    {

	}
	public void StartEndingEvent()
	{
		playableDirector.Play();
	}
	public void GotoTitle()
	{
		if (isLoading)
		{
			return;
		}
		isLoading = true;
		// アニメーションスタート
		StartCoroutine(Animation());
		bgmFader.FadeBGM(AudioManager.Get().GetBGMBaseLinear(), 0.0f, 1.0f);
	}
	private IEnumerator Animation()
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Title");
		asyncLoad.allowSceneActivation = false;
		animFade.SetTrigger("IrisOut");
		yield return new WaitForSeconds(1.1f);


		while (asyncLoad.progress < 0.9f)
		{
			Debug.Log("Progress:" + asyncLoad.progress);
			yield return null;
		}
		Debug.Log("Load complete");
		// シーンを切り替える
		asyncLoad.allowSceneActivation = true;
	}
}
