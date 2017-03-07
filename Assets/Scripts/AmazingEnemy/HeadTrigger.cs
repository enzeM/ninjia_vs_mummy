using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTrigger : MonoBehaviour {
	[SerializeField]
	public Zombie1 enemy;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player")
		{
			Player.Instance.MyRigibody.velocity = new Vector2 (Player.Instance.MyRigibody.velocity.x, 5);
			if(!enemy.IsDead) 
			{
				StartCoroutine (enemy.TakeDamage (40));
			}
		}
	}
}
