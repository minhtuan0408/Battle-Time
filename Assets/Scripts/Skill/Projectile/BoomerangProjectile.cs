using UnityEngine;

public class BoomerangProjectile : Projectile
{
	private bool returning;
	private float returnDelay = 1.5f;

	[SerializeField] private Transform spriteTransform;
	[SerializeField] private float rotateSpeed = 720f;

	public override void Init(Vector2 dir, float spd, int dmg, float life, Transform player)
	{
		base.Init(dir, spd, dmg, life, player);

		returning = false;
		returnDelay = 1.5f;
	}

	private void OnEnable()
	{
		returning = false;
		returnDelay = 1.5f;
	}

	protected override void Update()
	{
		if (spriteTransform != null)
			spriteTransform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);

		transform.position += (Vector3)(direction * speed * Time.deltaTime);

		if (!returning)
		{
			returnDelay -= Time.deltaTime;
			if (returnDelay <= 0)
				returning = true;
		}
		else
		{
			direction = ((Vector2)player.position - (Vector2)transform.position).normalized;

			float distance = Vector2.Distance(transform.position, player.position);
			if (distance < 0.5f)
			{
				returning = false;
				ReturnToPool();
			}
		}
	}

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		IDamageable target = collision.GetComponent<IDamageable>();
		if (target != null)
		{
			target.TakeDamage(damage);
		}
	}
}