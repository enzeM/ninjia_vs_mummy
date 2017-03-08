using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour {
	[SerializeField] private AmazingEnemy enemy;

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			enemy.target = other.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player"){
			enemy.target = null;
		}
	}
}
