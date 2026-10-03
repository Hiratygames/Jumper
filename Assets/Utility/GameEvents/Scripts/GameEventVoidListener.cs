using UnityEngine;
using UnityEngine.Events;

public class GameEventVoidListener : MonoBehaviour, IGameEventVoidListener
{
	[SerializeField] private GameEventVoid gameEvent;
	[SerializeField] private UnityEvent response;

	private void OnEnable()
	{
		gameEvent?.RegisterListener(this);
	}

	private void OnDisable()
	{
		gameEvent?.UnregisterListener(this);
	}

	public void OnEventRaised()
	{
		response?.Invoke();
	}

}
