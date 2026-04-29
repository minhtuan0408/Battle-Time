using System.Security.Cryptography;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{

	public float speed;
	protected int damage;
	protected float lifeTime;

	protected Vector2 direction;
	protected float timer;
	protected Transform player;

	public virtual void Init(Vector2 dir, float spd, int dmg, float life, Transform player)
	{
		direction = dir.normalized;
		speed = spd;
		damage = dmg;
		lifeTime = life;
		timer = lifeTime;
		this.player = player;

		
	}

	protected virtual void Update()
	{
		transform.position += (Vector3)(direction * speed * Time.deltaTime);

		timer -= Time.deltaTime;
		if (timer <= 0)
			ReturnToPool();
	}

	protected virtual void ReturnToPool()
	{
		gameObject.SetActive(false);
	}

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		IDamageable target = collision.GetComponent<IDamageable>();
		if (target != null)
		{
			target.TakeDamage(damage);
			ReturnToPool();
		}
	}
}