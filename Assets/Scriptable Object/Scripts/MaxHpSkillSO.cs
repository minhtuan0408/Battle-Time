using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/MaxHP")]
public class MaxHpSkillSO : SkillSO
{
	public List<MaxHpSkillSOLevel> info;
	public override string GetDescription(int level)
	{
		var data = info.Find(x => x.level == level);
		return data != null ? data.description : "";
	}
}

[System.Serializable]
public class MaxHpSkillSOLevel
{
	public int level;
	public int hpIncrease;
	[TextArea] public string description;
}