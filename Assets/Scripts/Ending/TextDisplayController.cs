using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TextDisplayController : MonoBehaviour
{
    public enum State
    {
        Wait,
        Appear,
        Display,
        Disappear,
        End,
        Max
    };
    [SerializeField]
    private float appearTime;
	[SerializeField]
	private float displayTime;
	[SerializeField]
	private float disappearTime;
    private float timeCounter;
	private TextMeshPro text;
    private State state = State.Wait;
    public State CurrentState { get { return state; } }
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.alpha = 0.0f;
        timeCounter = 0;
		state = State.Wait;
	}

    // Update is called once per frame
    void Update()
    {
        if (!(state == State.Appear || state == State.Display || state == State.Disappear))
        {
            return;
        }
        timeCounter += Time.deltaTime;
        state = CheckState(state, timeCounter);
        text.alpha = CalcAlpha(state, timeCounter);
    }

    public void Display()
	{
		text.alpha = 0.0f;
		timeCounter = 0;
		state = State.Appear;
	}
    private State CheckState(State state, float timeCounter)
    {
        switch (state)
        {
            case State.Appear:
                if (timeCounter >= appearTime)
                {
                    return State.Display;
                }
                break;
            case State.Display:
                if (timeCounter >= appearTime + displayTime)
                {
                    return State.Disappear;
                }
                break;
            case State.Disappear:
                if (timeCounter >= appearTime + displayTime + disappearTime)
                {
                    return State.End;
                }
                break;
        }
        return state;
    }
    private float CalcAlpha(State state, float timeCounter)
	{
		switch (state)
		{
			case State.Appear:
				return Mathf.Lerp(0.0f, 1.0f, timeCounter / appearTime);
			case State.Display:
				return 1.0f;
			case State.Disappear:
				return Mathf.Lerp(1.0f, 0.0f, (timeCounter - appearTime - displayTime) / disappearTime);
		}
        return 0.0f;
	}
}
