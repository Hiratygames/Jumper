using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class StageDatabase : ScriptableObject
{
	public List<StageData> stages;

	public bool Exists(string id)
	{
		return stages.Exists(s => s.stageId == id);
	}
	public StageData Get(string id)
	{
		return stages.Find(s => s.stageId == id);
	}
	public int GetOrder(string id)
	{
		return stages.FindIndex(s => s.stageId == id);
	}

	public StageData GetFirstStage()
	{
		return stages[0];
	}
}
