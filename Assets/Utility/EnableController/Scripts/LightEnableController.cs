using UnityEngine;

public class LightEnableController : MonoBehaviour, IEnableController
{
	private Light l;
	private void Awake()
	{
		l = GetComponent<Light>();
	}
	public void SetEnable(bool enabled)
	{
		l.enabled = enabled;
	}
}
