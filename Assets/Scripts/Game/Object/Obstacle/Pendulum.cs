using UnityEngine;
public class Pendulum : MonoBehaviour, ITimeSynchronize
{
	public float angle = 45f;     // Å‘åŠp“x
	public float speed = 2f;      // U‚èq‚Ì‘¬‚³
	public void Synchronize(float time)
	{
		float z = angle * Mathf.Sin(time * speed);
		transform.localRotation = Quaternion.Euler(0, 0, z);
	}
}
