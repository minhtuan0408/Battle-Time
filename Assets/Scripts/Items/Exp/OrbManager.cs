using System.Collections.Generic;
using UnityEngine;

public class OrbManager : MonoBehaviour
{
	public static OrbManager Instance;

	public List<Orb> allOrbs = new();
	public List<Orb> attractingOrbs = new();

	[Header("Pool")]
	public Orb orbPrefab;
	public int poolSize = 100;

	private Queue<Orb> pool = new();

	public Transform player;

	private void Awake()
	{
		Instance = this;
		

		InitPool();
	}

	void InitPool()
	{
		for (int i = 0; i < poolSize; i++)
		{
			Orb orb = Instantiate(orbPrefab, transform);
			orb.gameObject.SetActive(false);
			pool.Enqueue(orb);
		}
	}

	public void Register(Orb orb)
	{
		allOrbs.Add(orb);
	}

	public void Unregister(Orb orb)
	{
		allOrbs.Remove(orb);
		attractingOrbs.Remove(orb);
	}

	public void SpawnOrb(Vector2 pos, int value)
	{
		Orb orb = GetOrb();
		orb.gameObject.SetActive(true);
		orb.Init(pos, value);
	}

	Orb GetOrb()
	{
		if (pool.Count > 0)
		{
			return pool.Dequeue();
		}
		else
		{
			return Instantiate(orbPrefab, transform);
		}
	}
	public void ReturnToPool(Orb orb)
	{
		orb.gameObject.SetActive(false);
		pool.Enqueue(orb);
	}

	public void AttractNearby(Vector2 playerPos, float radius)
	{
		foreach (var orb in allOrbs)
		{
			if (orb.isAttracting) continue;

			if (Vector2.Distance(playerPos, orb.transform.position) <= radius)
			{
				orb.StartAttract(player);
				if (!attractingOrbs.Contains(orb))
				{
					attractingOrbs.Add(orb);
				}
			}
		}
	}

	private void Update()
	{
		for (int i = attractingOrbs.Count - 1; i >= 0; i--)
		{
			attractingOrbs[i].Tick();
		}

		//for (int i = 0; i < attractingOrbs.Count; i++)
		//{
		//	attractingOrbs[i].Tick(); tìm hiểu thêm
		//}
	}
}