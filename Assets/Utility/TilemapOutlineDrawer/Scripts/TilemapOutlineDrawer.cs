using UnityEngine;

[RequireComponent(typeof(CompositeCollider2D))]
public class TilemapOutlineDrawer : MonoBehaviour
{
	[SerializeField] private Material lineMaterial;
	[SerializeField] private float lineWidth = 0.05f;

	private CompositeCollider2D composite;

	void Awake()
	{
		composite = GetComponent<CompositeCollider2D>();
		GenerateOutline();
	}

	public void GenerateOutline()
	{
		// 既存の子LineRendererを削除（再生成用）
		foreach (Transform child in transform)
		{
			Destroy(child.gameObject);
		}

		int pathCount = composite.pathCount;
		var pointsBuffer = new Vector2[1024]; // 十分大きめに

		for (int i = 0; i < pathCount; i++)
		{
			int pointCount = composite.GetPath(i, pointsBuffer);

			var go = new GameObject($"OutlinePath_{i}");
			go.transform.SetParent(transform, false);

			var lr = go.AddComponent<LineRenderer>();
			lr.useWorldSpace = false; // 親のローカル座標で扱う
			lr.material = lineMaterial;
			lr.startWidth = lineWidth;
			lr.endWidth = lineWidth;
			lr.positionCount = pointCount + 1;
			lr.sortingOrder = 1;

			lr.startColor = Color.white;
			lr.endColor = Color.white;

			for (int p = 0; p < pointCount; p++)
			{
				lr.SetPosition(p, pointsBuffer[p]);
			}
			// 最後に始点をもう一度入れてループ
			lr.SetPosition(pointCount, pointsBuffer[0]);

			lr.loop = false; // 自前で閉じているのでfalseでOK

			go.AddComponent<RendererEnableController>();
		}
	}
}
