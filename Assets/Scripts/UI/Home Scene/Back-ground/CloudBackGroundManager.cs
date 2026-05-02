using UnityEngine;

public class CloudBackGroundManager : MonoBehaviour
{
	[Header("Cloud Prefabs")]
	public GameObject[] cloudPrefabs;

	[Header("Spawn Settings")]
	public int cloudCount = 5;
	public float spawnX = 10f;
	public float minY = -3f;
	public float maxY = 3f;

	[Header("Move Settings")]
	public float minSpeed = 0.5f;
	public float maxSpeed = 2f;

	[Header("Bounds")]
	public float leftBound = -10f;

	void Start()
	{
		SpawnClouds();
	}

	void SpawnClouds()
	{
		for (int i = 0; i < cloudCount; i++)
		{
			SpawnOne();
		}
	}

	void SpawnOne()
	{
		// Random prefab
		GameObject prefab = cloudPrefabs[Random.Range(0, cloudPrefabs.Length)];

		// Random v? trí
		Vector3 pos = new Vector3(
			Random.Range(leftBound, spawnX),
			Random.Range(minY, maxY),
			0
		);

		GameObject cloud = Instantiate(prefab, pos, Quaternion.identity, transform);

		// Gán mover
		Cloud mover = cloud.GetComponent<Cloud>();
		if (mover == null)
		{
			mover = cloud.AddComponent<Cloud>();
		}

		mover.direction = Vector3.left;
		mover.speed = Random.Range(minSpeed, maxSpeed);
		mover.leftBound = leftBound;
		mover.rightSpawn = spawnX;
	}
}