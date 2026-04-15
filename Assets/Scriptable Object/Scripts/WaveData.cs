using UnityEngine;

[CreateAssetMenu(menuName = "Game/Wave")]
public class WaveData : ScriptableObject
{
	public EnemySpawnData[] enemies;
	public SpawnType spawnType;
	public int totalSpawnCount;
	public float spawnInterval;
	public int spawnPerBatch = 1;
	[Header("Boss")]
	public bool hasBoss;
	public GameObject[] bossPrefab;

	public int bossSpawnAtCount; 
}

[System.Serializable]
public class EnemySpawnData
{
	public GameObject prefab;
	[Range(0f, 1f)]
	public float weight; // tỉ lệ xuất hiện
}