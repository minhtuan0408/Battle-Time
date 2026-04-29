using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileSkill : BaseSkill
{
	[SerializeField] private Projectile projectilePrefab;
	private List<Projectile> pool = new List<Projectile>();
	[Header("Pattern")]
	public ShootPattern pattern;
	[Header("Cone only")]
	public float coneAngle = 60f;

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

		switch (pattern)
		{
			case ShootPattern.Straight:
				ShootStraight(mainDir);
				break;

			case ShootPattern.Circle:
				ShootCircle(count);
				break;

			case ShootPattern.Cone:
				ShootCone(mainDir, count, coneAngle);
				break;
		}
	}
	void ShootStraight(Vector2 mainDir)
	{
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

	void ShootCone(Vector2 forward, int bulletCount, float angleRange)
	{
		if (bulletCount == 1)
		{
			SpawnProjectile(forward);
			return;
		}

		float startAngle = -angleRange / 2f;
		float angleStep = angleRange / (bulletCount - 1);

		for (int i = 0; i < bulletCount; i++)
		{
			float angle = startAngle + angleStep * i;
			Vector2 dir = RotateVector(forward, angle);
			SpawnProjectile(dir);
		}
	}

	void ShootCircle(int bulletCount)
	{
		float angleStep = 360f / bulletCount;

		for (int i = 0; i < bulletCount; i++)
		{
			float angle = i * angleStep;

			Vector2 dir = new Vector2(
				Mathf.Cos(angle * Mathf.Deg2Rad),
				Mathf.Sin(angle * Mathf.Deg2Rad)
			);

			SpawnProjectile(dir);
		}
	}
}
