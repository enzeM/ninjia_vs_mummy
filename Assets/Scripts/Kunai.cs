using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Kunai : MonoBehaviour 
{

	private Rigidbody2D kunaiBody;
	[SerializeField]
	private float speed;
	[SerializeField]
	private float aliveTime;
	[SerializeField]
	private int weaponDamage;

	void Awake() 
	{
		kunaiBody = GetComponent<Rigidbody2D>();		
		//decided bullet rotation
		if(transform.localRotation.z > 0) {
			kunaiBody.AddForce(new Vector2(-1, 0) * speed, ForceMode2D.Impulse);	
		} else {
			kunaiBody.AddForce(new Vector2(1, 0) * speed, ForceMode2D.Impulse);
		}
		Destroy(gameObject, aliveTime);
	}

	//bullet hit target
	void OnTriggerEnter2D (Collider2D collider) 
	{
		HandleKunaiAttack (collider);
	}

	void OnTriggerStay2D (Collider2D collider) 
	{
		HandleKunaiAttack (collider);
	}

	private void HandleKunaiAttack (Collider2D collider) {
		if(collider.gameObject.layer == LayerMask.NameToLayer("Shootable")) {
			Destroy(gameObject);
			if (collider.CompareTag ("Enemy")) {
				EnemyHealth enemyHealth = collider.gameObject.GetComponent<EnemyHealth> ();
				enemyHealth.addDamage(weaponDamage);
			}
		}
	}
}
