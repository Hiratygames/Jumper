using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
	[SerializeField]
	private StageDatabase stageDatabase;
	private SaveData saveData = new SaveData();
	public SaveData Data => saveData; // 読み取り専用で公開

	private void Awake()
	{
		if (HasSaveData())
		{
			Load();
		}
	}

	// プレイヤーの座標を設定する
	public bool SetPosition(Vector2 position)
	{
		saveData.player.x = position.x;
		saveData.player.y = position.y;
		return true;
	}
	public bool SetIsFlip(bool isFlip)
	{
		saveData.player.isFlip = isFlip;
		return true;
	}
	public bool SetJumpCount(int count)
	{
		if (count < 0)
		{
			return false;
		}
		if (count > 999999999)
		{
			saveData.player.jumpCount = 999999999;
		}
		else
		{
			saveData.player.jumpCount = count;
		}
		return true;
	}

	// 現在ステージを設定する
	public bool SetCurrentStageId(string stageId)
	{
		if (stageDatabase.Exists(stageId) == false)
		{
			return false;
		}
		saveData.world.curStageId = stageId;
		return true;
	}
	// 到達ステージを設定する
	public bool SetMaxReachedStageId(string stageId)
	{
		if (stageDatabase.Exists(stageId) == false)
		{
			return false;
		}
		int order = stageDatabase.GetOrder(stageId);
		if (order > stageDatabase.GetOrder(saveData.world.maxReachedStageId))
		{
			saveData.world.maxReachedStageId = stageId;
		}
		return true;
	}

	public bool SetPlayTime(float time)
	{
		if (time < 0.0f)
		{
			return false;
		}
		// 時間は100000分を上限とする
		if (time >= 60 * 100000)
		{
			saveData.system.playTime = 60 * 60 * 100000;
		}
		else
		{
			saveData.system.playTime = time;
		}
		return true;
	}

	public bool HasSaveData()
    {
        return PlayerPrefs.HasKey(SaveData.SaveKey);
	}
    public void Load()
	{
		SaveData.Load(SaveData.SaveKey, out saveData);
	}
    public void Save()
	{
		SaveData.Save(SaveData.SaveKey, saveData);
	}
	public void Delete()
	{
		saveData = new SaveData();
		PlayerPrefs.DeleteKey(SaveData.SaveKey);
		PlayerPrefs.Save();
	}
}
