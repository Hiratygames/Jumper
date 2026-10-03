using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Timeline;

[CreateAssetMenu(menuName = "Audio/AudioSettings")]
public class AudioVolumeSettings : ScriptableObject
{
	public const int Max = 10;
	[Range(0, Max)] public int master = Max;
	[Range(0, Max)] public int bgm = Max;
	[Range(0, Max)] public int se = Max;
	[Range(0, Max)] public int ambience = Max;
	[Range(0, Max)] public int system = Max;
	private bool isLoaded = false;
	public bool IsLoaded {  get { return isLoaded; } }

	public void Load()
	{
		master = PlayerPrefs.GetInt("MasterVolume", Max);
		bgm = PlayerPrefs.GetInt("BGMVolume", Max);
		se = PlayerPrefs.GetInt("SEVolume", Max);
		ambience = PlayerPrefs.GetInt("AmbienceVolume", Max);
		system = PlayerPrefs.GetInt("SystemVolume", Max);
		isLoaded = true;
	}
	public void Save()
	{
		PlayerPrefs.SetInt("MasterVolume", master);
		PlayerPrefs.SetInt("BGMVolume", bgm);
		PlayerPrefs.SetInt("SEVolume", se);
		PlayerPrefs.SetInt("AmbienceVolume", ambience);
		PlayerPrefs.SetInt("SystemVolume", system);
		PlayerPrefs.Save();
	}

	public float ToLinear(int step)
	{
		return step / (float)Max; // 0〜1に変換
	}
}
