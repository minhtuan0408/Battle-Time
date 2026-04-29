using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkillCombatUI : MonoBehaviour
{
	[SerializeField] private Image[] images;

	private void OnEnable()
	{
		PlayerCombat.OnSkillsChanged += UpdateUI;
	}

	private void OnDisable()
	{
		PlayerCombat.OnSkillsChanged -= UpdateUI;
	}

	private void UpdateUI(List<SkillInstance> skills)
	{
		// clear hết trước
		for (int i = 0; i < images.Length; i++)
		{
			images[i].gameObject.SetActive(false);
		}

		// gán icon
		for (int i = 0; i < skills.Count && i < images.Length; i++)
		{
			images[i].gameObject.SetActive(true);
			images[i].sprite = skills[i].data.image;
		}
	}
}