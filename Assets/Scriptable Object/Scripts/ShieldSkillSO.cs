using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Shield")]
public class ShieldSkillSO : SkillSO
{
	public List<ShieldSkillSOLevel> info;
	public override string GetDescription(int level)
	{
		var data = info.Find(x => x.level == level);
		return data != null ? data.description : "";
	}

}

[System.Serializable]
public class ShieldSkillSOLevel
{
	public int level;
	public int maxShield;
	public int shieldPerCast;
	public float cooldown;
	[TextArea] public string description;
}