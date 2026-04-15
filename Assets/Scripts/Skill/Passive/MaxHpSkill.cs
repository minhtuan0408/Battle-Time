using UnityEngine;

public class MaxHpSkill : PassiveSkill
{
	public MaxHpSkillSO dataSkill;

	private int currentBonus = 0; // 👉 lưu bonus đang áp dụng

	protected override void Start()
	{
		base.Start();
		ApplyLevelData();
	}

	public override void LevelUp()
	{
		base.LevelUp();
		ApplyLevelData();
	}

	public override void ApplyLevelData()
	{
		if (targetStats == null) return;

		// ❌ remove bonus cũ
		targetStats.maxHealth -= currentBonus;
		targetStats.currentHealth -= currentBonus;

		// 👉 lấy data mới
		var data = dataSkill.info.Find(x => x.level == level);
		if (data == null) return;

		currentBonus = data.hpIncrease;

		// ✅ apply bonus mới
		targetStats.maxHealth += currentBonus;
		targetStats.currentHealth += currentBonus;

		// clamp
		if (targetStats.currentHealth > targetStats.maxHealth)
			targetStats.currentHealth = targetStats.maxHealth;
	}
}