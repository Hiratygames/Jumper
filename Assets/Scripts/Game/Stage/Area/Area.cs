using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Area : MonoBehaviour
{
    private int id;
	private CameraController cam;
	public event Action<int> OnPlayerEntered;
	public event Action<int> OnPlayerExited;
	[SerializeField]
	private Slime slime;
	public int ID {  get { return id; } }
	public void SetID(int id)
	{
		this.id = id;
	}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
		cam = GetComponentInChildren<CameraController>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("PlayerCameraCollider"))
		{
			cam.NotifyAreaEntered();
			OnPlayerEntered.Invoke(ID);
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("PlayerCameraCollider"))
		{
			cam.NotifyAreaExited();
			OnPlayerExited.Invoke(ID);
		}
	}
}
