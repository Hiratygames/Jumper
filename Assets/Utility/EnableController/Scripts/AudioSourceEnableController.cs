using UnityEngine;

public class AudioSourceEnableController : MonoBehaviour, IEnableController
{
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
	}
	public void SetEnable(bool enabled)
	{
		if (enabled)
		{
			audioSource.Play();
		}
		else
		{
			 audioSource.Stop();
		}
	}
}
