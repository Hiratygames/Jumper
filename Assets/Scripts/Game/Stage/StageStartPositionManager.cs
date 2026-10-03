using UnityEngine;
using UnityEngine.InputSystem;

public class StageStartPositionManager : MonoBehaviour
{
    [SerializeField]
    private Slime slime;
	[SerializeField]
    private Transform[] transforms;
	private InputAction jumpAction;
    private int stageIndex = 0;
    public int Length {  get { return transforms.Length; } }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jumpAction = InputSystem.actions.FindAction("Jump");

	}

    // Update is called once per frame
    void Update()
    {
        if (jumpAction.WasPressedThisFrame())
        {
            stageIndex++;
            stageIndex %= transforms.Length;
            slime.transform.position = GetPosition(stageIndex);
		}
    }
    public Vector2 GetPosition(int idx)
    {
        if (idx < 0 || idx >= transforms.Length)
        {
            return transforms[0].position;
        }
        return transforms[idx].position;
    }
}
