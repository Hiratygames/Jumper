using UnityEngine;

public class SlimeSE : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip jumpAudio;
	[SerializeField]
	private AudioClip landingAudio;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayJump()
    {
        audioSource.PlayOneShot(jumpAudio);
	}
	public void PlayLanding()
	{
		audioSource.PlayOneShot(landingAudio);
	}
}
