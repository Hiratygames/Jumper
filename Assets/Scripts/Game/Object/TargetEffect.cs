using UnityEngine;

public class TargetEffect : MonoBehaviour
{
    private float x = 1.0f;
	private float y = 0.2f;
	private float time = 4.0f;
    private float timer = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
	{
        timer += Time.deltaTime;
        while (timer >= time)
        {
            timer -= time;
        }
		float f = 2.0f * Mathf.PI * (timer / time);

		Vector2 newPos = new Vector2(Mathf.Cos(f) * x, Mathf.Sin(f * 2) * y);
        transform.localPosition = newPos;
	}
}
