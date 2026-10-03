using UnityEngine;

public interface IGameEventListener<T>
{
	void OnEventRaised(T value);
}

public interface IGameEventVoidListener
{
	void OnEventRaised();
}
