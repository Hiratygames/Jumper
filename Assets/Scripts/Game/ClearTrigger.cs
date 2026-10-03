using UnityEngine;

public class ClearTrigger : MonoBehaviour
{
    [SerializeField]
    private GameEventVoid OnGameClear;
    bool isCleared = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (isCleared)
        {
            return;
        }
		if (collision.gameObject.tag == "Player")
        {
            isCleared = true;
			// ÉNÉäÉA
			OnGameClear.Raise();

		}
	}
}
