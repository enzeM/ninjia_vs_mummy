using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D collider) 
	{
		if (collider.CompareTag("Player") && Player.Instance.curHealth < Player.Instance.health)
		{
			Player.Instance.HpUp();
		}
		Destroy (this.gameObject);
	}
}
