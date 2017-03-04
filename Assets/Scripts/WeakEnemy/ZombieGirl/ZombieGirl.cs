using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGirl : WeakEnemy {

	//singleton design for zombie object
	public static ZombieGirl instance;

	private static ZombieGirl Instance {
		get 
		{
			if (instance == null) 
			{
				instance = GameObject.FindObjectOfType<ZombieGirl> ();
			}
			return instance;
		}
	}
	//how height it can jump
	[SerializeField]
	private int jumpForce;
	//on ground?
	public bool OnGround {
		get {
			return EnemyBody.velocity.y == 0;
		}
	}

	public override void Start ()
	{
		base.Start ();
	}

	void Update ()
	{
		if (!IsDead) {
			EnemyPatrol ();
			if (target != null) {
				TargetEnter ();
			} 
			if(OnGround)
			{
				EnemyBody.AddForce (new Vector2 (0, jumpForce));
			}

		}
		if(Player.Instance.IsDead)
		{
			
		}
	}
	public override void TargetEnter ()
	{
		base.TargetEnter ();
	}
	//zombie is an melee attack enemy, it will walk toward player
	public override void EnemyAttack ()
	{	
		
	}

	public override void TakeDamage ()
	{
//		enemyHealth -= 10;
//		enemyHealthSlider.value = enemyHealth;
//		if(!IsDead){
//			enemyHealthSlider.gameObject.SetActive(true);
//			EnemyAnimator.SetTrigger ("damage");
//		}
//		else{
//			EnemyAnimator.SetTrigger ("die");
//			EnemyBody.velocity = Vector2.zero;
//			StartCoroutine(Die());
//		}
	}
	public override IEnumerator Die () {
		yield return new WaitForSeconds(3);
		Player.Instance.immortal = false;
		Destroy(gameObject);
	}

	public override void EnemyPatrol ()
	{
		transform.Translate (GetDirection () * (enemyMoveSpeed * Time.deltaTime));	
	}

	public Vector2 GetDirection(){
		return faceRight ? Vector2.right : Vector2.left;
	}
}
