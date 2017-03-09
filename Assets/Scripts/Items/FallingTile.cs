using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTile : MonoBehaviour 
{

	private Rigidbody2D fallingTileBody;
	//private Collider2D fallingTileCollider;
	[SerializeField]
	private float delayTime;
	[SerializeField]
	private float fallingGravely;

	void Start() 
	{
		fallingTileBody = gameObject.GetComponent<Rigidbody2D> ();
	}

	void OnCollisionEnter2D(Collision2D collision) 
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			StartCoroutine (Fall());
		}
	}

	IEnumerator Fall() 
	{
		yield return new WaitForSeconds (delayTime);
		fallingTileBody.isKinematic = false;
		fallingTileBody.gravityScale = fallingGravely;
		yield return 0;
	}
}
