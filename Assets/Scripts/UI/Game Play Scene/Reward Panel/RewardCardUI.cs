using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class RewardCardUI : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private Image icon;
	[SerializeField] private TextMeshProUGUI nameText;
	[SerializeField] private TextMeshProUGUI valueText;

	[SerializeField] private GameObject front;
	[SerializeField] private GameObject back;

	private RewardSO data;
	private Action<RewardCardUI, RewardSO> onClick;

	private bool isFlipped = false;

	public void Setup(RewardSO reward, Action<RewardCardUI, RewardSO> onClick)
	{
		data = reward;
		this.onClick = onClick;

		icon.sprite = reward.icon;
		nameText.text = reward.rewardName;
		valueText.text = "+" + reward.value;

		isFlipped = false;

		front.SetActive(false);
		back.SetActive(true);

		gameObject.SetActive(true);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (isFlipped) return;

		isFlipped = true;

		// flip
		front.SetActive(true);
		back.SetActive(false);

		onClick?.Invoke(this, data);
	}
}