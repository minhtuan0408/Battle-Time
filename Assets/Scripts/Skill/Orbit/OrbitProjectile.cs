using System.Collections.Generic;
using UnityEngine;

public class OrbitProjectile : MonoBehaviour
{
	private Transform center;
	private float radius;
	private float angle;

	private int damage;

	public float hitInterval = 0.5f;
	protected Dictionary<IDamageable, float> hitCooldown = new();
	[SerializeField] private Transform spriteTransform;
	[SerializeField] private float rotateSpeed = 720f;
	public void Init(Transform center, float radius, float startAngle, int damage)
	{
		this.center = center;
		this.radius = radius;
		this.angle = startAngle;
		this.damage = damage;

		hitCooldown.Clear();
	}

	// 👉 Skill sẽ gọi cái này mỗi frame
	public void SetAngle(float newAngle)
	{
		if (center == null) return; // tránh crash
		angle = newAngle;

		float x = Mathf.Cos(angle) * radius;
		float y = Mathf.Sin(angle) * radius;

		transform.position = center.position + new Vector3(x, y, 0f);
	}

	private void Update()
	{
		UpdateCooldowns();
		if (spriteTransform != null)
			spriteTransform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
	}

	private void UpdateCooldowns()
	{
		var keys = new List<IDamageable>(hitCooldown.Keys);
		foreach (var key in keys)
		{
			hitCooldown[key] -= Time.deltaTime;
			if (hitCooldown[key] <= 0)
				hitCooldown.Remove(key);
		}
	}

	protected virtual void OnTriggerStay2D(Collider2D collision)
	{
		IDamageable target = collision.GetComponent<IDamageable>();
		if (target == null) return;

		if (hitCooldown.ContainsKey(target)) return;

		target.TakeDamage(damage);
		hitCooldown[target] = hitInterval;
	}
}