using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie1 : AmazingEnemy {

	private bool immortal = false;

	[SerializeField]private float immortalTime;

	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		myState = STATE.Idle;
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (myState) {
		case STATE.Idle:
			StartCoroutine (Idle ());
			break;
		case STATE.Patrol:
			StartCoroutine (Patrol ());
			break;
		case STATE.Catch:
			Catch ();
			break;
		}
	}

	private IEnumerator IndicateImmortal(){
		while(immortal){
			spriteRenderer.enabled = false;
			yield return new WaitForSeconds (.1f);
			spriteRenderer.enabled = true;
			yield return new WaitForSeconds (.1f);
		}
	}

	public IEnumerator Idle ()
	{
		MyAnimator.SetFloat ("speed", 0);
		MyAnimator.SetFloat ("runSpeed", 0);
		if(target!= null){
			yield return null;
			LookAtTarget ();
			myState = STATE.Catch;
		}else{
			yield return new WaitForSeconds (1.5f);
			myState = STATE.Patrol;
		}
	}

	public IEnumerator Patrol ()
	{
		MyAnimator.SetFloat ("speed", 1);
		//if((facingRight && transform.position.x <=rightEdge.position.x) || (!facingRight && transform.position.x >=leftEdge.position.x))
			transform.Translate (GetDirection () * (walkSpeed * Time.deltaTime));
		if(target!= null){
			LookAtTarget ();
			myState = STATE.Catch;
		}else{
			yield return new WaitForSeconds (3);
			myState = STATE.Idle;
		}
	}

	public void Catch ()
	{
		MyAnimator.SetFloat ("runSpeed", 1);
		//if((facingRight && transform.position.x <= rightEdge.position.x) || (!facingRight && transform.position.x >= leftEdge.position.x))
			transform.Translate (GetDirection () * (runSpeed * Time.deltaTime));
		if(target == null){
			myState = STATE.Idle;
		}
	}

	public override IEnumerator TakeDamage (int atk)
	{
		if (!immortal) {
			health -= atk;
			healthSlider.value = health;
			if (!IsDead) {
				healthSlider.gameObject.SetActive(true);
				MyAnimator.SetTrigger ("damage");
				immortal = true;
				StartCoroutine (IndicateImmortal ());
				yield return new WaitForSeconds (immortalTime);
				immortal = false;
			} else {
				MyAnimator.SetTrigger ("die");
				MyRigidbody.velocity = Vector2.zero;
				StartCoroutine(base.Die());
			}
		}	
	}

	public override void OnTriggerEnter2D(Collider2D other){
		base.OnTriggerEnter2D (other);
		if(other.tag == "PlayerDamage")
		{
			target = Player.Instance.gameObject;
		}
	}
}
