using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : MonoBehaviour {

	private Player player;
	private int playerDefaultSpeed;

	[SerializeField]
	private float aliveTime;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindObjectOfType<Player> ();		
		playerDefaultSpeed = player.GetMoveSpeed();
	}
	
	void OnTriggerEnter2D (Collider2D collider) 
	{
		if (collider.CompareTag("Player") && !player.IsBoost())
		{
			player.BoostMoveSpeed ();
			player.SetIsBoost (true);
			player.StartCoroutine (RevertMoveSpeed());	
		}
		Destroy (this.gameObject);
	}

	//revert back to default speed after aliveTime of the accelerator
	IEnumerator RevertMoveSpeed() 
	{
		yield return new WaitForSeconds (aliveTime);
		player.setMoveSpeed(playerDefaultSpeed);
		player.SetIsBoost(false);
	}
}
