using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : WeakEnemy 
{
	//singleton design for zombie object
	public static Zombie instance;

	private static Zombie Instance {
		get 
		{
			if (instance == null) 
			{
				instance = GameObject.FindObjectOfType<Zombie> ();
			}
			return instance;
		}
	}
	public bool canFlip;
	private float flipFrequence;
	private float nextFlipTime;


	public override void Start ()
	{
		base.Start ();
		canFlip = true;
		flipFrequence = 1;
		nextFlipTime = 0;
	}

	void Update () 
	{
		if(!IsDead)
		{
			EnemyPatrol ();
			if(target != null)
			{
				TargetEnter ();
			}
			else
			{
				TargetExit ();
			}
		}
		if(Player.Instance.IsDead)
		{
			TargetExit ();
		}
	}
	public override void TargetEnter ()
	{
		base.TargetEnter ();
		canFlip = false;
		EnemyAnimator.SetTrigger ("prepareAttack");
		EnemyAnimator.SetBool("attack", true);
		EnemyAttack ();
	}
	//zombie is an melee attack enemy, it will walk toward player
	public override void EnemyAttack ()
	{	
		if (!EnemyAnimator.GetCurrentAnimatorStateInfo (0).IsTag ("Threat")) {
			//move toward the facing area
			if (!faceRight) {
				EnemyBody.velocity = new Vector2 (-1 * enemyMoveSpeed, EnemyBody.velocity.y);
			} else {
				EnemyBody.velocity = new Vector2 (1 * enemyMoveSpeed, EnemyBody.velocity.y);
			}
		}
	}

	public override void TargetExit ()
	{
		base.TargetExit ();
		EnemyAnimator.ResetTrigger ("prepareAttack");
		canFlip = true;
		EnemyAnimator.SetBool ("attack", false);
	}
	public override void TakeDamage ()
	{
		enemyHealth -= 10;
		enemyHealthSlider.value = enemyHealth;
		if(!IsDead){
			enemyHealthSlider.gameObject.SetActive(true);
			EnemyAnimator.SetTrigger ("damage");
		}
		else{
			EnemyAnimator.SetTrigger ("die");
			EnemyBody.velocity = Vector2.zero;
			StartCoroutine(Die());
		}
	}
	public override IEnumerator Die () {
		yield return new WaitForSeconds(3);
		Player.Instance.immortal = false;
		Destroy(gameObject);
	}

	public override void EnemyPatrol ()
	{
		if (Time.time > nextFlipTime && canFlip) {
			EnemyFlip ();
			nextFlipTime = Time.time + flipFrequence;
		}
	}
}
