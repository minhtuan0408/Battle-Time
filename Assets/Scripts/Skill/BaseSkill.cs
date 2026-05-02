using System.Collections;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
	public string key;
	protected int level = 0;
	public int maxLevel = 5;
	[Header("Cooldown")]
	public float cooldown = 1f;
	protected float cooldownTimer;
	[Header("Auto Cast")]
	public bool autoCast = true;
	protected TargetFinder targetFinder;
	protected PlayerStats targetStats;

	protected virtual void Start()
	{
		cooldownTimer = cooldown;
	}

	public void SetUpSkill(TargetFinder targetFinder, PlayerStats stats) 
	{
		transform.localPosition = new Vector2(0, 0);
		this.targetFinder = targetFinder;
		this.targetStats = stats;	
	}

	protected virtual void Update()
	{
		if (!autoCast) return;

		cooldownTimer -= Time.deltaTime;

		if (cooldownTimer <= 0f)
		{
			Activate();
			cooldownTimer = cooldown;
		}
	}

	protected abstract void Activate();

	public virtual void LevelUp()
	{
		if (level >= maxLevel) return;

		level++;
		ApplyLevelData();
	}
	public abstract void ApplyLevelData();
}
public enum ShootPattern
{
	Straight,   // 1 hướng
	Circle,     // tỏa 360
	Cone        // hình nón
}

