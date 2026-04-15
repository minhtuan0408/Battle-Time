using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Orbit")]
public class OrbitSkillSO : SkillSO
{
	public List<OrbitSkillSOLevel> info;
	public override string GetDescription(int level)
	{
		var data = info.Find(x => x.level == level);
		return data != null ? data.description : "";
	}
}

[System.Serializable]
public class OrbitSkillSOLevel
{
	public int level;
	public float damage;
	public float cooldown;

	public float radius;
	public float angularSpeed;
	public float lifeTime;

	public int projectileCount;

	[TextArea] public string description;
}

