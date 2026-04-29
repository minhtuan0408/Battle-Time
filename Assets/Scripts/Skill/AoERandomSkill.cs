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
	[SerializeField] private AoEEffect effectPrefab;

	private List<AoEEffect> pool = new List<AoEEffect>();
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
		AoEEffect effect = GetEffect();

		effect.transform.position = pos;
		effect.gameObject.SetActive(true);

		effect.Init(pos, damage, radius);
	}


	AoEEffect GetEffect()
	{
		for (int i = 0; i < pool.Count; i++)
		{
			if (!pool[i].gameObject.activeInHierarchy)
				return pool[i];
		}

		// không có → tạo mới
		AoEEffect newEffect = Instantiate(effectPrefab, transform);
		pool.Add(newEffect);
		return newEffect;
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