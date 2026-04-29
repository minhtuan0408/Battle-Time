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

	int TotalDiamond;
	int TotalGold;

	int TotalTicket;
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
}