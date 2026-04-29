using System;
using System.Collections.Generic;
using UnityEngine;

public class OrbitSkill : BaseSkill
{
	public OrbitSkillSO dataSkill;

	// 👉 runtime stat
	float radius;
	float angularSpeed;
	float lifeTime;
	int damage;
	int count;

	private bool isActive = false;
	private float activeTimer = 0f;
	private List<GameObject> currentOrbits = new List<GameObject>();
	float currentAngle = 0f;
	[SerializeField] private OrbitProjectile orbitPrefab;
	public OrbitProjectile[] pool = new OrbitProjectile[5];

	protected override void Start()
	{
		base.Start();

		for (int i = 0; i < pool.Length; i++)
		{
			pool[i] = Instantiate(orbitPrefab, transform);
			pool[i].gameObject.SetActive(false);
		}

		Debug.Log("Orbit"+level);
	}
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

			// 👉 Skill tự quay
			currentAngle += angularSpeed * Time.deltaTime;

			UpdateOrbitPositions();

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
		count = Mathf.Min(count, pool.Length);

		for (int i = 0; i < pool.Length; i++)
		{
			if (i < count)
			{
				var orb = pool[i];
				orb.gameObject.SetActive(true);

				orb.Init(transform, radius, 0f, damage);
			}
			else
			{
				pool[i].gameObject.SetActive(false);
			}
		}

		currentAngle = 0f; // reset rotation
	}

	private void ClearOrbit()
	{
		for (int i = 0; i < pool.Length; i++)
		{
			pool[i].gameObject.SetActive(false);
		}
	}

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

	private void UpdateOrbitPositions()
	{
		int activeCount = Mathf.Min(count, pool.Length);
		float angleStep = 360f / activeCount;

		for (int i = 0; i < activeCount; i++)
		{
			float angle = currentAngle + i * angleStep;
			pool[i].SetAngle(angle * Mathf.Deg2Rad);
		}
	}
}