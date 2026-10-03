using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class SaveData
{
	public const string SaveKey = "SaveData000";
	public PlayerData player = new PlayerData();
	public WorldData world = new WorldData();
	public SystemData system = new SystemData();

	public static void Save(string key, SaveData saveData)
	{
		string json = JsonUtility.ToJson(saveData);
#if UNITY_WEBGL
		// unityroomではファイル保存は使用できないようなので
		// PlayerPrefsを使用する
		PlayerPrefs.SetString(key, json);
		PlayerPrefs.Save();
#else
		File.WriteAllText(path, json);
#endif
	}
	public static void Load(string key, out SaveData saveData)
	{
#if UNITY_WEBGL
		// unityroomではファイル保存は使用できないようなので
		// PlayerPrefsを使用する
		string json = PlayerPrefs.GetString(key);
#else
		string json = File.ReadAllText(path);
#endif
		saveData = JsonUtility.FromJson<SaveData>(json);
	}
}

[System.Serializable]
public class PlayerData
{
	public float x;
	public float y;
	public bool isFlip;
	public int jumpCount;
}

[System.Serializable]
public class WorldData
{
	public string curStageId;
	public string maxReachedStageId;
}

[System.Serializable]
public class SystemData
{
	public float playTime;
}
