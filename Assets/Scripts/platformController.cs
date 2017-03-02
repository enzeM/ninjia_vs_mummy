using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformController : MonoBehaviour {
	private BoxCollider2D playerCollider;
	//private Animator playerAnimator;

	[SerializeField]
	private BoxCollider2D platformCollider;
	[SerializeField]
	private BoxCollider2D platformTrigger;
	// Use this for initialization
	void Start () {
		playerCollider = GameObject.Find ("Player").GetComponent<BoxCollider2D> ();
		//playerAnimator = GameObject.Find ("Player").GetComponent<Animator> ();
		Physics2D.IgnoreCollision (platformCollider, platformTrigger, true);
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.name == "Player"){
			Physics2D.IgnoreCollision (platformCollider, playerCollider, true);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.name == "Player"){
			Physics2D.IgnoreCollision (platformCollider, playerCollider, false);
		}
	}
}
