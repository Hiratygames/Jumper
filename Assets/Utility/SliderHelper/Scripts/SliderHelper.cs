using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderHelper : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	private Slider slider;

	[SerializeField]
	private UnityEvent<int> OnReleaseCallback;

	private bool isDragging = false;
	private int latestValue;

	private void Awake()
	{
		slider = GetComponent<Slider>();
		slider.onValueChanged.AddListener(OnSliderValueChanged);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		isDragging = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		isDragging = false;
		// リリース時にだけコールバックを実行
		OnSliderReleased(latestValue);
	}

	private void OnSliderValueChanged(float value)
	{
		latestValue = (int)value;
		// ドラッグ中は何もしない
	}

	private void OnSliderReleased(int value)
	{
		Debug.Log("Released: " + value);
		// ← 本来の処理をここに書く
		OnReleaseCallback.Invoke(value);
	}
}
