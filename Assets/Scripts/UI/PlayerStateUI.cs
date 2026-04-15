using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateUI : MonoBehaviour
{
	[SerializeField] private PlayerStats playerStats;
	[SerializeField] private Slider healthBar;
	[SerializeField] private Slider expBar;
	[SerializeField] private TextMeshProUGUI level;
	private void OnEnable()
	{
		playerStats.OnHealthChanged += UpdateHealth;
		playerStats.OnLevelChanged += UpdateExp;
	}

	private void OnDisable()
	{
		playerStats.OnHealthChanged -= UpdateHealth;
		playerStats.OnLevelChanged -= UpdateExp; // 🔥 thêm
	}
	private void Start()
	{
		UpdateHealth(playerStats.currentHealth, playerStats.maxHealth);
		UpdateExp(playerStats.currentExp, playerStats.expToNextLevel, playerStats.level);
	}

	private void UpdateHealth(int current, int max)
	{
		healthBar.value = (float)current / max;
	}

	private void UpdateExp(int currentExp, int expToNext, int currentLevel)
	{
		expBar.value = (float)currentExp / expToNext;
		level.text = "Lv." + currentLevel;
	}
}