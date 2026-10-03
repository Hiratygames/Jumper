using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightWaver : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve animCurve;
    [SerializeField]
    private float time;

    private Light2D light2D;
    private float baseIntensity;

    private float curTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        light2D = GetComponent<Light2D>();
		baseIntensity = light2D.intensity;
        curTime = 0.0f;

	}

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime >= time)
        {
            curTime -= time;
        }
        light2D.intensity = baseIntensity * animCurve.Evaluate(curTime / time);
    }
}
