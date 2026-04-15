using UnityEngine;

public class PassiveSkill : BaseSkill
{
	public override void ApplyLevelData()
	{
		throw new System.NotImplementedException();
	}

	protected override void Activate()
	{
		// Passive thường không dùng Activate
	}
}