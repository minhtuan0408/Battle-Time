using UnityEngine;

public abstract class SkillSO : ScriptableObject
{
	public string skillName;
	public abstract string GetDescription(int level);
	public GameObject SkillPrefab;
	public SkillType type;
}
public enum SkillType
{
	Projectile,
	AoERandom,
	Orbit,
	Passive
}