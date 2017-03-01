using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour 
{

	//private Player player;
	private float nextHarm;
	private float harmRate;
	private HealthManager playerHealth;

	// Use this for initialization
	void Start () 
	{
		nextHarm = 0f;
		harmRate = 0.5f;
		//player = GameObject.FindObjectOfType<Player> (); 
		playerHealth = GameObject.FindObjectOfType<HealthManager> ();
	}	

	void OnTriggerStay2D(Collider2D collider)
	{
		HandleHarm(collider);
	}

	//check player status frequently
	void HandleHarm (Collider2D collider) 
	{
		if (collider.CompareTag("Player"))
		{
			if(playerHealth.GetHealth () > 0 && Time.time > nextHarm) 
			{
				playerHealth.Harm ();
				nextHarm = Time.time + harmRate;
			}
		}
	}
}
