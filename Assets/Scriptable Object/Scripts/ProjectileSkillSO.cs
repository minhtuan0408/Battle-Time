using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Skills/Projectile")]
public class ProjectileSkillSO : SkillSO
{
	public List<ProjectileSkillSOLevel> info;
	
	public override string GetDescription(int level)
	{
		var data = info.Find(x => x.level == level);
		return data != null ? data.description : "";
	}
}

[System.Serializable]
public class ProjectileSkillSOLevel
{
	public int level;
	public int damage;
	public float cooldown;
	public float range;
	public int projectileCount;
	public float speed;
	[TextArea] public string description;
}

