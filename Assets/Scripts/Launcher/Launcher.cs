using UnityEngine;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
	{
		AudioManager.Get();
		SceneManager.LoadScene("Title");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
