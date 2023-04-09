﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : Enemies
{
	[SerializeField] private float leftLimit;
	[SerializeField] private float rightLimit;
	[SerializeField] private float jumpLength = 10f;
	[SerializeField] private float jumpHeight = 15f;
	[SerializeField] private LayerMask ground;

	private Collider2D coll;

	protected override void Start()
	{
		base.Start();
		coll = gameObject.GetComponent<Collider2D>();
	}
	private bool facingLeft = true;

    // Update is called once per frame
    void Update()
    {
		if (anim.GetBool("Jumping"))
		{
			if(rb.velocity.y < .1f)
			{
				anim.SetBool("Falling", true);
				anim.SetBool("Jumping", false);
			}
		}

		if (coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
		{
			anim.SetBool("Falling", false);
		}
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

				if (coll.IsTouchingLayers(ground))
				{
					rb.velocity = new Vector2(-jumpLength, jumpHeight);
					anim.SetBool("Jumping", true);
				}
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

				if (coll.IsTouchingLayers(ground))
				{
					rb.velocity = new Vector2(jumpLength, jumpHeight);
					anim.SetBool("Jumping", true);
				}
			}

			else
			{
				facingLeft = true;
			}
		}
	}

}
