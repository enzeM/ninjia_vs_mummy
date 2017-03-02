using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeakEnemy : MonoBehaviour 
{

	[SerializeField]
	private int enemyHealth;

	public int enemyMoveSpeed;

	//attacking
	[SerializeField]
	private float prepareAttackTime;
	private float startAttackTime;
	private float nextAttackTime;

	//facing
	[SerializeField]
	private GameObject enemyGraphic;
	public bool faceRight {get; private set;}
	private bool canFlip;
	private float flipFrequence;
	private float nextFlipTime;


	//melee attack or long-range attack 
	public bool Attack 
	{
		get;
		set;
	}

	public Animator EnemyAnimator 
	{
		get;
		private set;
	}	

	public Rigidbody2D EnemyBody 
	{
		get;
		private set;
	}

	public virtual void Start () 
	{
		EnemyAnimator = GetComponentInChildren<Animator> ();
		EnemyBody = GetComponent<Rigidbody2D> ();

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
		//if player is in enemy's attack area, 
		//1. face to player if not
		//2. prepare attack
		//3. toward player or shoot player
		if(collider.CompareTag ("Player")) 
		{
			float targetPosX = collider.gameObject.transform.position.x;
			float myPosX = this.transform.position.x;
			if(targetPosX > myPosX & !faceRight)
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
	}

	void OnTriggerStay2D (Collider2D collider)
	{
		if(collider.CompareTag ("Player") && startAttackTime <= Time.time)
		{
			EnemyAttack (); //override attack based on enemy's characteristics
			canFlip = false; //ensure canFlip always false if player is in enemy's attack region
		}
	}

	void OnTriggerExit2D (Collider2D collider)
	{
		print("exit");
		canFlip = true;	
		EnemyBody.velocity = new Vector2 (0f, 0f);
		EnemyAnimator.SetBool("attack", false);
	}

	void Update () {
		EnemyPatrol ();
		print(canFlip);
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
