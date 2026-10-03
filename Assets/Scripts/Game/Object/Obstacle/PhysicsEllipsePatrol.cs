using System;
using UnityEngine;
using UnityEngine.Timeline;

public class PhysicsEllipsePatrol : MonoBehaviour, ITimeSynchronize
{
    [SerializeField]
    private float x;
	[SerializeField]
	private float y;
	[SerializeField]
	private float speed;

	private Rigidbody2D rb;
	private Vector2 basePosition;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Awake()
    {
		rb = GetComponent<Rigidbody2D>();
		basePosition = transform.position;
	}

	public void Synchronize(float time)
	{
		float f = 2.0f * Mathf.PI * time * speed;

		Vector2 newPos = basePosition + new Vector2(Mathf.Cos(f) * x, Mathf.Sin(f) * y);
		rb.MovePosition(newPos);
	}
}
