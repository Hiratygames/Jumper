using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance { get; private set; }
	public static AudioManager Get()
	{
		if (Instance != null) return Instance;
		var prefab = Resources.Load<AudioManager>("AudioManager");
		var obj = Instantiate(prefab);
		return Instance;
	}

	[SerializeField] AudioMixer mixer;
	[SerializeField] AudioVolumeSettings settings;

	const string MASTER = "MasterVolume";
	const string BGM = "BGMVolume";
	const string SE = "SEVolume";
	const string AMBIENCE = "AmbienceVolume";
	const string SYSTEM = "SystemVolume";

	private void Awake()
	{
		// シーンから切り離す
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);

		// 設定のロード
		if (settings.IsLoaded == false)
		{
			settings.Load();
		}
		ApplyAll();
	}

	public void ApplyAll()
	{
		SetVolume(MASTER, settings.ToLinear(settings.master));
		SetVolume(BGM, settings.ToLinear(settings.bgm));
		SetVolume(SE, settings.ToLinear(settings.se));
		SetVolume(AMBIENCE, settings.ToLinear(settings.ambience));
		SetVolume(SYSTEM, settings.ToLinear(settings.system));
	}

	public void SetMaster(int step)
	{
		settings.master = step;
		SetVolume(MASTER, settings.ToLinear(step));
	}
	public int GetMaster()
	{
		return settings.master;
	}

	public void SetBGM(int step)
	{
		settings.bgm = step;
		SetVolume(BGM, settings.ToLinear(step));
	}
	public int GetBGM()
	{
		return settings.bgm;
	}
	public float GetBGMBaseLinear()
	{
		return settings.bgm / (float)AudioVolumeSettings.Max;
	}

	public void SetSE(int step)
	{
		settings.se = step;
		SetVolume(SE, settings.ToLinear(step));
	}
	public int GetSE()
	{
		return settings.se;
	}

	public void SetAmbience(int step)
	{
		settings.ambience = step;
		SetVolume(AMBIENCE, settings.ToLinear(step));
	}
	public int GetAmbience()
	{
		return settings.ambience;
	}

	public void SetSystem(int step)
	{
		settings.system = step;
		SetVolume(SYSTEM, settings.ToLinear(step));
	}
	public int GetSystem()
	{
		return settings.system;
	}

	void SetVolume(string param, float linear)
	{
		mixer.SetFloat(param, LinearToDecibel(linear));
	}

	float LinearToDecibel(float linear)
	{
		return Mathf.Log10(Mathf.Clamp(linear, 0.0001f, 1f)) * 20f;
	}
}
