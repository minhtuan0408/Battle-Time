using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	public static ObjectPool Instance;

	[System.Serializable]
	public class Pool
	{
		public string key;
		public Poolable prefab;
		public int size;
	}

	public List<Pool> pools;

	private Dictionary<string, Queue<Poolable>> poolDict;
	private Dictionary<string, Poolable> prefabDict;

	private void Awake()
	{
		Instance = this;
		InitPool();
	}

	void InitPool()
	{
		poolDict = new Dictionary<string, Queue<Poolable>>();
		prefabDict = new Dictionary<string, Poolable>();

		foreach (var pool in pools)
		{
			Queue<Poolable> queue = new Queue<Poolable>();

			for (int i = 0; i < pool.size; i++)
			{
				Poolable obj = Instantiate(pool.prefab, transform);
				obj.poolKey = pool.key;
				obj.gameObject.SetActive(false);
				queue.Enqueue(obj);
			}

			poolDict.Add(pool.key, queue);
			prefabDict.Add(pool.key, pool.prefab);
		}
	}

	public Poolable Spawn(string key, Vector3 pos, Quaternion rot)
	{
		if (!poolDict.ContainsKey(key))
		{
			Debug.LogWarning("Pool không tồn tại: " + key);
			return null;
		}

		Queue<Poolable> queue = poolDict[key];

		Poolable obj;

		if (queue.Count > 0)
		{
			obj = queue.Dequeue();
		}
		else
		{
			obj = Instantiate(prefabDict[key], transform);
			obj.poolKey = key;
		}

		obj.transform.position = pos;
		obj.transform.rotation = rot;
		obj.gameObject.SetActive(true);

		obj.OnSpawn();

		return obj;
	}

	public void ReturnToPool(string key, Poolable obj)
	{
		obj.gameObject.SetActive(false);
		obj.OnDespawn();
		poolDict[key].Enqueue(obj);
	}
}