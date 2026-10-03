using UnityEngine;

public class StageSelectTrigger : MonoBehaviour
{
	[SerializeField]
	private int ID = 0;
	[SerializeField]
	private IntEvent OnStageSelected;

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			// プレイヤーの座標が
			if (collision.transform.position.y >= transform.position.y)
			{
				OnStageSelected.Raise(ID + 1);

			}
			else
			{
				OnStageSelected.Raise(ID);
			}
		}
	}
}
