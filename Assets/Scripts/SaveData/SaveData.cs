
[System.Serializable]
public class SaveData
{
	public PlayerData player = new PlayerData();
	public WorldData world = new WorldData();
	public SystemData system = new SystemData();
}

[System.Serializable]
public class PlayerData
{
	public float x;
	public float y;
	public bool isFlip;
	public int jumpCount;
}

[System.Serializable]
public class WorldData
{
	public string curStageId;
	public string maxReachedStageId;
}

[System.Serializable]
public class SystemData
{
	public float playTime;
}
