using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

	private HealthManager playerHealthManager;

	// Use this for initialization
	void Start () 
	{
		playerHealthManager = GameObject.FindObjectOfType<HealthManager> ();
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		if(collider.CompareTag("Player"))
		{
			playerHealthManager.AddHealth();
			Destroy (this.gameObject);
		}
	}
}
