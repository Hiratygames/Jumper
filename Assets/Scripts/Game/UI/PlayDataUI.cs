using TMPro;
using UnityEngine;

public class PlayDataUI : MonoBehaviour
{
    [SerializeField]
    private StageDatabase stageDatabase;
    [SerializeField]
    private SaveDataManager saveDataManager;

    [SerializeField]
    private TextMeshProUGUI textCurStage;
	[SerializeField]
	private TextMeshProUGUI textMaxReachedStage;
    [SerializeField]
    private TextMeshProUGUI textPlayTime;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void OnEnable()
	{
		textCurStage.text = GetStageName(saveDataManager.Data.world.curStageId);
		textMaxReachedStage.text = GetStageName(saveDataManager.Data.world.maxReachedStageId);
		textPlayTime.text = GetPlayTimeString(saveDataManager.Data.system.playTime);
	}

    private string GetStageName(string stageId)
	{
		if (stageId == "" || stageId == null)
		{
			return "-";
		}
		return stageDatabase.Get(stageId).displayName;
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
