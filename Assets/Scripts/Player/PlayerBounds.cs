using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
	public Collider2D boundsCollider;

	void LateUpdate()
	{
		if (boundsCollider == null) return;

		Vector2 pos = transform.position;

		// lấy điểm gần nhất bên trong collider
		Vector2 closest = boundsCollider.ClosestPoint(pos);

		transform.position = closest;
	}
}