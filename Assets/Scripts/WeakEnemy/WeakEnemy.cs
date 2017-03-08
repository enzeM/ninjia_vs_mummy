using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class WeakEnemy : MonoBehaviour 
{
	//difine enemy's health
	[SerializeField]
	public int enemyHealth;

	//define enemy move speed
	[SerializeField]
	protected int enemyMoveSpeed;

	//define if the enemy have a target to catch
	public GameObject target {
		get;
		set;
	}

	//facing
	public bool faceRight { 
		get; 
		private set; 
	}

	public float prepareAttackTime;
	private float startAttackTime;
	private float nextAttackTime;

	//enemy damage & die
	[SerializeField]
	private string damageSources;
	//what will happen when enemy take damage and die
	public virtual void TakeDamage ()
	{

	}
	public abstract IEnumerator Die ();

	//define the slider that indicate the health of enemy
	[SerializeField]
	protected Slider enemyHealthSlider;

	//enemy dead or not
	public bool IsDead {
		get {
			return enemyHealth <= 0;
		}
	}

	//enemy's animator
	public Animator EnemyAnimator { 
		get; 
		private set;
	}
	//enemy's rigidbody
	public Rigidbody2D EnemyBody { 
		get; 
		private set;
	}

	public virtual void Start () 
	{
		faceRight = true;
		EnemyAnimator = GetComponent<Animator> ();
		EnemyBody = GetComponent<Rigidbody2D> ();
		enemyHealthSlider.maxValue = enemyHealth;			
	}

	//flip enemy
	public void EnemyFlip ()
	{
		faceRight = !faceRight;
		float facingX = transform.localScale.x;
		facingX *= -1f;
		transform.localScale = new Vector3 (facingX,
			transform.localScale.y, transform.localScale.z);
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		//if player hit enemy, then enemy take damage
		if(collider.CompareTag(damageSources)){
			TakeDamage ();
		}
		if (collider.CompareTag ("Edge")) {
			EnemyFlip ();
		}
	}

	public virtual void TargetEnter ()
	{
		//if player is in enemy's attack area, 
		//1. face to player if not
		float targetPosX = target.transform.position.x;
		float myPosX = this.transform.position.x;
		if ((targetPosX > myPosX && !faceRight) || (targetPosX < myPosX && faceRight)) {
			EnemyFlip ();
		}
	}

	public abstract void EnemyAttack();

	public virtual void TargetExit ()
	{
		
	}

	void Update () 
	{
		
	}

	public abstract void EnemyPatrol ();

}
