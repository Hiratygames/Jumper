using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettingsUI : MonoBehaviour
{
    [SerializeField]
    private Slider masterVolumeSlider;
	[SerializeField]
	private Slider bgmVolumeSlider;
	[SerializeField]
	private Slider seVolumeSlider;
	[SerializeField]
	private Slider ambienceVolumeSlider;
	[SerializeField]
	private Slider systemVolumeSlider;

	[SerializeField]
    private AudioSource seAudioSource;

    [SerializeField]
    private GameEventVoid OnDecide;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		masterVolumeSlider.value = AudioManager.Get().GetMaster();
		bgmVolumeSlider.value = AudioManager.Get().GetBGM();
		seVolumeSlider.value = AudioManager.Get().GetSE();
		ambienceVolumeSlider.value = AudioManager.Get().GetAmbience();
		systemVolumeSlider.value = AudioManager.Get().GetSystem();
	}
	public void OnDecideCallback()
	{
		OnDecide.Raise();
	}
	public void OnDecideSECallback()
	{
		seAudioSource.Play();
	}
	public void OnMasterValueChanged()
	{
		AudioManager.Get().SetMaster((int)masterVolumeSlider.value);
	}
	public void OnBGMValueChanged()
    {
        AudioManager.Get().SetBGM((int)bgmVolumeSlider.value);
	}
	public void OnSEValueChanged()
	{
		AudioManager.Get().SetSE((int)seVolumeSlider.value);
	}
	public void OnAmbienceValueChanged()
	{
		AudioManager.Get().SetAmbience((int)ambienceVolumeSlider.value);
	}
	public void OnSystemValueChanged()
	{
		AudioManager.Get().SetSystem((int)systemVolumeSlider.value);
	}
}
