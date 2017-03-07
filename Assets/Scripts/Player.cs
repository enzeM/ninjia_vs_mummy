using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void DeadEventHandler();

public class Player : Character
{
	private static Player instance;

	public static Player Instance {
		get {
			if (instance == null) {
				instance = GameObject.FindObjectOfType<Player> ();
			}
			return instance;
		}
	}
	[SerializeField]
	private Slider healthSlider;
	public int curHealth {get; private set;}

	public override void Death ()
	{
		MyRigibody.velocity = Vector2.zero;
		//MyAnimator.SetTrigger ("idle");
		//curHealth = health;
		//transform.position = startPos;
	}
	public event DeadEventHandler Dead;

	public bool immortal = false;

	[SerializeField]
	private float immortalTime;

	private SpriteRenderer spriteRenderer;

	private IEnumerator IndicateImmortal(){
		while(immortal){
			spriteRenderer.color = new Color(1, 1, 1, 0.01F);
			yield return new WaitForSeconds (.1f);
			spriteRenderer.color = new Color(1, 1, 1, 1);
			yield return new WaitForSeconds (.1f);
		}
	}
	public override bool IsDead {
		get {
			return curHealth <= 0;
		}
	}
	public bool fightBoss;
	public override IEnumerator TakeDamage ()
	{
		if (!immortal) {
			curHealth -= 10;
			if (!IsDead) {
				MyAnimator.SetTrigger ("damage");
				immortal = true;

				StartCoroutine (IndicateImmortal ());
				yield return new WaitForSeconds (immortalTime);

				immortal = false;
			} else {
				MyAnimator.SetLayerWeight (1, 0);
				MyAnimator.SetTrigger ("die");
				//let enemy know player is dead
				OnDead ();
			}
		}	
	}

	public float defaultGravity;

	[SerializeField]
	private bool airControl;

	private float groundedRaduis = 0.2f;
	public Transform groundCheck;
	public LayerMask whatIsGround; //define what is ground for player

	//boost speed param, ensure speed allow to boost only once
	private bool isBoost;

	[SerializeField]
	//how high the player can jump
	private float jumpForce;

	public Rigidbody2D MyRigibody{
		get;
		set;
	}

	public bool Slide{
		get;
		set;
	}
	public bool Jump{
		get;
		set;
	}
	public bool OnGround{
		get;
		set;
	}

	// Use this for initialization
	public override void Start () 
	{
		base.Start();
		fightBoss = false;
		MyRigibody = GetComponent<Rigidbody2D> ();		
		spriteRenderer = GetComponent<SpriteRenderer> ();
		defaultGravity = MyRigibody.gravityScale;

		curHealth = health;
		healthSlider.maxValue = health;
		healthSlider.value = curHealth;

		startPos = transform.position;

		isBoost = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(IsDead){
			Death ();
		}
		if (!TakingDamage && !IsDead) {
			HandleInput ();
		}
	}

	//ensure no matter a computer is faster or slow, the player will move with same speed
	void FixedUpdate() 
	{ 
		if (!TakingDamage && !IsDead) {
			//check fequently player is on ground -> return true if player on the ground, else return false
			OnGround = IsGrounded ();
			//flip player sprite when moving left or right 
			float horizontal = Input.GetAxis ("Horizontal");
			HandleMovement (horizontal);

			Flip (horizontal);
		
			HandleLayers ();
		}
		HandleHealth ();
	}
	private void HandleHealth() 
	{
		if(curHealth >= health)
		{
			curHealth = health;
		}
		healthSlider.value = curHealth;
	}
	public void HpUp()
	{
		curHealth += 10;
	}
	private bool IsGrounded()
	{
		if(MyRigibody.velocity.y <= 0)
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRaduis, whatIsGround);
			for(int i = 0; i <colliders.Length;i++)
			{
				if(colliders[i].gameObject != gameObject)
				{
					return true;
				}
			}
		}
		return false;
	}

	private void HandleMovement(float horizontal) 
	{
		if(MyRigibody.velocity.y < 0)
		{
			MyAnimator.SetBool ("land", true);
		}
		if(!Attack && !Shoot && (OnGround || airControl))
		{
			MyRigibody.velocity = new Vector2 (horizontal * moveSpeed, MyRigibody.velocity.y);
		}

		if(Jump && OnGround)
		{
			MyRigibody.AddForce (new Vector2 (0, jumpForce));
		}
		MyAnimator.SetFloat ("speed", Mathf.Abs (horizontal));
	}

	//create bullet(kunai) to fire, take value as parameter, 0 is ground fire, 1 is air fire
	private void Fire (int value)
	{
		if (!OnGround && value == 1 || OnGround && value == 0) {
			Fire ();
		}
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
	public int GetHealth(){
		return this.curHealth;
	}
	//change face direction properly
	private void Flip(float horizontal) 
	{
		if (horizontal < 0 && faceRight || horizontal > 0 && !faceRight) 
		{
			ChangeDirection ();
		}	
	}

	private void HandleLayers()
	{
		if(!OnGround)
		{
			MyAnimator.SetLayerWeight (1, 1);
		}
		else
		{
			MyAnimator.SetLayerWeight (1, 0);
		}
	}

	private void HandleInput() 
	{
		if (Input.GetKeyDown (KeyCode.J)) 
		{
			MyAnimator.SetTrigger ("attack");
		} 
		if (Input.GetKeyDown (KeyCode.K)) 
		{ 
			MyAnimator.SetTrigger ("throw");
		}
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			MyAnimator.SetTrigger ("jump");
		}
		if(Input.GetKeyDown(KeyCode.LeftShift)) 
		{
			MyAnimator.SetTrigger ("slide");
		}
		//listening key status to generate glide param
		if(Input.GetKey(KeyCode.W))
		{
			MyAnimator.SetBool ("glide2", true);
		}
		if(Input.GetKeyUp(KeyCode.W))
		{
			MyAnimator.SetBool ("glide2", false);
		}
	}
	public void OnDead(){
		if(Dead != null){
			Dead ();
		}
	}
	void OnBecameInvisible(){
		curHealth = 0;
	}
}
