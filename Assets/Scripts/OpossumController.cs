using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpossumController : Enemies
{
	[SerializeField] private float leftLimit;
	[SerializeField] private float rightLimit;
	[SerializeField] private float speed = 5f;

	protected override void Start()
	{
		base.Start();
	}

	private bool facingLeft = true;

	// Update is called once per frame
	void Update()
    {
		Movement();
    }

	 private void Movement()
	{
		if (facingLeft)
		{
			if (transform.position.x > leftLimit)
			{
				if (transform.localScale.x != 1)
				{
					transform.localScale = new Vector3(1, 1);
				}

				rb.velocity = new Vector2(-speed, 0);

			}

			else
			{
				facingLeft = false;
			}
		}

		else
		{
			if (transform.position.x < rightLimit)
			{
				if (transform.localScale.x != -1)
				{
					transform.localScale = new Vector3(-1, 1);
				}

				rb.velocity = new Vector2(speed, 0);

			}

			else
			{
				facingLeft = true;
			}
		}
	}
}
