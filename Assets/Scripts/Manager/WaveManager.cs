using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
	public List<WaveData> waves;
	public Transform player;
	public float spawnRadius = 10f;

	int currentWave = 0;
	int enemiesAlive = 0;

	public EnemySpawn spawn;
	public event Action onAllWavesCleared;
	public void SetUpWave(LevelData levelData)
	{
		waves = new List<WaveData>(levelData.waves);
	}

	public IEnumerator RunWaves()
	{
		while (currentWave < waves.Count)
		{
			WaveData wave = waves[currentWave];

			yield return StartCoroutine(SpawnWave(wave));

			yield return new WaitUntil(() => enemiesAlive <= 0);

			Debug.Log("Wave " + currentWave + " cleared!");

			currentWave++;
			yield return new WaitForSeconds(2f);
		}

		Debug.Log("All waves done!");
		onAllWavesCleared?.Invoke();
	}

	IEnumerator SpawnWave(WaveData wave)
	{
		bool bossSpawned = false;
		int spawnedCount = 0;

		while (spawnedCount < wave.totalSpawnCount)
		{
			// spawn boss
			//if (wave.hasBoss && !bossSpawned && spawnedCount >= wave.bossSpawnAtCount)
			//{
			//	foreach (GameObject bossPrefab in wave.bossPrefab)
			//	{
			//		GameObject boss = spawn.Spawn(bossPrefab, wave.spawnType);
			//		RegisterEnemy(boss);
			//	}
			//}

			// spawn theo batch
			for (int i = 0; i < wave.spawnPerBatch; i++)
			{
				if (spawnedCount >= wave.totalSpawnCount)
					break;

				GameObject prefab = GetRandomEnemy(wave.enemies);
				GameObject enemy = spawn.Spawn(prefab, wave.spawnType);

				RegisterEnemy(enemy);
				spawnedCount++;
			}

			yield return new WaitForSeconds(wave.spawnInterval);
		}

		// fallback boss
		if (wave.hasBoss && !bossSpawned)
		{
			foreach (GameObject bossPrefab in wave.bossPrefab)
			{
				GameObject boss = spawn.Spawn(bossPrefab, wave.spawnType);
				RegisterEnemy(boss);
			}
		}
	}
	void OnEnemyDead()
	{
		enemiesAlive--;
	}
	void RegisterEnemy(GameObject enemy)
	{
		enemiesAlive++;

		BaseEnemy e = enemy.GetComponent<BaseEnemy>();
		if (e != null)
		{
			e.onDeath += OnEnemyDead;
		}
	}
	GameObject GetRandomEnemy(EnemySpawnData[] enemies)
	{
		float totalWeight = 0f;

		foreach (var e in enemies)
			totalWeight += e.weight;

		float rand = UnityEngine.Random.value * totalWeight;

		foreach (var e in enemies)
		{
			if (rand < e.weight)
				return e.prefab;

			rand -= e.weight;
		}

		return enemies[0].prefab;
	}
}