using System.Collections.Generic;
using UnityEngine;

public class StageSwitcher : MonoBehaviour
{
	[SerializeField]
	private StageDatabase stageDatabase;
	private List<Stage> stages;
	private int stageNum = 0;
	private List<string> enteredStages = new List<string>();
	private string current = "";
	private void Awake()
	{
		stages = new List<Stage>(GetComponentsInChildren<Stage>());
		current = "";
		stageNum = 0;
		foreach (Stage stage in stages)
		{
			stage.OnPlayerEntered += OnEnterStage;
			stage.OnPlayerExited += OnExitStage;
		}
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		for (int i = 0; i < stages.Count; i++)
		{
			stages[i].SetEnable(false);
		}
	}
	public void OnEnterStage(string stageId)
	{
		Debug.Log("OnEnterStage " + stageId);
		if (!enteredStages.Contains(stageId))
		{
			enteredStages.Add(stageId);
			if (enteredStages.Count == 1)
			{
				SetCurrentStage(stageId);
			}
		}
	}
	public void OnExitStage(string stageId)
	{
		Debug.Log("OnExitStage " + stageId);
		enteredStages.Remove(stageId);
		GetStage(stageId).SetEnable(false);
		if (enteredStages.Count == 1)
		{
			SetCurrentStage(enteredStages[0]);
		}
		if (enteredStages.Count == 0)
		{
			current = "";
		}
	}
	private void SetCurrentStage(string stageId)
	{
		if (stageId == current)
		{
			return;
		}
		GetStage(stageId).SetEnable(true);
		current = stageId;
	}
	private Stage GetStage(string stageId)
	{
		return stages.Find(s => s.Data.stageId == stageId);
	}
}
