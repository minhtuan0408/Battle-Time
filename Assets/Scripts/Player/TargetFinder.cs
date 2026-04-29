using System.Collections.Generic;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
	public LayerMask enemyLayer;
	private PlayerMovement playerMovement;
	public float radius;
	private void Awake()
	{
		playerMovement = GetComponentInParent<PlayerMovement>();
	}
	public Vector2 GetDirectionToNearestTarget(Vector2 position)
	{
		Collider2D[] hits = Physics2D.OverlapCircleAll(position, radius, enemyLayer);

		Transform nearest = null;
		float minDist = Mathf.Infinity;

		foreach (var hit in hits)
		{
			float dist = Vector2.Distance(position, hit.transform.position);

			if (dist < minDist)
			{
				minDist = dist;
				nearest = hit.transform;
			}
		}

		// ❌ Không có enemy → lấy hướng player
		if (nearest == null)
		{
			Vector2 moveDir = playerMovement.LastMoveDir;

			if (moveDir == Vector2.zero)
				return Vector2.right;

			return moveDir.normalized;
		}

		// ✅ Có enemy
		return ((Vector2)nearest.position - position).normalized;
	}

	public List<Transform> GetRandomTargets(Vector2 position, float radius, int count)
	{
		Collider2D[] hits = Physics2D.OverlapCircleAll(position, radius, enemyLayer);

		List<Transform> result = new List<Transform>();

		if (hits.Length == 0) return result;

		List<Transform> pool = new List<Transform>();
		foreach (var h in hits)
		{
			pool.Add(h.transform);
		}

		count = Mathf.Min(count, pool.Count);

		for (int i = 0; i < count; i++)
		{
			int index = Random.Range(0, pool.Count);
			result.Add(pool[index]);
			pool.RemoveAt(index); // tránh trùng
		}

		return result;
	}
}