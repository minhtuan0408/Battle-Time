using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using UnityEngine.EventSystems;


public class LevelUpCardUI : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private Image icon;
	[SerializeField] private TextMeshProUGUI nameText;
	[SerializeField] private TextMeshProUGUI descText;
	[SerializeField] private TextMeshProUGUI levelText;
	private SkillSO data;
	private LevelUpPanelRewardSO rewardData;
	private Action<SkillSO> onClick;
	private Action<LevelUpPanelRewardSO> onClickReward;

	public void OnPointerClick(PointerEventData eventData)
	{
		if (data != null)
		{
			//Debug.Log("Chọn skill: " + data.skillName);
			onClick?.Invoke(data);
		}
		else
		{
			//Debug.Log("Chọn reward");
			onClickReward?.Invoke(null); // hoặc truyền reward nếu bạn lưu lại
		}
	}


	public void Setup(SkillSO skill, int level, Action<SkillSO> onClick)
	{
		this.data = skill;
		this.onClick = onClick;
		if (icon != null)
			icon.sprite= skill.image;
		nameText.text = skill.skillName;
		descText.text = skill.GetDescription(level);
		levelText.text = "Level up : " + level;
	}

	public void SetupReward(LevelUpPanelRewardSO reward, Action<LevelUpPanelRewardSO> onClick)
	{
		this.rewardData = reward;
		this.onClickReward = onClick;

		nameText.text = reward.skillName;
		icon.sprite = reward.image;
		levelText.text = "";
	}
}