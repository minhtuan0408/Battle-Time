using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/AoE Random")]
public class AoESkillSO : SkillSO
{
	public List<AoERandomSkillLevel> info;

	public override string GetDescription(int level)
	{
		var data = info.Find(x => x.level == level);
		return data != null ? data.description : "";
	}

	public AoERandomSkillLevel GetData(int level)
	{
		return info.Find(x => x.level == level);
	}
}

[System.Serializable]
public class AoERandomSkillLevel
{
	public int level;

	public int damage;
	public float cooldown;
	public float radius;
	public float range;

	public int ObjectCount;

	[TextArea] public string description;
}