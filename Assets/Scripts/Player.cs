using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{

	private Rigidbody2D playerBody;
	private Animator playerAnimator;
	private float defaultGravity;

	private bool faceRight;
	private bool attack;
	private bool slide;
	private bool jump;
	private bool harm;
	private bool die;
	private bool fire;
	private bool glide;

	//check player on ground
	private bool grounded;
	private float groundedRaduis = 0.2f;
	public Transform groundCheck;
	public LayerMask whatIsGround; //define what is ground for player

	//shooting param
	[SerializeField]
	private Transform shootPoint;
	[SerializeField]
	private GameObject bullet;
	private float fireRate = 0.5f;
	private float nextFire = 0f;

	//boost speed param, ensure speed allow to boost only once
	private bool isBoost;

	[SerializeField]
	//how fast the player can move
	private int moveSpeed;
	[SerializeField]
	//how high the player can jump
	private float jumpForce;


	// Use this for initialization
	void Start () 
	{
		playerBody = GetComponent<Rigidbody2D> ();		
		playerAnimator = GetComponent<Animator> ();
		defaultGravity = playerBody.gravityScale;
		faceRight = true;
		fire = false;
		harm = false;
		die = false;
		glide = false;

		isBoost = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		HandleInput ();
	}

	//ensure no matter a computer is faster or slow, the player will move with same speed
	void FixedUpdate() 
	{ 
		//check fequently player is on ground -> return true if player on the ground, else return false
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRaduis, whatIsGround);
		playerAnimator.SetBool ("ground", grounded);

		//how fast we are moving up or down
		playerAnimator.SetFloat("vSpeed", playerBody.velocity.y);

		//flip player sprite when moving left or right 
		float horizontal = Input.GetAxis ("Horizontal");

		if (!die) //not able to move after player is dead
		{
			HandleMovement (horizontal);
			Flip (horizontal);
			HandleAttack ();
			HandleFire ();
			HandleJumpFire ();
		}
		ResetValues ();
	}

	private void HandleMovement(float horizontal) 
	{
		//if player animation is neither attack or jump attack, player allowed to move
		if (!this.playerAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("Attack")) 
		{ 
			playerBody.velocity = new Vector2 (horizontal * moveSpeed, playerBody.velocity.y);
		}
		//handle slide -> slide can not operate sequentially, slide is allowed only when player is running
		if (slide 
			&& this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Run") 
			&& !this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide")) 
		{
			playerAnimator.SetTrigger ("slide");
		}
		//handle jump -> jump can not operate sequentially
		if (jump && !glide && !this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) 
		{
			playerAnimator.SetBool ("ground", false);
			playerBody.AddForce (new Vector2 (0, jumpForce));
		}
		//harm reaction when attacked by obstacle or ememy
		if(harm && !this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Harm")) 
		{
			playerAnimator.SetTrigger("harm");
		}
		//glide in the air 
		if(!grounded) {
			playerAnimator.SetBool("glide", glide);
			HandleGlide ();
		}

		playerAnimator.SetFloat ("speed", Mathf.Abs(horizontal)); //change states between idel and run
	}

	private void HandleAttack() 
	{
		//player allow to fire must fulfill all following conditions
		//1. player is grounded 
		//2. player is not in other attack animate state 
		//3. current time is larger than player's fire stand by time
		if (grounded && attack && !this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) 
		{
			playerAnimator.SetTrigger ("attack");	
			playerBody.velocity = Vector2.zero; // also used to stop move and attack at the same time
		}
	}

	private void HandleFire() 
	{
		//player allow to fire must fulfill all following conditions
		//1. player is grounded 
		//2. player is not in any other attack animate state 
		//3. current time is larger than player's fire stand by time
		if(grounded && fire && Time.time > nextFire) 
		{
			playerAnimator.SetTrigger ("fire");
			nextFire = Time.time + fireRate;
			StartCoroutine (FireWithFireRate(this.fireRate));
			playerBody.velocity = Vector2.zero; //stop move and attack at the same time
		}	
	}

	private void HandleJumpFire()
	{
		//player allow to fire must fulfill all following conditions
		//1. player is on air(not on ground) 
		//2. player is not in any other attack animate state 
		//3. current time is larger than player's fire stand by time
		if(!grounded && fire && Time.time > nextFire)
		{
			playerAnimator.SetTrigger ("jumpFire");
			nextFire = Time.time + fireRate;
			StartCoroutine (FireWithFireRate(this.fireRate/10));
		}	
	}

	//create bullet(kunai) to fire
	private void Fire() 
	{
		if(faceRight) 
		{
			Instantiate(bullet, shootPoint.position, Quaternion.Euler (new Vector3(0, 0, 0)));
		} 
		else 
		{
			Instantiate(bullet, shootPoint.position, Quaternion.Euler (new Vector3(0, 0, 180)));
		}
	}

	//fire with delay time that consistance with fire rate
	private IEnumerator FireWithFireRate(float fireRate) 
	{
		yield return new WaitForSeconds (fireRate);
		Fire();
		yield return 0;
	}

	//check glide status to modify player's gravity scale
	private void HandleGlide()
	{
		if(glide) 
		{
			playerBody.gravityScale = 0.2f;
		}
		else 
		{
			playerBody.gravityScale = defaultGravity;
		}
	}

	//setter used to manage player health
	public void setHarm (bool harm)
	{
		this.harm = harm;
	}

	public void setDie (bool die)
	{
		this.die = die;
	}

	//boost move speed params
	public int GetMoveSpeed() 
	{
		return this.moveSpeed;
	}

	public void BoostMoveSpeed () {
		this.moveSpeed *= 2;	
	}

	public void setMoveSpeed(int moveSpeed)
	{
		this.moveSpeed = moveSpeed;
	}

	public bool IsBoost() {
		return this.isBoost;
	}

	public void SetIsBoost(bool isBoost) 
	{
		this.isBoost = isBoost;
	}
		
	/*
	void OnCollisionEnter2D(Collision2D collision) 
	{
		//listen slide status
		if (this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide") 
			&& collision.gameObject.tag == "Block") 
		{
			collision.collider.isTrigger = true;
		}
	}

	void OnCollisionStay2D(Collision2D collision) 
	{
		//listen dunland status
		if (this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("DunLand") 
			&& collision.gameObject.tag == "Block") 
		{
			collision.collider.isTrigger = true;
		}

	}

	void OnTriggerExit2D (Collider2D collider) 
	{
		//revert slide status after player exit item with Block tag
		if (collider.gameObject.tag == "Block") 
		{
			collider.isTrigger = false;
		}
	}
	*/

	private void HandleInput() 
	{
		if (Input.GetKeyDown (KeyCode.J)) 
		{
			attack = true;
		} 
		if (Input.GetKeyDown (KeyCode.K)) 
		{ 
			slide = true;
		}
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			jump = true;
		}
		if(Input.GetKeyDown(KeyCode.L)) 
		{
			fire = true;
		}
		//listening key status to generate glide param
		if(Input.GetKey(KeyCode.W))
		{
			glide = true;
		}
		if(Input.GetKeyUp(KeyCode.W))
		{
			glide = false;
		}
	}

	//change face direction properly
	private void Flip(float horizontal) 
	{
		if (horizontal < 0 && faceRight || horizontal > 0 && !faceRight) 
		{
			faceRight = !faceRight;
			Vector3 newPlayerScale = transform.localScale;
			newPlayerScale.x *= -1;
			transform.localScale = newPlayerScale;
		}	
	}

	//reset values that only operate once each time such as attack and jump
	private void ResetValues() 
	{
		attack = false;
		slide = false;
		jump = false;
		harm = false;
		fire = false;
	}
}
