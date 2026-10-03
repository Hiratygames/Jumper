using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventBase : ScriptableObject
{
	protected readonly List<object> listeners = new();

	protected void Register(object listener)
	{
		if (!listeners.Contains(listener))
			listeners.Add(listener);
	}

	protected void Unregister(object listener)
	{
		if (listeners.Contains(listener))
			listeners.Remove(listener);
	}


}
