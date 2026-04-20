using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
	public int maxSkills = 5;

	public List<BaseSkill> skills = new List<BaseSkill>();
	public List<SkillInstance> ownedSkills = new List<SkillInstance>();

	public SkillInstance GetSkill(SkillSO skill)
	{
		return ownedSkills.Find(s => s.data == skill);
	}

	public TargetFinder TargetFinder;
	public PlayerStats Stats;
	public void Awake()
	{
		foreach (BaseSkill skill in skills) {
			skill.SetUpSkill(TargetFinder, Stats);
			
		}
	}

	public void LearnSkill(SkillSO skillSO)
	{
		var existing = GetSkill(skillSO);
		if (existing != null)
		{
			existing.level++;

			BaseSkill skillComponent = skills.Find(s => s.name == skillSO.skillName);
			skillComponent?.LevelUp();

			Debug.Log(skillComponent);
		}
		else
		{
			// chưa có → thêm mới
			var instance = new SkillInstance(skillSO);
			ownedSkills.Add(instance);

			GameObject newSkill = Instantiate(skillSO.SkillPrefab, transform);
			BaseSkill skill = newSkill.GetComponent<BaseSkill>();
			skill.name = skillSO.skillName;
			skills.Add(skill);

			skill.SetUpSkill(TargetFinder, Stats);
			skill.LevelUp();
		}


	}
}

public class SkillInstance
{
	public SkillSO data;
	public int level;

	public SkillInstance(SkillSO data)
	{
		this.data = data;
		level = 1;
	}
}