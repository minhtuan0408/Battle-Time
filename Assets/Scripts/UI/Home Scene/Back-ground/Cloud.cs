using UnityEngine;

public class Cloud : MonoBehaviour
{
	public float speed = 2f;
	public Vector3 direction = Vector3.left;

	public float leftBound = -10f;
	public float rightSpawn = 10f;

	void Update()
	{
		transform.Translate(direction * speed * Time.deltaTime);

		if (direction.x < 0 && transform.position.x < leftBound)
		{
			ResetPosition();
		}
		else if (direction.x > 0 && transform.position.x > rightSpawn)
		{
			ResetPosition();
		}
	}

	void ResetPosition()
	{
		Vector3 pos = transform.position;

		if (direction.x < 0)
			pos.x = rightSpawn;
		else
			pos.x = leftBound;

		pos.y = Random.Range(-3f, 3f); // optional random lại Y

		transform.position = pos;
	}
}