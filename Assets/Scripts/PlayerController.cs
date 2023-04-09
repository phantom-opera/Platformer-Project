using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	private Rigidbody2D rb;
	private Animator anim;
	private enum State {idle, running, jumping, falling, hurt};
	private State state = State.idle;
	private Collider2D coll;

	[SerializeField] private LayerMask ground;
	[SerializeField] private float speed;
	[SerializeField] private float jumpForce;
	[SerializeField] private int cherries = 0;
	[SerializeField] public TextMeshProUGUI scoreText;
	[SerializeField] private float pushForce = 3f;
	[SerializeField] private AudioSource cherrySound;
	[SerializeField] private AudioSource footstep;
	[SerializeField] private int health;
	[SerializeField] private TextMeshProUGUI healthText;

	[SerializeField] public bool grounded = true;
	[SerializeField] public float groundRadius;

	private void Start()
	{
		rb = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
		coll = gameObject.GetComponent<Collider2D>();
		healthText.text = health.ToString();
	}

    private void Update()
    {

		grounded = Physics2D.OverlapCircle(rb.position, groundRadius, ground);

		if (state != State.hurt)
		{
			Movement();
		}

		StateChange();
		anim.SetInteger("state", (int)state);
	}

	private void Movement()
	{
		float hDirection = Input.GetAxis("Horizontal");

		rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

		if (hDirection < 0)
		{
			transform.localScale = new Vector2(-1, 1);
		}

		else if (hDirection > 0)
		{
			transform.localScale = new Vector2(1, 1);
		}

		else
		{

		}

		if (Input.GetButtonDown("Jump") && grounded)
		{
			Jump();

			//RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, 1.3f, ground);
			//if (hit.collider != null)
				//Jump();
		}
	}

	private void Jump()
	{
		grounded = false;
		rb.velocity = new Vector2(rb.velocity.x, jumpForce);
		state = State.jumping;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Collectable")
		{
			cherrySound.Play();
			Destroy(collision.gameObject);
			cherries += 1;
			PlayerPrefs.SetInt("CurrentScore", cherries);
			scoreText.text = cherries.ToString();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Enemy" )
		{
			Enemies enemy = collision.gameObject.GetComponent<Enemies>();
			if(state == State.falling)
			{
				enemy.OnJump();
				Jump();
			}
			else
			{
				state = State.hurt;
				health -= 1;
				healthText.text = health.ToString();

				if(health <= 0)
				{
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
				if (collision.gameObject.transform.position.x > transform.position.x)
				{
					rb.velocity = new Vector2(-pushForce, rb.velocity.y);
				}

				else
				{
					rb.velocity = new Vector2(pushForce, rb.velocity.y);
				}
			}

		}
	}

	private void StateChange()
	{
	    
		if(state == State.jumping)
		{
			if(rb.velocity.y < .1f)
			{
				state = State.falling;
			}
		}

		else if(state == State.falling)
		{
			if (coll.IsTouchingLayers(ground))
			{
				state = State.idle;
			}
		}

		else if (state == State.hurt)
		{
			if(Mathf.Abs(rb.velocity.x) < .1f)
			{
				state = State.idle;
			}
		}

		else if(Mathf.Abs(rb.velocity.x) > 2f)
		{
			state = State.running;
		}

		else
		{
			state = State.idle;
		}
	}

	private void Footsteps()
	{
		footstep.Play();
	}
}
