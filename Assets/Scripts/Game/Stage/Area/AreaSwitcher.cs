using Unity.Cinemachine;
using UnityEngine;

public class AreaSwitcher : MonoBehaviour
{
	[SerializeField]
	private Area[] areas;
	private int current = 0;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
	}
	public void CameraChange(int ID)
	{
	}
}
