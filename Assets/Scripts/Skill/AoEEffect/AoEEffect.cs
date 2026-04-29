using UnityEngine;

public class AoEEffect : MonoBehaviour
{
	protected float radius;
	protected int damage;

	protected float lifeTime = 0.5f;
	protected float timer;

	public LayerMask LayerMask;
	public virtual void Init(Vector2 pos, int damage, float radius)
	{
		transform.position = pos;
		this.damage = damage;
		this.radius = radius;

		timer = 0f;
	}

	protected virtual void Update()
	{
		timer += Time.deltaTime;

		if (timer >= lifeTime)
		{
			ReturnToPool();
		}
	}

	protected virtual void DealDamage()
	{
		Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask);

		foreach (var col in hits)
		{
			IDamageable target = col.GetComponent<IDamageable>();
			if (target != null)
			{
				target.TakeDamage(damage);
			}
		}
	}

	protected void ReturnToPool()
	{
		gameObject.SetActive(false);
	}
}