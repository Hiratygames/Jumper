using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvent<T> : GameEventBase
{
	public void Raise(T value)
	{
		for (int i = listeners.Count - 1; i >= 0; i--)
		{
			if (listeners[i] is IGameEventListener<T> typed)
				typed.OnEventRaised(value);
		}
	}

	public void RegisterListener(IGameEventListener<T> listener)
		=> Register(listener);

	public void UnregisterListener(IGameEventListener<T> listener)
		=> Unregister(listener);

}
