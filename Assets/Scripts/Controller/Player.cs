using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//dead event which can inform enemies that player is dead
public delegate void DeadEventHandler();
/**
	Player controller
*/
public class Player : Character
{
	//create player instance
	private static Player instance;

	public static Player Instance {
		get {
			if (instance == null) {
				instance = GameObject.FindObjectOfType<Player> ();
			}
			return instance;
		}
	}
	//player sound effects
	public AudioSource audio;
	public AudioClip jump;
	public AudioClip deathSound;
	public AudioClip runSound;
	public AudioClip shootSound;
	public AudioClip meleeSound;
	public AudioClip hurtSound;
	public AudioClip slideSound;
	//player health slider
	[SerializeField]
	private Slider healthSlider;
	//player current health
	public int curHealth {get; private set;}
	//override function: what will happen when player dead
	public override void Death ()
	{
	}
	//dead event handler
	public event DeadEventHandler Dead;
	//bool value to indicate if player dead
	public bool immortal = false;
	//immortal duration
	[SerializeField]
	private float immortalTime;
	//player sprite
	private SpriteRenderer spriteRenderer;
	//set the color for indicate immortal
	private IEnumerator IndicateImmortal(){
		while(immortal){
			spriteRenderer.color = new Color(1, 1, 1, 0.01F);
			yield return new WaitForSeconds (.1f);
			spriteRenderer.color = new Color(1, 1, 1, 1);
			yield return new WaitForSeconds (.1f);
		}
	}
	//Return: if current health is less than 0: true 
	//		  if current health is larger than 0: false
	public override bool IsDead {
		get {
			return curHealth <= 0;
		}
	}
	//if player enter to the door
	public bool fightBoss;
	//if player kill all bosses
	public bool winBoss;
	//when player take damage, what will happen (override)
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
				immortal = true;
				//let enemy know player is dead
				OnDead ();
			}
		}	
	}
	//default garvity scale: 1
	public float defaultGravity;
	//if the player can move when he jumped
	[SerializeField]
	private bool airControl;
	//check if the player is on the ground
	private float groundedRaduis = 0.2f;
	public Transform groundCheck;
	public LayerMask whatIsGround; //define what is ground for player
	//how high the player can jump
	[SerializeField]
	private float jumpForce;
	//rigibody2d
	public Rigidbody2D MyRigibody{
		get;
		set;
	}
	//Slide
	public bool Slide{
		get;
		set;
	}
	//Jump
	public bool Jump{
		get;
		set;
	}
	//OnGround
	public bool OnGround{
		get;
		set;
	}
	//default move speed : 4
	public int defaultSpeed;
	// Use this for initialization
	public override void Start () 
	{
		base.Start();
		//init values
		defaultSpeed = moveSpeed;
		audio = GetComponent<AudioSource> ();
		fightBoss = false;
		MyRigibody = GetComponent<Rigidbody2D> ();		
		spriteRenderer = GetComponent<SpriteRenderer> ();
		defaultGravity = MyRigibody.gravityScale;

		curHealth = health;
		healthSlider.maxValue = health;
		healthSlider.value = curHealth;

		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{	
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
		Jump = false;
	}
	//update health slider each frame
	private void HandleHealth() 
	{
		if(curHealth >= health)
		{
			curHealth = health;
		}
		healthSlider.value = curHealth;
	}
	//when player pick up the heart item, the health will be up
	public void HpUp()
	{
		this.curHealth += 10;
	}
	//is on the ground ?
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
	//handle movement
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
			audio.PlayOneShot(jump,1);
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
	public void BoostMoveSpeed (float aliveTime) {
		this.moveSpeed *= 2;
		StartCoroutine (resetSpeed (aliveTime));
	}
	//reset move speed
	public IEnumerator resetSpeed (float aliveTime) {
		yield return new WaitForSeconds (aliveTime);
		this.moveSpeed = defaultSpeed;
	}
	//return: current health
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
	//handle anmiator layer for ground and air
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
	//set input control
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
			Jump = true;
			MyAnimator.SetTrigger ("jump");
		}
		if(Input.GetKeyDown(KeyCode.LeftShift)) 
		{
			MyAnimator.SetTrigger ("slide");
		}
		//listening key status to generate glide param
		if(Input.GetKey(KeyCode.W) && !OnGround)
		{
			MyAnimator.SetBool ("glide2", true);
		}
		if(Input.GetKey(KeyCode.W) && OnGround)
		{
			MyAnimator.SetBool ("glide2", false);
		}
		if(Input.GetKeyUp(KeyCode.W))
		{
			MyAnimator.SetBool ("glide2", false);
		}
	}
	//on dead event
	public void OnDead(){
		if(Dead != null){
			Dead ();
		}
	}
	//if player fall down, set current health to 0
	void OnBecameInvisible(){
		audio.PlayOneShot (deathSound, 1);
		curHealth = 0;
	}
	//play shoot sound
	public void PlayShootSound(){
		Player.Instance.audio.PlayOneShot (Player.Instance.shootSound, 1);
	}
	//play melee sound
	public void PlayMeleeSound(){
		Player.Instance.audio.PlayOneShot (Player.Instance.meleeSound, 1);
	}
}
