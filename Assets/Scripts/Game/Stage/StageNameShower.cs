using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class StageNameShower : MonoBehaviour
{
	[SerializeField]
	private StageDatabase stageDatabase;
    private TMPFade fade;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        fade = GetComponent<TMPFade>();
    }
    public void Show(string stageId)
	{
		fade.CancelFade();

		StageData stageData = stageDatabase.Get(stageId);
		fade.SetText("Å`" + stageData.displayName + "Å`");
		fade.SetColor(Color.black);
		fade.StartFade();
	}
    public void Cancel()
	{
		fade.CancelFade();
	}
}
