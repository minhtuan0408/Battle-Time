using System.Collections.Generic;
using UnityEngine;

public class OrbitSkill : BaseSkill
{
	public GameObject orbitPrefab;
	public OrbitSkillSO dataSkill;

	// 👉 runtime stat
	float radius;
	float angularSpeed;
	float lifeTime;
	float damage;
	int count;

	private bool isActive = false;
	private float activeTimer = 0f;
	private List<GameObject> currentOrbits = new List<GameObject>();

	protected override void Activate()
	{
		if (isActive) return;

		SpawnOrbit(count);

		isActive = true;
		activeTimer = 0f;
	}

	protected override void Update()
	{
		if (!autoCast) return;

		if (isActive)
		{
			activeTimer += Time.deltaTime;

			if (activeTimer >= lifeTime)
			{
				ClearOrbit();
				isActive = false;
				cooldownTimer = cooldown;
			}
			return;
		}

		cooldownTimer -= Time.deltaTime;

		if (cooldownTimer <= 0f)
		{
			Activate();
			cooldownTimer = cooldown;
		}
	}

	private void SpawnOrbit(int count)
	{
		currentOrbits.Clear();

		float angleStep = 360f / count;

		for (int i = 0; i < count; i++)
		{
			float angle = angleStep * i * Mathf.Deg2Rad;

			GameObject obj = Instantiate(orbitPrefab);

			var orbit = obj.GetComponent<OrbitProjectile>();
			orbit.Init(
				targetStats.transform,
				radius,
				angularSpeed,
				angle,
				lifeTime,
				(int)damage
			);

			currentOrbits.Add(obj);
		}
	}

	private void ClearOrbit()
	{
		foreach (var orb in currentOrbits)
		{
			if (orb != null)
				orb.SetActive(false);
		}
		currentOrbits.Clear();
	}

	// ⭐ CORE
	public override void ApplyLevelData()
	{
		var data = dataSkill.info.Find(x => x.level == level);
		if (data == null) return;

		damage = data.damage;
		radius = data.radius;
		angularSpeed = data.angularSpeed;
		lifeTime = data.lifeTime;
		count = Mathf.Max(1, data.projectileCount);

		cooldown = data.cooldown;
	}
}