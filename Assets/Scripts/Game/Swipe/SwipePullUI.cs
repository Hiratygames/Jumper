using UnityEngine;
using UnityEngine.UI;

public class SwipePullUI : MonoBehaviour
{
	[SerializeField]
	private SwipeParameter swipeParam;
	[SerializeField]
	private Vector2 scaleMax;
    private Image imageArrow;
	private RectTransform rectTransform;
	private Vector2 startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        imageArrow = GetComponent<Image>();
		rectTransform = GetComponent<RectTransform>();
		imageArrow.enabled = false;
	}


	public void OnSwipeStart(Vector2 start)
	{
		startPos = start;
		rectTransform.anchoredPosition = start;
		imageArrow.enabled = true;
		rectTransform.localScale = Vector2.zero;

	}
	public void OnSwipePerform(Vector2 vec)
	{
		if (!swipeParam.IsSwipe(vec))
		{
			rectTransform.localScale = Vector2.zero;
			return;// ÉXÉèÉCÉvÇ∆ÇµÇƒàµÇÌÇ»Ç¢
		}
		float raito = swipeParam.Raito(vec);
		rectTransform.localScale = Vector2.Lerp(Vector2.one, scaleMax, raito);
		rectTransform.rotation = Quaternion.AngleAxis(AngleFromDown(vec.normalized), Vector3.forward);
	}
	public void OnSwipeEnd()
	{
		imageArrow.enabled = false;
	}
	float AngleFromDown(Vector2 dir)
	{
		float angle = Mathf.Atan2(dir.x, -dir.y) * Mathf.Rad2Deg;
		if (angle < 0) angle += 360f; return angle;
	}
}
