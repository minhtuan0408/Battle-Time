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
	private Action<SkillSO> onClick;


	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("Chọn: " + data.skillName);
		onClick?.Invoke(data);
	}

	//public void Setup(ProjectileSkillSO skill, int level)
	//{
	//	data = skill;

	//	nameText.text = skill.skillName;

	//	if (skill.info.Count > level)
	//	{
	//		descText.text = skill.info[level].description;
	//	}
	//}

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
}