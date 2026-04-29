using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
	[Header("Health")]
	public int maxHealth = 100;
	public int currentHealth;

	[Header("Damage Settings")]
	public float invincibleTime = 0.5f;

	private bool canDamage = true;
	private float damageTimer = 0f;

	public int level = 1;

	public int currentExp = 0;
	public int expToNextLevel = 10;
	public HitScreenUI hitUI;

	public event Action<int, int> OnHealthChanged;
	public event Action<int, int, int> OnLevelChanged;
	public event Action<int> OnLevelUp;

	public GameObject LevelUpPanel;
	int shieldCnt;
	public GameObject Shield;
	private void Start()
	{
		currentHealth = maxHealth;
		OnHealthChanged?.Invoke(currentHealth, maxHealth);

		OnLevelChanged?.Invoke(currentExp, expToNextLevel, level);
	}

	private void Update()
	{
		if (!canDamage)
		{
			damageTimer += Time.deltaTime;

			if (damageTimer >= invincibleTime)
			{
				canDamage = true;
				damageTimer = 0f;
			}
		}
	}
	public void TakeDamage(int damage)
	{
		if (!canDamage) return;

        if (shieldCnt > 0)
        {
			shieldCnt--;
			if (shieldCnt <= 0)
				Shield.SetActive(false);
			return;
        }
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
		OnHealthChanged?.Invoke(currentHealth, maxHealth);
		hitUI.Trigger();
		if (currentHealth <= 0)
		{
			Die();
			return;
		}

		canDamage = false; 
	}
	private void Die()
	{
		Debug.Log("Player chết");
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			TakeDamage(10);

		}


	}

	public void AddExp(int amount)
	{
		currentExp += amount;

		OnLevelChanged?.Invoke(currentExp, expToNextLevel, level); 

		while (currentExp >= expToNextLevel)
		{
			LevelUp();
		}
	}

	void LevelUp()
	{
		currentExp -= expToNextLevel;
		level++;
		this.LevelUpPanel.SetActive(true);
		expToNextLevel = GetExpForNextLevel(level);

		OnLevelUp?.Invoke(level); 

		OnLevelChanged?.Invoke(currentExp, expToNextLevel, level);
	}

	int GetExpForNextLevel(int level)
	{

		return 10 + (level * 5);
	}

	public void Heal(int amount)
	{
		currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

		OnHealthChanged?.Invoke(currentHealth, maxHealth);
	}
	public void ShieldOn(int amount)
	{
		shieldCnt += amount;
		Shield.SetActive(true);
		Debug.Log("Số lượng shield"+ shieldCnt);
	}

	public int GetShield()
	{
		return shieldCnt;
	}
}