using System.Collections.Generic;
using UnityEngine;

public class AoERandomSkill : BaseSkill
{
	int damage;
	float radius;
	float range;
	int rockCount;
	public AoESkillSO dataSkill;
	public LayerMask enemyLayer;

	protected override void Activate()
	{
		if (targetFinder == null) return;

		int count = Mathf.Max(1, rockCount);

		var targets = targetFinder.GetRandomTargets(transform.position, range, count);

		if (targets.Count == 0) return;

		foreach (var target in targets)
		{
			SpawnRock(target.position);
		}
	}

	void SpawnRock(Vector2 pos)
	{
		AoEEffect effect = ObjectPool.Instance.Spawn(key, pos, Quaternion.identity) as AoEEffect;

		if (effect != null)
		{
			effect.Init(pos, damage, radius);
		}
	}





	public override void ApplyLevelData()
	{
		var data = dataSkill.info.Find(x => x.level == level);
		if (data == null) return;

		damage = data.damage;
		radius = data.radius;
		range = data.range;
		rockCount = Mathf.Max(1, data.ObjectCount);

		// nếu BaseSkill có cooldown
		cooldown = data.cooldown;
	}
}