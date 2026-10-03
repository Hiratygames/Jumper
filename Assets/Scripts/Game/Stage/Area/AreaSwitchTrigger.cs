using UnityEngine;
using UnityEngine.Events;

public class AreaSwitchTrigger : MonoBehaviour
{
	[SerializeField]
	private Area area1;
	[SerializeField]
	private Area area2;
	[SerializeField]
	private bool isVertival = true;
	[SerializeField]
	private UnityEvent<int> action;
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			// プレイヤーの座標が
			if (isVertival)
			{
				if (collision.transform.position.y >= transform.position.y)
				{
					SwitchArea2();

				}
				else
				{
					SwitchArea1();
				}
			}
			else
			{
				if (collision.transform.position.x >= transform.position.x)
				{
					SwitchArea2();

				}
				else
				{
					SwitchArea1();
				}
			}
		}
	}
	private void SwitchArea1()
	{
		if (area1 != null)
		{
			action.Invoke(area1.ID);
		}
	}
	private void SwitchArea2()
	{
		if (area2!= null)
		{
			action.Invoke(area2.ID);
		}
	}
}
