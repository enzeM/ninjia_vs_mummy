using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : MonoBehaviour {
	[SerializeField]
	private float aliveTime;

	// Use this for initialization
	void Start () 
	{
				
	}
	
	void OnTriggerEnter2D (Collider2D collider) 
	{
		if (collider.CompareTag("Player") && Player.Instance.moveSpeed == Player.Instance.defaultSpeed)
		{
			Player.Instance.BoostMoveSpeed (aliveTime);
		}
		Destroy (this.gameObject);
	}
}
