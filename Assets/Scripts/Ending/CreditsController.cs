using System;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    [Serializable]
    public class Timing
    {
        public RoleDisplayController display;
        public float timing;
        [NonSerialized]
        public bool isProgress = false;
    }
    [SerializeField]
    private Transform start;
	[SerializeField]
	private Transform end;
	[SerializeField]
	private float time;
    [SerializeField]
    private bool isScroll;

    [SerializeField]
    private Timing[] timings;

    [SerializeField]
    private GameEventVoid OnCreditsFinish;

	private float timeCounter = 0.0f;
    private bool isEnd = false;
    public bool IsEnd { get { return isEnd; } }
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        if (isScroll)
        {
            transform.position = start.position;
        }
		timeCounter = 0.0f;
	}

    // Update is called once per frame
    void Update()
    {
        if (isEnd)
        {
            return;
        }
        timeCounter += Time.deltaTime;

        if (isScroll)
        {
            if (timeCounter >= time)
            {
                transform.position = end.position;
                isEnd = true;
				OnCreditsFinish.Raise();
				return;
            }
            transform.position = Vector3.Lerp(start.position, end.position, timeCounter / time);
        }
        else
        {
            foreach (var timing in timings)
            {
                if (timing.isProgress)
                {
                    continue;
                }
                if (timeCounter >= timing.timing)
                {
                    timing.display.Display();
                    timing.isProgress = true;
				}
            }
            bool b = true;
            foreach (var timing in timings)
            {
                if (!(timing.display.IsEnd))
                {
                    b = false;
                    break;
                }
            }
            if (b)
            {
                isEnd = true;
                OnCreditsFinish.Raise();

			}
        }
    }
}
