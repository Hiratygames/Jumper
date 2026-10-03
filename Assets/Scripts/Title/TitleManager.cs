using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
	[SerializeField]
	private SaveDataManager saveDataManager;
	[SerializeField]
	private BGMFader bgmFader;
	[SerializeField]
	private Animator anim;
	[SerializeField]
	private GameObject gameStartUI;
	[SerializeField]
	private GameObject clearFlower;

	private bool isLoading = false;

	[SerializeField]
	private GameEventVoid OnDecide;

	private void Start()
	{
		clearFlower.SetActive(PlayerPrefs.GetInt("IsGameClear", 0) == 1);
		AudioManager.Get();
		gameStartUI.SetActive(false);
		bgmFader.SetVolume(AudioManager.Get().GetBGMBaseLinear());
	}
	public void TapToStart()
	{
		if (saveDataManager.HasSaveData())
		{
			ShowPlayGameMenu();
		}
		else
		{
			GotoGame();
		}
	}
	public void ShowPlayGameMenu()
	{
		gameStartUI.SetActive(true);
		OnDecide.Raise();
	}
	public void HidePlayGameMenu()
	{
		gameStartUI.SetActive(false);
		OnDecide.Raise();
	}
	public void PushDeleteButton()
	{
		saveDataManager.Delete();
		HidePlayGameMenu();
	}
	public void GotoGame()
	{
		if (isLoading)
		{
			return;
		}
		isLoading = true;
		OnDecide.Raise();
		bgmFader.FadeBGM(AudioManager.Get().GetBGMBaseLinear(), 0.0f, 1.0f);
		// アニメーションスタート
		StartCoroutine(Animation());
	}
	private IEnumerator Animation()
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game");
		asyncLoad.allowSceneActivation = false;
		anim.SetTrigger("IrisOut");
		yield return new WaitForSeconds(1.0f);


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
