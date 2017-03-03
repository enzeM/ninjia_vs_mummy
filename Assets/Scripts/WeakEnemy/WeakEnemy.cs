using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class WeakEnemy : MonoBehaviour 
{

	[SerializeField]
	private int enemyHealth;

	//attacking
	[SerializeField]
	public float prepareAttackTime;
	public float startAttackTime;
	public float nextAttackTime;
	public GameObject target;

	//facing
	[SerializeField]
	private GameObject enemyGraphic;
	public bool faceRight { get; private set; }
	public bool canFlip;
	private float flipFrequence;
	private float nextFlipTime;

	//enemy damage & die
	[SerializeField]
	private string damageSources;
	[SerializeField]
	private Slider enemyHealthSlider;
	public bool IsDead {
		get {
			return enemyHealth <= 0;
		}
	}


	//melee attack or long-range attack 
	public bool Attack { get; set; }

	public Animator EnemyAnimator { get; private set; }

	public Rigidbody2D EnemyBody { get; private set; }

	public virtual void Start () 
	{
		EnemyAnimator = GetComponentInChildren<Animator> ();
		EnemyBody = GetComponent<Rigidbody2D> ();
		//EnemyBody = GetComponentInChildren<Rigidbody2D> ();

		faceRight = true;			
		canFlip = true;
		flipFrequence = 5f;
		nextFlipTime = 0f;
	}

	public void EnemyFlip()
	{
		if(!canFlip)
		{
			return;
		}
		else //flip if fliping is allowed
		{
			float facingX = enemyGraphic.transform.localScale.x;
			facingX *= -1f;
			enemyGraphic.transform.localScale = new Vector3 (facingX,
				enemyGraphic.transform.localScale.y, enemyGraphic.transform.localScale.z);
			faceRight = !faceRight;
		}
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		
		HandleDamage (collider);
	}

	private void HandleDamage (Collider2D collider) 
	{
		if(collider.CompareTag(damageSources))
		{
			TakeDamage ();
		}
	}

	private void TakeDamage ()
	{
		enemyHealth -= 10;
		enemyHealthSlider.gameObject.SetActive(true);
		enemyHealthSlider.value = enemyHealth;
		if(!IsDead){
			//Debug.Log (health);
			EnemyAnimator.SetTrigger ("damage");
		}
		else{
			EnemyAnimator.SetTrigger ("die");
		}
	}

	public void LookAtTarget ()
	{
		//if player is in enemy's attack area, 
		//1. face to player if not
		//2. prepare attack
		//3. toward player or shoot player
		float targetPosX = target.transform.position.x;
		float myPosX = this.transform.position.x;
		if(targetPosX > myPosX & ! faceRight)
		{
			EnemyFlip();
		}
		else if (targetPosX < myPosX && faceRight)
		{
			EnemyFlip();
		}
		canFlip = false;
		startAttackTime = prepareAttackTime + Time.time;
		EnemyAnimator.SetTrigger ("prepareAttack");
	}


	public void AttackTarget ()
	{
		if(Time.time >= startAttackTime)
		{
			EnemyAttack (); //override attack based on enemy's characteristics
			canFlip = false; //ensure canFlip always false if player is in enemy's attack region
		}
	}

	public void TargetExit ()
	{
		print("exit");
		canFlip = true;	
		EnemyBody.velocity = new Vector2 (0f, 0f);
		print(EnemyBody.velocity);
		EnemyAnimator.SetBool("attack", false);
	}

	void Update () 
	{
		EnemyPatrol ();
	}

	private void EnemyPatrol ()
	{
		if(Time.time > nextFlipTime) 
		{
			if(Random.Range(0, 10) <= 5)
			{
				EnemyFlip ();
				nextFlipTime = Time.time + flipFrequence;
			}
		}
	}
		
	public abstract void EnemyAttack();

}
