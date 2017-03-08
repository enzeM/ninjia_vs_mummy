using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
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
			if(!Player.Instance.IsDead) 
			{
				StartCoroutine (Player.Instance.TakeDamage ());
			}
		}
	}
}
