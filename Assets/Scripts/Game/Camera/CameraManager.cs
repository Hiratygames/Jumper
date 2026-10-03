using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	private Dictionary<int, CameraController> cams = new Dictionary<int, CameraController>();
	private int camNum = 0;
	private List<int> enteredCams = new List<int>();
	private int curCamID = -1;
	private CameraController[] cameras;
	private void Awake()
	{
		cameras = GetComponentsInChildren<CameraController>();
		curCamID = -1;
		foreach (CameraController c in cameras)
		{
			c.SetID(camNum++);
			c.OnPlayerEntered += OnEnterCamera;
			c.OnPlayerExited += OnExitCamera;
		}
	}
	public void OnEnterCamera(int id)
	{
		Debug.Log("OnEnterCamera " + id);
		if (!enteredCams.Contains(id))
		{
			enteredCams.Add(id);
			if (enteredCams.Count == 1)
			{
				SetCurrentCamera(id);
			}
		}
	}
	public void OnExitCamera(int id)
	{
		Debug.Log("OnExitCamera " + id);
		enteredCams.Remove(id);
		cameras[id].SetCameraPriority(0);
		if (enteredCams.Count == 1)
		{
			SetCurrentCamera(enteredCams[0]);
		}
	}
	private void SetCurrentCamera(int id)
	{
		cameras[id].SetCameraPriority(1);
		curCamID = id;
	}
}
