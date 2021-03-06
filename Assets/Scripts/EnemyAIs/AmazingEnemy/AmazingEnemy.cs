﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
	AmazingEnemy: abstract class for enemy
*/
public abstract class AmazingEnemy : MonoBehaviour {
	public enum STATE{
		Idle,
		Patrol,
		Catch,
		Jump,
		Damage,
		Die,
	}
	//define enemy's current state

	protected STATE myState;
	//difine enemy's health
	[SerializeField]
	protected int health;

	[SerializeField]
	protected Transform rightEdge;
	[SerializeField]
	protected Transform leftEdge;

	//define enemy walk speed
	[SerializeField]
	protected int walkSpeed;

	//defing enemy run speed
	[SerializeField]
	protected int runSpeed;

	//define the force of jump
	[SerializeField]
	protected int jumpForce;

	//define a list of damage sources
	[SerializeField]
	protected List<string> damageSources;

	//define if enemy dead
	public bool IsDead {
		get {
			return health <= 0;
		}
	}

	[SerializeField]
	protected Slider healthSlider;

	//define if the enemy have a target to catch
	public GameObject target;

	//facing
	protected bool facingRight;
	//animator
	public Animator MyAnimator {
		get;
		private set;
	}
	//rigidbody
	public Rigidbody2D MyRigidbody {
		get;
		private set;
	}
	// Use this for initialization
	public virtual void Start () {
		facingRight = true;
		MyAnimator = GetComponent<Animator> ();
		MyRigidbody = GetComponent<Rigidbody2D> ();
		healthSlider.maxValue = health;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//change direction
	protected void ChangeDirection()
	{
		facingRight = !facingRight;
		transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y);
	}
	//return: (vector2)right or (vector2)left 
	protected Vector2 GetDirection()
	{
		return facingRight ? Vector2.right : Vector2.left;
	}

	public virtual void OnTriggerEnter2D(Collider2D other){
		//take damage
		if(damageSources.Contains(other.tag)){
			StartCoroutine (TakeDamage (10));
		}
		//changedirection
		if(other.tag == "Edge"){
			ChangeDirection ();
		}
	}
	//look at target
	protected void LookAtTarget(){
		if(target != null){
			float xDir = target.transform.position.x - transform.position.x;

			if(xDir < 0 && facingRight || xDir > 0 && !facingRight){
				ChangeDirection ();
			}
		}
	}
	//take damage
	public abstract IEnumerator TakeDamage (int atk);
	//die
	public virtual IEnumerator Die () {
		yield return new WaitForSeconds(1);
		Player.Instance.immortal = false;
		Destroy(gameObject);
	}



}
