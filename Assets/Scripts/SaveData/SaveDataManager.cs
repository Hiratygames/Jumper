using UnityEngine;
using System.IO;

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
	private string GetSavePath(int idx)
	{
#if UNITY_WEBGL
		return "SaveData" + idx.ToString("D3");
#else
		return Application.persistentDataPath + "/" + "SaveData" + idx.ToString("D3") + ".data";
#endif
	}
	public bool HasSaveData()
	{
#if UNITY_WEBGL
		return PlayerPrefs.HasKey(GetSavePath(0));
#else
		return File.Exists(GetSavePath(0));
#endif
	}
	public void Load()
	{
#if UNITY_WEBGL
		string json = PlayerPrefs.GetString(GetSavePath(0));
		saveData = JsonUtility.FromJson<SaveData>(json);
#else
		string json = File.ReadAllText(GetSavePath(0));
		saveData = JsonUtility.FromJson<SaveData>(json);
#endif
	}
	public void Save()
	{
#if UNITY_WEBGL
		string json = JsonUtility.ToJson(saveData);
		PlayerPrefs.SetString(GetSavePath(0), json);
		PlayerPrefs.Save();
#else
		string json = JsonUtility.ToJson(saveData);
		File.WriteAllText(GetSavePath(0), json);
#endif
	}
	public void Delete()
	{
#if UNITY_WEBGL
		saveData = new SaveData();
		PlayerPrefs.DeleteKey(GetSavePath(0));
		PlayerPrefs.Save();
#else
		saveData = new SaveData();
		File.Delete(GetSavePath(0));
#endif
	}
}
