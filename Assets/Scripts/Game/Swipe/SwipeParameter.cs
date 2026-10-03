using UnityEngine;

[CreateAssetMenu(menuName = "Slime/SwipeParameter")]
public class SwipeParameter : ScriptableObject
{
	public float jumpSwipeThreshold = 50.0f;
	public float jumpSwipeLimit = 300.0f;

	public bool IsSwipe(Vector2 vec)
	{
		return vec.magnitude >= jumpSwipeThreshold;
	}
	public float Raito(Vector2 vec)
	{
		return Mathf.Min(vec.magnitude - jumpSwipeThreshold, jumpSwipeLimit) / jumpSwipeLimit;
	}
}
