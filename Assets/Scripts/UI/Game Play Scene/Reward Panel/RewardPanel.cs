using UnityEngine;

public class RewardPanel : MonoBehaviour
{
	[SerializeField] private RewardCardUI[] cards;
	[SerializeField] private RewardSO[] rewardPool;

	private int flipCount = 0;
	private int maxFlip = 3;

	public void Show()
	{
		gameObject.SetActive(true);

		flipCount = 0;

		// random reward cho TẤT CẢ card
		for (int i = 0; i < cards.Length; i++)
		{
			RewardSO reward = rewardPool[Random.Range(0, rewardPool.Length)];

			cards[i].Setup(reward, OnCardFlipped);
		}
	}

	void OnCardFlipped(RewardCardUI card, RewardSO reward)
	{
		if (flipCount >= maxFlip) return;

		flipCount++;

		Debug.Log($"Lật {flipCount}: {reward.rewardName}");

		ApplyReward(reward);

		// đủ 3 lần → khóa game
		if (flipCount >= maxFlip)
		{
			DisableAllCards();

			//Invoke(nameof(Hide), 0.5f);
		}
	}

	void ApplyReward(RewardSO reward)
	{
		//if (reward.rewardName == "Gold")
		//{
		//	PlayerData.Instance.AddGold(reward.value);
		//}
		//else
		//{
		//	PlayerData.Instance.AddFragment(reward.rewardName, reward.value);
		//}
	}

	void DisableAllCards()
	{
		foreach (var c in cards)
		{
			c.GetComponent<UnityEngine.UI.Button>()?.interactable.Equals(false);
		}
	}

	void Hide()
	{
		gameObject.SetActive(false);
	}
}