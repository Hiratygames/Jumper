using UnityEngine;

public class GameObjectSwitcher : MonoBehaviour
{
	public void SwitchEnabled()
	{
		gameObject.SetActive(!gameObject.activeInHierarchy);
	}
}
