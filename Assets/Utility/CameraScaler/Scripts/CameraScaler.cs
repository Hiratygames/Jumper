using UnityEngine;
using Unity.Cinemachine;

public class CameraScaler : MonoBehaviour
{
	// 想定解像度（基準）
	[SerializeField]
	public float referenceWidth = 1080f;
	[SerializeField]
	public float referenceHeight = 1920f;

	private CinemachineCamera cam;

	void Start()
	{
		cam = GetComponent<CinemachineCamera>();

		float targetAspect = referenceWidth / referenceHeight;
		float currentAspect = (float)Screen.width / Screen.height;
		Debug.Log("targetAspect:" + targetAspect + ", currentAspect:" + currentAspect);

		// 現在のアスペクト比が基準より縦長なら、縦方向を広げる
		if (currentAspect < targetAspect)
		{
			float scale = targetAspect / currentAspect;
			cam.Lens.OrthographicSize *= scale;
		}
	}
}
