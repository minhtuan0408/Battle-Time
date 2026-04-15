using UnityEngine;

public class ItemInteract : MonoBehaviour
{
	public Transform Player;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Orb"))
		{
			var orb = other.GetComponent<Orb>();
			orb.StartAttract(Player);

			if (!OrbManager.Instance.attractingOrbs.Contains(orb))
			{
				OrbManager.Instance.attractingOrbs.Add(orb);
			}
		}
	}
}