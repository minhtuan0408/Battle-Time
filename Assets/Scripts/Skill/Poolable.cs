using UnityEngine;

public abstract class Poolable : MonoBehaviour
{
	public string poolKey;

	public virtual void OnSpawn() { }
	public virtual void OnDespawn()
	{
		gameObject.SetActive(false);
	}
}