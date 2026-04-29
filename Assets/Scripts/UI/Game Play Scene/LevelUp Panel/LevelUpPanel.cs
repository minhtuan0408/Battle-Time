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
	[SerializeField] private List<LevelUpPanelRewardSO> rewards;
	[SerializeField] private PlayerCombat playerCombat;
	[SerializeField] private PlayerStats playerStats;

	private void OnEnable()
	{
		Show();
	}

	public void Show()
	{
		panel.SetActive(true);
		Time.timeScale = 0f;

		List<SkillSO> randomSkills = GetRandomSkills(cards.Length);

		int skillCount = randomSkills.Count;
		int rewardCount = cards.Length - skillCount;

		List<LevelUpPanelRewardSO> randomRewards = new();

		if (rewardCount > 0)
		{
			randomRewards = GetRandomReward(rewardCount);
		}

		int skillIndex = 0;
		int rewardIndex = 0;

		for (int i = 0; i < cards.Length; i++)
		{
			cards[i].gameObject.SetActive(true);

			// ✅ Ưu tiên hiển thị skill trước
			if (skillIndex < skillCount)
			{
				var skill = randomSkills[skillIndex++];

				var instance = playerCombat.GetSkill(skill);
				int level = instance != null ? instance.level : 1;

				cards[i].Setup(skill, level, OnSelectSkill);
			}
			else
			{
				var reward = randomRewards[rewardIndex++];
				cards[i].SetupReward(reward, OnSelectReward);
			}
		}
	}

	void OnSelectReward(LevelUpPanelRewardSO reward)
	{
		if (reward.Type == LevelUpRewardType.HP)
			playerStats.Heal(50);
		else if (reward.Type == LevelUpRewardType.Gold)
			GameFlowManager.instance.AddGolds(500);
		else if (reward.Type == LevelUpRewardType.Diamond)
			GameFlowManager.instance.AddDiamonds(500);
		else if (reward.Type == LevelUpRewardType.Ticket)
			GameFlowManager.instance.AddTickets(1);
		Close();
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
		List<SkillSO> pool = new();

		bool hasActive = HasAnyActiveSkill();

		// ✅ CASE 1: FULL SLOT → chỉ lấy skill đã có (để upgrade)
		if (IsFullSlot())
		{
			foreach (var s in playerCombat.ownedSkills)
			{
				// bỏ skill đã max
				if (s.level >= 5)
					continue;

				pool.Add(s.data);
			}
		}
		else
		{
			// ✅ CASE 2: chưa full slot → lấy từ allSkills
			foreach (var skill in allSkills)
			{
				// nếu chưa có active nào thì không cho ra passive
				if (!hasActive && skill.type == SkillType.Passive)
					continue;

				// nếu đã có skill này và max rồi thì bỏ
				if (IsSkillMaxed(skill))
					continue;

				pool.Add(skill);
			}
		}

		List<SkillSO> result = new();

		for (int i = 0; i < count; i++)
		{
			if (pool.Count == 0) break;

			int index = UnityEngine.Random.Range(0, pool.Count);
			result.Add(pool[index]);
			pool.RemoveAt(index);
		}

		return result;
	}
	List<LevelUpPanelRewardSO> GetRandomReward(int count)
	{
		List<LevelUpPanelRewardSO> pool = new(rewards);
		List<LevelUpPanelRewardSO> result = new();

		for (int i = 0; i < count; i++)
		{
			if (pool.Count == 0) break;

			int index = UnityEngine.Random.Range(0, pool.Count);
			result.Add(pool[index]);
			pool.RemoveAt(index);
		}
		return result;
	}
	bool IsFullSlot()
	{
		return playerCombat.ownedSkills.Count >= 5;
	}
	bool IsSkillMaxed(SkillSO skill)
	{
		var instance = playerCombat.GetSkill(skill);
		return instance != null && instance.level >= 5;
	}

	bool HasAnyActiveSkill()
	{
		foreach (var s in playerCombat.ownedSkills)
		{
			if (s.data.type != SkillType.Passive)
				return true;
		}
		return false;
	}
}

