using UnityEngine;

[CreateAssetMenu(menuName = "GameEvents/Void Event")]
public class GameEventVoid : GameEventBase
{
	public void Raise()
	{
		for (int i = listeners.Count - 1; i >= 0; i--)
		{
			if (listeners[i] is IGameEventVoidListener typed)
				typed.OnEventRaised();
		}
	}

	public void RegisterListener(IGameEventVoidListener listener)
		=> Register(listener);

	public void UnregisterListener(IGameEventVoidListener listener)
		=> Unregister(listener);
}
