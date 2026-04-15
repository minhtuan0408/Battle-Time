using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : Projectile
{
	protected override void ReturnToPool()
	{
		// reset nếu cần
		base.ReturnToPool();
	}
}
