using UnityEngine;

public class AoEEffect : Poolable
{
	protected Vector2 position;
	protected int damage;
	protected float radius;
	public LayerMask enemyLayer;

	public virtual void Init(Vector2 pos, int damage, float radius)
	{
		this.position = pos;
		this.damage = damage;
		this.radius = radius;

		transform.position = pos;

		OnSpawn();
	}



	protected void DoDamage()
	{
		Collider2D[] hits = Physics2D.OverlapCircleAll(position, radius, enemyLayer);

		foreach (var hit in hits)
		{
			hit.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
		}
	}
}