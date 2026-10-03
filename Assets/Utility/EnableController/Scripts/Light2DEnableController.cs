using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Light2DEnableController : MonoBehaviour, IEnableController
{
	private Light2D l;
	private void Awake()
	{
		l = GetComponent<Light2D>();
	}
	public void SetEnable(bool enabled)
	{
		l.enabled = enabled;
	}
}
