using System.Collections;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
	public static GameFlowManager instance;
	public float startDelay = 5f;

	public LevelData levelData;
	public GameObject levelPanel;
	public WaveManager waveManager;

	public RewardPanel rewardPanel;
	public UIGamePlayScene gamePlayScene;

	int TotalDiamond;
	int TotalGold;
	int TotalEnemyKill;
	int TotalTicket;

	void OnEnable()
	{
		BaseEnemy.OnAnyEnemyDied += OnEnemyDied;
	}
	void OnDisable()
	{
		BaseEnemy.OnAnyEnemyDied -= OnEnemyDied;
	}
	void OnEnemyDied(BaseEnemy enemy)
	{
		AddEnemyKill(1);
	}
	void Start()
	{
		waveManager.onAllWavesCleared += OnGameWin;
		StartCoroutine(StartGameRoutine());

		instance = this;
	}

	IEnumerator StartGameRoutine()
	{
		waveManager.SetUpWave(levelData);
		yield return new WaitForSeconds(startDelay);
		StartCoroutine(waveManager.RunWaves());
		levelPanel.SetActive(true);
		
	}

	void OnGameWin()
	{
		Debug.Log("WIN → mở reward");

		levelPanel.SetActive(false);

		rewardPanel.Show();
	}

	public void AddDiamonds(int value)
	{
		TotalDiamond += value;
	}
	public void AddGolds(int value)
	{
		TotalGold += value;
	}

	public void AddTickets(int value)
	{
		if (TotalTicket > 9) { 
			TotalTicket =9;
			return;
		}
		else 
			TotalTicket += value;
	}
	public void AddEnemyKill(int value)
	{
		TotalEnemyKill += value;
		if (gamePlayScene.EnemyKilled != null)
			gamePlayScene.EnemyKilled.text = TotalEnemyKill.ToString();
	}
}