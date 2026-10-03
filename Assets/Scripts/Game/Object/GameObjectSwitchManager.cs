using UnityEngine;

public class GameObjectSwitchManager : MonoBehaviour
{
    private GameObjectSwitcher[] gameObjectSwitchers;

    public void Load(int jumpCount)
    {
        if (jumpCount % 2 == 1)
        {
            SwitchEnabled();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
		gameObjectSwitchers = GetComponentsInChildren<GameObjectSwitcher>(true);
	}
    public void SwitchEnabled()
    {
        foreach (GameObjectSwitcher switcher in gameObjectSwitchers)
        {
            switcher.SwitchEnabled();
        }
    }
}
