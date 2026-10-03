using UnityEngine;

public class TimeSynchronizer : MonoBehaviour
{
    private float timeCounter = 0.0f;
    private ITimeSynchronize[] timeSynchronizes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        timeSynchronizes = GetComponentsInChildren<ITimeSynchronize>();
    }

    public void Load(float time)
    {
		timeCounter = time;

	}

    // Update is called once per frame
    void FixedUpdate()
    {
        timeCounter += Time.fixedDeltaTime;
        foreach (var t in timeSynchronizes)
        {
            t.Synchronize(timeCounter);
        }
    }
}
