using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileSkill : BaseSkill
{
	[SerializeField] private Projectile projectilePrefab;
	private List<Projectile> pool = new List<Projectile>();

	[Header("Stats")]
	int damage;
	float speed;
	float range;
	int count;
	public override void ApplyLevelData()
	{
		var data = dataSkill.info.Find(x => x.level == level);
		if (data == null) return;
		speed = data.speed;
		damage = data.damage;
		range = data.range;
		count = Mathf.Max(1, data.projectileCount);
	}

	public ProjectileSkillSO dataSkill;
	protected override void Activate()
	{
		if (targetFinder == null) return;

		Vector2 mainDir = targetFinder.GetDirectionToNearestTarget(transform.position);

		for (int i = 0; i < count; i++)
		{
			Vector2 dir = (i == 0)
				? mainDir
				: RotateVector(mainDir, (360f / count) * i);

			SpawnProjectile(dir);
		}
	}

	void SpawnProjectile(Vector2 dir)
	{
		Projectile proj = GetProjectile();

		proj.transform.position = transform.position;
		proj.gameObject.SetActive(true);

		proj.Init(dir, speed, damage, range, transform);

		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		proj.transform.rotation = Quaternion.Euler(0, 0, angle);
	}
	Vector2 RotateVector(Vector2 v, float angle)
	{
		float rad = angle * Mathf.Deg2Rad;
		float cos = Mathf.Cos(rad);
		float sin = Mathf.Sin(rad);

		return new Vector2(
			v.x * cos - v.y * sin,
			v.x * sin + v.y * cos
		).normalized;
	}

	Projectile GetProjectile()
	{
		// tìm object đang inactive
		for (int i = 0; i < pool.Count; i++)
		{
			if (!pool[i].gameObject.activeInHierarchy)
			{
				return pool[i];
			}
		}
		Projectile newProj = Instantiate(projectilePrefab);
		pool.Add(newProj);
		return newProj;
	}
}