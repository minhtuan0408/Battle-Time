using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
	public Transform visual;       
	public Animator animator;
	private Rigidbody2D _rb;
    private Vector2 _input;
	public Vector2 LastMoveDir { get; private set; } = Vector2.right;
	void Awake()
    {

        _rb = GetComponent<Rigidbody2D>();
    }
	[SerializeField] private ParticleSystem dustFx;
	[SerializeField] private float dustCooldown = 0.1f;
	private float dustTimer;
	void PlayDust()
	{
		var shape = dustFx.shape;

		if (_input.x > 0)
			shape.rotation = new Vector3(0, 0, 0); 
		else if (_input.x < 0)
			shape.rotation = new Vector3(0, 180, 0);     

		dustFx.Play();
	}
	void Update()
	{
		_input.x = Input.GetAxisRaw("Horizontal");
		_input.y = Input.GetAxisRaw("Vertical");
		_input.Normalize();

		if (_input != Vector2.zero)
		{
			LastMoveDir = _input;

			dustTimer += Time.deltaTime;
			if (dustTimer >= dustCooldown)
			{
				PlayDust();
				dustTimer = 0;
			}
		}

		bool isMove = _input != Vector2.zero;
		animator.SetBool("IsMove", isMove);

		if (_input.x != 0)
		{
			Vector3 scale = visual.localScale;
			scale.x = Mathf.Sign(_input.x) * Mathf.Abs(scale.x);
			visual.localScale = scale;
		}
	}

	void FixedUpdate()
    {
        _rb.velocity = _input * moveSpeed;
    }
}

