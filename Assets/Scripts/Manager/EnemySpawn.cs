using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawn : MonoBehaviour
{
	public Transform player;
	public float spawnRadius = 10f;

	public Vector2 GetSpawnPosition(SpawnType type)
	{
		Vector2 playerPos = player.position;

		switch (type)
		{
			case SpawnType.RandomAroundPlayer:
				return playerPos + Random.insideUnitCircle.normalized * spawnRadius;

			case SpawnType.TopToBottom:
				return playerPos + new Vector2(Random.Range(-8f, 8f), 6f);

			case SpawnType.LeftToRight:
				return playerPos + new Vector2(-10f, Random.Range(-5f, 5f));

			case SpawnType.RightToLeft:
				return playerPos + new Vector2(10f, Random.Range(-5f, 5f));

			case SpawnType.TwoCorners:
				return Random.value > 0.5f
					? playerPos + new Vector2(-10f, 6f)
					: playerPos + new Vector2(10f, -6f);
		}

		return playerPos;
	}

	public GameObject Spawn(GameObject prefab, SpawnType type)
	{
		Vector2 pos = GetSpawnPosition(type);

		GameObject enemy = Instantiate(prefab, pos, Quaternion.identity);

		BaseEnemy e = enemy.GetComponent<BaseEnemy>();
		if (e != null)
		{
			e.SetTarget(player);
		}

		return enemy;
	}
}
public enum SpawnType
{
	RandomAroundPlayer,
	TopToBottom,
	LeftToRight,
	RightToLeft,
	TwoCorners
}