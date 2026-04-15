using System.Collections;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{

	public float startDelay = 5f;

	public LevelData levelData;
	public GameObject levelPanel;
	public WaveManager waveManager;

	public RewardPanel rewardPanel;
	void Start()
	{
		waveManager.onAllWavesCleared += OnGameWin;
		StartCoroutine(StartGameRoutine());
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
}