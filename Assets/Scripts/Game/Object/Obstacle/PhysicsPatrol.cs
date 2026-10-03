using UnityEngine;
public class PhysicsPatrol : MonoBehaviour
{
	public Rigidbody2D rb;
	public Transform[] points;
	public float speed = 3f;
	int index = 0;

	void FixedUpdate()
	{
		Vector2 target = points[index].position;
		Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
		rb.MovePosition(newPos);

		if (Vector2.Distance(rb.position, target) < 0.05f)
			index = (index + 1) % points.Length;
	}
}
