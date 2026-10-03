using System;
using UnityEngine;

public class RoleDisplayController : MonoBehaviour
{
	[Serializable]
	public class Timing
	{
		public TextDisplayController text;
		public float timing;
		[NonSerialized]
		public bool isProgress = false;
	}
	[SerializeField]
	private Timing[] timings;
	private float timeCounter = 0.0f;
	private bool isStart = false;
	private bool isEnd = false;
	public bool IsEnd { get { return isEnd; } }
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		timeCounter = 0.0f;
	}

    // Update is called once per frame
    void Update()
	{
		if (!isStart)
		{
			return;
		}
		if (isEnd)
		{
			return;
		}
		timeCounter += Time.deltaTime;

		foreach (var timing in timings)
		{
			if (timing.isProgress)
			{
				continue;
			}
			if (timeCounter >= timing.timing)
			{
				timing.text.Display();
				timing.isProgress = true;
			}
		}
		bool b = true;
		foreach (var timing in timings)
		{
			if (!(timing.text.CurrentState == TextDisplayController.State.End))
			{
				b = false;
				break;
			}
		}
		if (b)
		{
			isEnd = true;
		}
	}

	public void Display()
	{
		isStart = true;
		isEnd = false;
		timeCounter = 0.0f;
	}
}
