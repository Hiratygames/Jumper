using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
	[SerializeField]
	private GameObject objMenu;
	[SerializeField]
	private VolumeSettingsUI volumeSettingsUI;
	[SerializeField]
	private GameEventVoid OnDecide;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		objMenu.SetActive(false);
		volumeSettingsUI.gameObject.SetActive(false);
	}

	public void ShowMenu()
	{
		Time.timeScale = 0;
		objMenu.SetActive(true);
		OnDecide.Raise();
	}
	public void HideMenu()
	{
		Time.timeScale = 1;
		objMenu.SetActive(false);
		OnDecide.Raise();
	}

	public void ShowVolumeSettingsUI()
	{
		Time.timeScale = 0;
		objMenu.SetActive(false);
		volumeSettingsUI.gameObject.SetActive(true);
		OnDecide.Raise();
	}
	public void HideVolumeSettingsUI()
	{
		Time.timeScale = 1;
		volumeSettingsUI.gameObject.SetActive(false);
		OnDecide.Raise();
	}
}
