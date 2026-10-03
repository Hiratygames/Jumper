using UnityEngine;
using UnityEngine.InputSystem;

public class Slime : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 6.0f;
    [SerializeField]
    private SwipeParameter swipeParam;

    [SerializeField]
    private SlimeScaler scaler;

    [SerializeField]
    private GroundChecker groundChecker;

	Rigidbody2D rb;
    FacingFlipper flipper;
    public bool IsFlip { get { return flipper.IsRight; } }
    Vector2 lastVelocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        flipper = GetComponentInChildren<FacingFlipper>();
    }


	void FixedUpdate()
    {
        lastVelocity = rb.linearVelocity;
        if (Mathf.Abs(lastVelocity.x) >= 1.0f)
        {
            flipper.FacingFlip(lastVelocity.x >= 0.0f);
        }
    }

    public void Load(PlayerData playerData)
    {
        transform.position = new Vector3(playerData.x, playerData.y, 0.0f);
        flipper.FacingFlip(playerData.isFlip);
    }

	public void OnSwipeStart(Vector2 start)
    {
        scaler.OnSwipeStart(start);
	}
	public void OnSwipePerform(Vector2 vec)
	{
        scaler.OnSwipePerform(vec);
        if (vec.magnitude > swipeParam.jumpSwipeThreshold)
        {
            flipper.FacingFlip(-vec.x >= 0.0f);
        }
	}
	public void OnSwipeEnd(Vector2 vec)
	{
        scaler.OnSwipeEnd();
        float raito = swipeParam.Raito(vec);
        Jump(-vec.normalized * jumpForce * raito);
	}
    public void OnJumpCancel()
    {
        scaler.OnSwipeEnd();
    }

    public void OnLandingCallback()
    {
        float speed = new Vector2(0.0f, lastVelocity.y).magnitude;
		scaler.OnLanding(speed);
    }
    public void OnFlyingCallback()
    {
        scaler.OnFlying();
    }

	private void Jump(Vector3 vec)
    {
        rb.AddForce(vec, ForceMode2D.Impulse);
	}

    private void JumpCharge()
    {

    }
}
