using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Regen")]
public class RegenSkillSO : SkillSO
{
	public List<RegenSkillSOLevel> info;
	public override string GetDescription(int level)
	{
		var data = info.Find(x => x.level == level);
		return data != null ? data.description : "";
	}
}

[System.Serializable]
public class RegenSkillSOLevel
{
	public int level;
	public int healAmount;
	public float interval;
	[TextArea] public string description;
}