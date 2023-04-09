using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
	protected Animator anim;
	protected Rigidbody2D rb;
	protected AudioSource death;

	protected virtual void Start()
	{
		anim = gameObject.GetComponent<Animator>();
		rb = gameObject.GetComponent<Rigidbody2D>();
		death = gameObject.GetComponent<AudioSource>();
	}

	public virtual void OnJump()
	{
		anim.SetTrigger("Death");
		death.Play();
		rb.velocity = Vector2.zero;
		rb.bodyType = RigidbodyType2D.Static;
		GetComponent<Collider2D>().enabled = false;
	}

	private void Death()
	{
		Destroy(this.gameObject);
	}
}
