using System.Collections;
using UnityEngine;

public class RockEffect : AoEEffect
{
	public float delay = 0.3f;

	public override void OnSpawn()
	{
		StartCoroutine(Fall());
	}

	IEnumerator Fall()
	{
		// delay trước khi rơi (telegraph)
		yield return new WaitForSeconds(delay);



		DoDamage();

		// tắt object (pool)
		gameObject.SetActive(false);
	}
}