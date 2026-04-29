using UnityEngine;

public class Orb : MonoBehaviour
{
	public int value;

	[HideInInspector] public bool isAttracting;
	[HideInInspector] public Transform target;

	float speed;
	float accel = 100f;

	private void OnEnable()
	{
		OrbManager.Instance.Register(this);
	}

	private void OnDisable()
	{
		if (OrbManager.Instance != null)
		{
			OrbManager.Instance.Unregister(this);
			OrbManager.Instance.ReturnToPool(this);
		}
	}

	// 👉 gọi khi spawn
	public void Init(Vector2 pos, int val)
	{
		transform.position = pos;
		value = val;

		isAttracting = false;
		speed = 0f;
		target = null;
	}

	public void StartAttract(Transform player)
	{
		isAttracting = true;
		target = player;
		speed = 0f;
	}

	public void Tick()
	{
		if (!isAttracting) return;

		Vector2 dir = (target.position - transform.position).normalized;

		speed += accel * Time.deltaTime;
		transform.position += (Vector3)(dir * speed * Time.deltaTime);

		// 👉 gần player thì collect luôn
		if (Vector2.Distance(transform.position, target.position) < 0.2f)
		{
			Collect();
		}
	}

	void Collect()
	{
		target.GetComponent<PlayerStats>().AddExp(value);
		gameObject.SetActive(false); 
	}
}