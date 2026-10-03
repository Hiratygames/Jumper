using System.Linq;
using TMPro;
using UnityEngine;

public class TalkEvent00 : MonoBehaviour
{
	[SerializeField]
	private TalkController talkController;
	[SerializeField]
	private SaveDataManager saveDataManager;
	[SerializeField]
	private StageDatabase stageDatabase;
	[SerializeField]
	private DialogueSet dialogueSet;

	private ProgressDialogue currentDialogue;

	private int currentString = 0;
	private bool isHeard = false;
	private bool isTalking = false;
	private int currentStageOrder = 0;

	// Player‚ª“ü‚Á‚Ä‚«‚½‚Æ‚«
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (isTalking)
		{
			return;
		}
		if (collision.gameObject.CompareTag("PlayerCameraCollider"))
		{
			currentString = 0;
			string stageId = saveDataManager.Data.world.maxReachedStageId;
			int stageOrder = stageDatabase.GetOrder(stageId);
			if (currentStageOrder < stageOrder)
			{
				isHeard = false;
				currentStageOrder = stageOrder;
			}
			currentDialogue = dialogueSet.dialogues.First(d => stageDatabase.GetOrder(d.minProgressStageId) >= stageOrder && stageDatabase.GetOrder(d.maxProgressStageId) <= stageOrder);
			talkController.OnFinish += Next;
			talkController.Play(GetString(currentString));
			isTalking = true;
		}
	}
	//private void OnTriggerExit2D(Collider2D collision)
	//{
	//	if (collision.gameObject.CompareTag("PlayerCameraCollider"))
	//	{
	//		talkController.Stop();
	//		talkController.OnFinish -= Next;
	//		isHeard = true;
	//	}
	//}

	private string GetString(int index)
	{
		return isHeard ? currentDialogue.repeatTalk[index] : currentDialogue.firstTalks[index];
	}
	private int GetStringNum()
	{
		return isHeard ? currentDialogue.repeatTalk.Length : currentDialogue.firstTalks.Length;
	}

	private void Next()
	{
			currentString++;
		if (currentString < GetStringNum())
		{
			talkController.Play(GetString(currentString));
		}
		else
		{
			talkController.OnFinish -= Next;
			isHeard = true;
			isTalking = false;
		}
	}
}
