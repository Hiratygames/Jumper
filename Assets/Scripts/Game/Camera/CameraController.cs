using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineCamera cam;
	private int id;
	public int ID { get { return id; } }

	public event Action<int> OnPlayerEntered;
	public event Action<int> OnPlayerExited;
	private void Awake()
	{
		cam = GetComponent<CinemachineCamera>();
		cam.Follow = GameObject.FindGameObjectWithTag("Player").transform;
	}
	public void SetID(int id)
	{
		this.id = id;
	}
	public void NotifyAreaEntered()
	{
		OnPlayerEntered.Invoke(id);
	}
	public void NotifyAreaExited()
	{
		OnPlayerExited.Invoke(id);
	}
	public void SetEnable(bool enabled)
	{
		cam.Priority = enabled ? 1 : 0;
	}
	public void SetCameraPriority(int priority)
	{
		cam.Priority = priority;
		cam.ForceCameraPosition(cam.Follow.transform.position, Quaternion.identity);
	}
}
