using UnityEngine;

public class RegenSkill : PassiveSkill
{
	public RegenSkillSO dataSkill;

	int healAmount;
	float interval;

	private float timer;

	protected override void Start()
	{
		base.Start();
		ApplyLevelData();
		timer = interval;
	}

	protected override void Update()
	{
		timer -= Time.deltaTime;

		if (timer <= 0f)
		{
			if (targetStats != null)
			{
				targetStats.Heal(healAmount);
			}

			timer = interval;
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

		healAmount = data.healAmount;
		interval = data.interval;

		// 👉 tránh bug timer khi giảm interval
		timer = Mathf.Min(timer, interval);
	}
}