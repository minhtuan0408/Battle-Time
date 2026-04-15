using UnityEngine;

public class ShieldSkill : PassiveSkill
{
	public ShieldSkillSO dataSkill;

	private int maxShield;
	private int shieldPerCast;

	protected override void Start()
	{
		base.Start();
		ApplyLevelData();
	}

	protected override void Update()
	{
		cooldownTimer -= Time.deltaTime;

		if (cooldownTimer <= 0f)
		{
			Activate();
			cooldownTimer = cooldown;
		}
	}

	protected override void Activate()
	{
		if (targetStats == null) return;

		// 👉 chỉ cộng khi chưa đạt max
		if (targetStats.GetShield() < maxShield)
		{
			targetStats.ShieldOn(shieldPerCast);
		}
	}

	public override void LevelUp()
	{
		base.LevelUp();
		ApplyLevelData();
	}

	public override void ApplyLevelData()
	{
		var data = dataSkill.info.Find(x => x.level == level);
		if (data == null) return;

		maxShield = data.maxShield;
		shieldPerCast = data.shieldPerCast;
		cooldown = data.cooldown;
	}
}