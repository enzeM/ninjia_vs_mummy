using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight1 : MonoBehaviour {
	[SerializeField] private Zombie enemy;

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			enemy.target = other.gameObject;
			enemy.LookAtTarget();
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.CompareTag("Player")) enemy.AttackTarget();
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player"){
			enemy.target = null;
			enemy.TargetExit();
		}
	}
}
