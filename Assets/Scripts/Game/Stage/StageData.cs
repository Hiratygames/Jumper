using UnityEngine;

[CreateAssetMenu()]
public class StageData : ScriptableObject
{
	public string stageId;
	public string displayName;
	public AudioClip bgm;
	public string audioSnapshot;
}
