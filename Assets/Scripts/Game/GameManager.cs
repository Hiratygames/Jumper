using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private Slime slime;
    [SerializeField]
    private Animator animFade;

	[SerializeField]
	private SaveDataManager saveDataManager;

	[SerializeField]
	private BGMManager bgmManager;
	[SerializeField]
	private TimeSynchronizer timeSynchronizer;
	[SerializeField]
	private GameObjectSwitchManager gameObjectSwitchManager;

	[SerializeField]
	private CinemachineBrain brain;
	[SerializeField]
	private CinemachineCamera gameClearEventCam;

	[SerializeField]
	private GameObject tapDisableUI;
	[SerializeField]
	private ClearTimeShower clearTimeShower;

	private AsyncOperation asyncLoad = null;
	private bool isClear = false;

	private void Awake()
	{
        animFade.SetTrigger("IrisIn");
		tapDisableUI.SetActive(false);
		AudioManager.Get();
		isClear = false;
	}
	private void Start()
	{
		if (saveDataManager.HasSaveData())
		{
			slime.Load(saveDataManager.Data.player);

			gameObjectSwitchManager.Load(saveDataManager.Data.player.jumpCount);
			timeSynchronizer.Load(saveDataManager.Data.system.playTime);
		}
		else
		{

		}
	}
	public void Update()
	{
		if (isClear)
		{
			return;
		}
		saveDataManager.SetPlayTime(saveDataManager.Data.system.playTime += Time.deltaTime);
	}

	public void PlayerStoppedCallback(Vector2 position)
	{
		if (isClear)
		{
			return;
		}
		saveDataManager.SetPosition(position);
		saveDataManager.SetIsFlip(slime.IsFlip);
		saveDataManager.Save();
	}
	public void StageChangedCallback(string stageId)
	{
		saveDataManager.SetCurrentStageId(stageId);
		saveDataManager.SetMaxReachedStageId(stageId);
	}
	public void JumpCallback()
	{
		saveDataManager.SetJumpCount(saveDataManager.Data.player.jumpCount + 1);
	}

	public void GameClearCallback()
	{
		if (isClear)
		{
			return;
		}
		isClear = true;
		tapDisableUI.SetActive(true);

		PlayerPrefs.SetInt("IsGameClear", 1);

		saveDataManager.SetPosition(slime.transform.position);
		saveDataManager.SetIsFlip(slime.IsFlip);
		saveDataManager.Save();
		clearTimeShower.OnClearCallback(saveDataManager.Data.system.playTime);

		StartCoroutine(GotoEndingAnimation());
		Invoke("GameClearMoveCamera", 3.0f);
	}

	private void GameClearMoveCamera()
	{
		brain.DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Styles.Linear, 3.0f);
		gameClearEventCam.Priority = 10;
		Invoke("GotoNextScene", 5.0f);
		Invoke("FadeClearTime",4.0f);
		bgmManager.StopBGM(5.0f);
	}
	private void FadeClearTime()
	{
		clearTimeShower.FadeOut(1.0f);
	}

	public void GotoTitle()
	{
		bgmManager.StopBGM(1.0f);
		StartCoroutine(GotoTitleAnimation());
	}
	private IEnumerator GotoTitleAnimation()
	{
		asyncLoad = SceneManager.LoadSceneAsync("Title");
		asyncLoad.allowSceneActivation = false;
		animFade.updateMode = AnimatorUpdateMode.UnscaledTime;
		animFade.SetTrigger("IrisOut");
		yield return new WaitForSecondsRealtime(1.0f);

		while (asyncLoad.progress < 0.9f)
		{
			yield return null;
		}
		// シーンを切り替える
		GotoNextScene();
	}
	private IEnumerator GotoEndingAnimation()
	{
		asyncLoad = SceneManager.LoadSceneAsync("Ending");
		asyncLoad.allowSceneActivation = false;

		while (asyncLoad.progress < 0.9f)
		{
			yield return null;
		}
	}
	private void GotoNextScene()
	{
		// シーンを切り替える
		Time.timeScale = 1;
		asyncLoad.allowSceneActivation = true;
	}
}
