using Unity.Cinemachine;
using UnityEngine;

public class StageSelector : MonoBehaviour
{
	[SerializeField]
	private Stage[] stages;
	private int current = 0;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		current = 0;
		for (int i = 0; i < stages.Length; i++)
		{
			stages[i].SetEnable(false);
		}
		stages[current].SetEnable(true);
	}
	public void StageChange(int ID)
	{
		if (ID == current)
		{
			return;
		}
		stages[current].SetEnable(false);
		current = ID;
		stages[current].SetEnable(true);
	}
}
