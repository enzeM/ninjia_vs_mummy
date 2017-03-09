using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Kunai : MonoBehaviour 
{
	private Rigidbody2D kunaiBody;
	[SerializeField]
	private float speed;
	[SerializeField]
	private int weaponDamage;
	[SerializeField]
	private string attackTarget;

	void Start() 
	{
		kunaiBody = GetComponent<Rigidbody2D>();		
		//decided bullet rotation
		if(transform.localRotation.z > 0) {
			kunaiBody.AddForce(new Vector2(-1, 0) * speed, ForceMode2D.Impulse);	
		} else {
			kunaiBody.AddForce(new Vector2(1, 0) * speed, ForceMode2D.Impulse);
		}

	}
	void FixedUpdate(){
		
	}

	void OnBecameInvisible()
	{
		Destroy (gameObject);
	}

	void OnTriggerEnter2D (Collider2D collider) 
	{
		if (collider.CompareTag(attackTarget))
		{
			Destroy (this.gameObject);
		}
	}
}
