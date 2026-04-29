using UnityEngine;

public class RockEffect : AoEEffect
{
	public float delay = 0.3f;

	private bool hasExploded = false;

	public override void Init(Vector2 pos, int damage, float radius)
	{
		base.Init(pos, damage, radius);

		timer = 0f;
		hasExploded = false;
	}

	protected override void Update()
	{
		timer += Time.deltaTime;

		// 👉 chờ delay rồi mới nổ
		if (!hasExploded && timer >= delay)
		{
			DealDamage();
			hasExploded = true;
		}

		// 👉 sau khi nổ thêm chút rồi biến mất
		if (hasExploded && timer >= delay + lifeTime)
		{
			ReturnToPool();
		}
	}
}