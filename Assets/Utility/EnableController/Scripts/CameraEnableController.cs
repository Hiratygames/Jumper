using Unity.Cinemachine;
using UnityEngine;

public class CameraEnableController : MonoBehaviour, IEnableController
{
    private CinemachineCamera cam;
	private void Awake()
	{
		cam = GetComponent<CinemachineCamera>();
	}

	public void SetEnable(bool enabled)
	{
		cam.enabled = enabled;
		cam.gameObject.SetActive(enabled);
	}
}
