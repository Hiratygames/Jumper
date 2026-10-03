using System;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
	[SerializeField]
	private StageData stageData;
	public StageData Data { get { return stageData; } }
	[SerializeField]
	private StringEvent OnStageChanged;
	private Area[] areas;
	private int areaNum = 0;

	private List<int> playerEnteredAreas = new List<int>();

	public event Action<string> OnPlayerEntered;
	public event Action<string> OnPlayerExited;

	private IEnableController[] enableControllers;
	private void Awake()
	{
		enableControllers = GetComponentsInChildren<IEnableController>();
		areas = GetComponentsInChildren<Area>();
		areaNum = 0;
		foreach (Area area in areas)
		{
			area.SetID(areaNum++);
			area.OnPlayerEntered += HandlePlayerEnteredArea;
			area.OnPlayerExited += HandlePlayerExitedArea;
		}
	}

	public void SetEnable(bool enabled)
	{
		foreach (IEnableController controller in enableControllers)
		{
			controller.SetEnable(enabled);
		}
		if (enabled)
		{
			OnStageChanged.Raise(stageData.stageId);
		}
	}
	private void HandlePlayerEnteredArea(int areaID)
	{
		if (!playerEnteredAreas.Contains(areaID))
		{
			playerEnteredAreas.Add(areaID);
			if (playerEnteredAreas.Count == 1)
			{
				OnPlayerEntered.Invoke(stageData.stageId);
			}
		}
	}
	private void HandlePlayerExitedArea(int areaID)
	{
		playerEnteredAreas.Remove(areaID);
		if (playerEnteredAreas.Count == 0)
		{
			OnPlayerExited.Invoke(stageData.stageId);
		}
	}
}
