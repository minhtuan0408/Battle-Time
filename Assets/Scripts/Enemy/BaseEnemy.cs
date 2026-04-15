using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public abstract class BaseEnemy : MonoBehaviour, IDamageable
{
	[Header("Stats")]
	public float moveSpeed = 2f;
	public float attackCooldown = 1f;
	protected float lastAttackTime;

	public Transform target;

	public SpriteRenderer sprite;
	private MaterialPropertyBlock mpb;


	public float separationRadius = 2f;
	public float separationForce = 3f;
	float flipThreshold = 0.1f;
	public LayerMask enemyLayer;

	public Action onDeath;
	protected virtual void Awake()
	{
		mpb = new MaterialPropertyBlock();
	}

	protected virtual void Update()
	{
		Move();
	}

	protected virtual void Move()
	{
		if (target == null) return;

		Vector2 dirToPlayer = (target.position - transform.position).normalized;
		Vector2 separation = CalculateSeparation();

		Vector2 moveDir = dirToPlayer + separation * separationForce;

		HandleRotation(dirToPlayer);

		transform.position += (Vector3)(moveDir.normalized * moveSpeed * Time.deltaTime);
	}
	private int facing = 1;
	protected void HandleRotation(Vector2 moveDir)
	{
		float threshold = 0.1f;

		if (moveDir.x > threshold)
			facing = 1;
		else if (moveDir.x < -threshold)
			facing = -1;

		transform.rotation = Quaternion.Euler(0, facing == 1 ? 0 : 180, 0);
	}
	protected Vector2 CalculateSeparation()
	{
		Vector2 separation = Vector2.zero;

		Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, separationRadius, enemyLayer);

		foreach (Collider2D c in hits)
		{
			if (c.gameObject == gameObject) continue;

			Vector2 diff = (Vector2)transform.position - (Vector2)c.transform.position;
			float dist = diff.magnitude;

			if (dist > 0 && dist < separationRadius)
			{
				// càng gần càng bị đẩy mạnh
				float weight = (separationRadius - dist) / separationRadius;
				separation += diff.normalized * weight;
			}
		}

		return separation;
	}
	public void SetTarget(Transform newTarget)
	{
		target = newTarget;
	}

	public float hp = 100;

	public virtual void TakeDamage(int damage)
	{
		hp -= damage;
		StartCoroutine(HitFlash());
		if (hp <= 0)
		{
			Die();
		}
	}

	IEnumerator HitFlash()
	{
		sprite.GetPropertyBlock(mpb);
		mpb.SetFloat("_HitEffectBlend", 1f);
		sprite.SetPropertyBlock(mpb);

		yield return new WaitForSeconds(0.05f);

		sprite.GetPropertyBlock(mpb);
		mpb.SetFloat("_HitEffectBlend", 0f);
		sprite.SetPropertyBlock(mpb);
	}

	protected virtual void Die()
	{
		OrbManager.Instance.SpawnOrb(transform.position, 5);
		onDeath?.Invoke();
		Destroy(gameObject);
	}
}