using UnityEngine;

public class RendererEnableController : MonoBehaviour, IEnableController
{
	private Renderer r;
	private void Awake()
	{
		r = GetComponent<Renderer>();
	}
	public void SetEnable(bool enabled)
	{
		r.enabled = enabled;
	}
}
