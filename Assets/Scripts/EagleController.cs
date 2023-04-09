using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : Enemies
{ 

    public override void OnJump()
	{
		GameObject.Find(gameObject.name + ("spawn point")).GetComponent<SpawnController>().Death = true;
		anim.SetTrigger("Death");
		death.Play();
		rb.velocity = Vector2.zero;
		rb.bodyType = RigidbodyType2D.Static;
		GetComponent<Collider2D>().enabled = false;
	}
}
