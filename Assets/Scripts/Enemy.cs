using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Enemy : Character {
	private int enemyCurHealth;

	[SerializeField]
	private Slider enemyHealthSlider;

	public override void Death ()
	{
		Destroy (gameObject);

//		MyAnimator.ResetTrigger ("die");
//		MyAnimator.SetTrigger ("idle");
//		health = 30;
//		transform.position = startPos;
	}


	public override bool IsDead {
		get {
			return health <= 0;
		}
	}

	public override IEnumerator TakeDamage ()
	{
		health -= 10;
		enemyHealthSlider.gameObject.SetActive(true);
		enemyHealthSlider.value = health;
		if(!IsDead){
			//Debug.Log (health);
			MyAnimator.SetTrigger ("damage");
		}
		else{
			MyAnimator.SetTrigger ("die");
			yield return null;
		}
	}

	private IEnemyState currentState;

	public GameObject Target { get; set;}

	[SerializeField]private float meleeRange;
	public bool InMeleeRange{
		get{
			if(Target != null){
				return Vector2.Distance (transform.position, Target.transform.position) <= meleeRange;
			}
			return false;
		}
	}

	[SerializeField]private float throwRange;
	public bool InThrowRange{
		get{
			if(Target != null){
				return Vector2.Distance (transform.position, Target.transform.position) <= throwRange;
			}
			return false;
		}
	}

	// Use this for initialization
	public override void Start () {
		base.Start ();
		enemyHealthSlider.maxValue = health;
		enemyHealthSlider.value = health;
		Player.Instance.Dead += new DeadEventHandler (RemoveTarget);
		ChangeState (new IdleState ());
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!IsDead) {
			if(!TakingDamage){
				currentState.Execute ();
			}
			LookAtTarget ();
		}
		
	}

	public void ChangeState(IEnemyState newState){
		if(currentState != null){
			currentState.Exit ();
		}

		currentState = newState;

		currentState.Enter (this);
	}

	public void Move(){
		if(!Attack && !Shoot){
			MyAnimator.SetFloat ("speed", 1);
			transform.Translate (GetDirection () * (moveSpeed * Time.deltaTime));
		}	
	}

	public Vector2 GetDirection(){
		return faceRight ? Vector2.right : Vector2.left;
	}

	private void LookAtTarget(){
		if(Target != null){
			float xDir = Target.transform.position.x - transform.position.x;

			if(xDir < 0 && faceRight || xDir > 0 && !faceRight){
				ChangeDirection ();
			}
		}
	}

	public void RemoveTarget(){
		Target = null;
		ChangeState (new PatrolState ());
	}

	public override void OnTriggerEnter2D(Collider2D other){
		base.OnTriggerEnter2D (other);
		currentState.OnTriggerEnter (other);
	}
}
