using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpPanel : MonoBehaviour
{
	[SerializeField] private GameObject panel;
	[SerializeField] private GameObject Content;
	[SerializeField] private LevelUpCardUI[] cards;

	[SerializeField] private GameObject LevelUpCardUIPrefab;
	[SerializeField] private List<SkillSO> allSkills;
	[SerializeField] private PlayerCombat playerCombat;
	private void OnEnable()
	{
		Show();
	}

	public void Show()
	{
		panel.SetActive(true);
		Time.timeScale = 0f;

		List<SkillSO> randomSkills = GetRandomSkills(cards.Length);

		for (int i = 0; i < cards.Length; i++)
		{
			cards[i].gameObject.SetActive(true);
			var skill = randomSkills[i];

			var instance = playerCombat.GetSkill(skill);
			int level = instance != null ? instance.level : 1;

			cards[i].Setup(skill, level, OnSelectSkill);

		}
	}
	public void Close()
	{
		panel.SetActive(false);
		Time.timeScale = 1f;
	}
	void OnSelectSkill(SkillSO skill)
	{

		playerCombat.LearnSkill(skill);
		Close();
	}
	List<SkillSO> GetRandomSkills(int count)
	{
		List<SkillSO> result = new();
		List<SkillSO> temp = new(allSkills);

		for (int i = 0; i < count; i++)
		{
			if (temp.Count == 0) break;

			int index = UnityEngine.Random.Range(0, temp.Count);
			result.Add(temp[index]);
			temp.RemoveAt(index);
		}

		return result;
	}

}