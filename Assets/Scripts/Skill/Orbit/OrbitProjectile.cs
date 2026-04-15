using System.Collections.Generic;
using UnityEngine;

public class OrbitProjectile : Poolable
{
	private Transform center;

	private float radius;
	private float angularSpeed;
	private float angle;

	private float lifeTime;
	private float timer;

	protected int damage;

	public float hitInterval = 0.5f;
	protected Dictionary<IDamageable, float> hitCooldown = new Dictionary<IDamageable, float>();

	public override void OnDespawn()
	{
		base.OnDespawn();

	}

	public void Init(Transform center, float radius, float angularSpeed, float startAngle, float lifeTime, int damage)
	{
		this.center = center;
		this.radius = radius;
		this.angularSpeed = angularSpeed;
		this.angle = startAngle;
		this.lifeTime = lifeTime;
		this.damage = damage;

		timer = 0f;
		hitCooldown.Clear();
	}

	private void Update()
	{
		if (center == null)
		{
			ReturnToPool();
			return;
		}

		// lifetime
		if (lifeTime > 0)
		{
			timer += Time.deltaTime;
			if (timer >= lifeTime)
			{
				ReturnToPool();
				return;
			}
		}

		// quay
		angle += angularSpeed * Time.deltaTime;

		float x = Mathf.Cos(angle) * radius;
		float y = Mathf.Sin(angle) * radius;

		transform.position = center.position + new Vector3(x, y, 0f);

		UpdateCooldowns();
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

		DealDamage(target);
		hitCooldown[target] = hitInterval;
	}

	protected virtual void DealDamage(IDamageable target)
	{
		target.TakeDamage(damage);
	}

	protected virtual void ReturnToPool()
	{
		ObjectPool.Instance.ReturnToPool(poolKey, this);
	}
}