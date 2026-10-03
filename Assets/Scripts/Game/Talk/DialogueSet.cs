using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "NPC/DialogueSet")]
public class DialogueSet : ScriptableObject
{
	public List<ProgressDialogue> dialogues;
}

[System.Serializable]
public class ProgressDialogue
{
	public string minProgressStageId;	// 対応する下限進行度
	public string maxProgressStageId;	// 対応する上限進行度
	public string[] firstTalks;			// 初回会話（長台詞）
	public string[] repeatTalk;     // 2回目以降（短台詞）
}
