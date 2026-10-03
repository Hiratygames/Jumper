using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BoxOutlineDrawer : MonoBehaviour
{
	[SerializeField] private Material lineMaterial;
	[SerializeField] private float lineWidth = 0.05f;

    private BoxCollider2D box;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Awake()
	{
		box = GetComponent<BoxCollider2D>();
		GenerateOutline();
	}

	public void GenerateOutline()
	{
		// 既存の子LineRendererを削除（再生成用）
		foreach (Transform child in transform)
		{
			Destroy(child.gameObject);
		}
		Bounds b = box.bounds; // ← AutoTiling でも正しいサイズが取れる
		Vector3 localMin = transform.InverseTransformPoint(b.min);
		Vector3 localMax = transform.InverseTransformPoint(b.max);

		Vector3[] points = new Vector3[5];
		points[0] = new Vector3(localMin.x, localMin.y);
		points[1] = new Vector3(localMin.x, localMax.y);
		points[2] = new Vector3(localMax.x, localMax.y);
		points[3] = new Vector3(localMax.x, localMin.y);
		points[4] = points[0];
		var go = new GameObject("OutlinePath");
		go.transform.SetParent(transform, false);
		var lr = go.AddComponent<LineRenderer>();
		lr.sortingOrder = 1;
		lr.useWorldSpace = false; // 親のローカル座標で扱う
		lr.material = lineMaterial;
		lr.startWidth = lineWidth;
		lr.endWidth = lineWidth;
		lr.startColor = Color.white;
		lr.endColor = Color.white;
		lr.positionCount = 5;
		lr.SetPositions(points);
		lr.loop = false; // 自前で閉じているのでfalseでOK
		go.AddComponent<RendererEnableController>();
	}
}
