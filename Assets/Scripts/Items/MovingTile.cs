using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovingTile : MonoBehaviour {

	private bool changeDirection;

	private float tilePosX;
	private float tilePosY;
	private float tilePosZ;

	private Vector2 upperLimit;
	private Vector2 lowerLimit;

	[SerializeField]
	private float moveSpeed;
	[SerializeField]
	private float range;
	[SerializeField]
	private bool vertical;
	// Use this for initialization
	void Start () 
	{
		tilePosX = this.transform.position.x;	
		tilePosY = this.transform.position.y;
		tilePosZ = this.transform.position.z;
		upperLimit = new Vector2 (this.transform.position.x + range, this.transform.position.y + range);
		lowerLimit = new Vector2 (this.transform.position.x - range, this.transform.position.y - range);
		//randomly init direction of moving tile
		int randDirection = Random.Range(0,2);
		if (randDirection != 0) 
		{
			changeDirection = true;
		} 
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(vertical) {
			VerticalMovement ();
		} else {
			HorizontalMovement ();			
		}
	}

	// if player on the moving tile, set moving tile as player's transform parent
	void OnCollisionStay2D (Collision2D collision) 
	{
		if(collision.gameObject.CompareTag("Player")) 
		{
			collision.transform.parent = this.transform;
		}
	}

	//reset when player exit
	void OnCollisionExit2D (Collision2D collision) 
	{
		collision.transform.parent = null;
	}

	//horizontal movement
	private void HorizontalMovement() 
	{
		if(transform.position.x <= lowerLimit.x || transform.position.x >= upperLimit.x) 
		{
			changeDirection = !changeDirection;
		}
		if(changeDirection) 
		{
			tilePosX -= moveSpeed * Time.deltaTime;
		} 
		else 
		{
			tilePosX += moveSpeed * Time.deltaTime;
		}
		this.transform.position = new Vector3(tilePosX, tilePosY, tilePosZ);
	}

	//vertical movement
	private void VerticalMovement() 
	{
		if(transform.position.y <= lowerLimit.y || transform.position.y >= upperLimit.y) 
		{
			changeDirection = !changeDirection;
		}
		if(changeDirection) 
		{
			tilePosY -= moveSpeed * Time.deltaTime;
		} 
		else 
		{
			tilePosY += moveSpeed * Time.deltaTime;
		}
		this.transform.position = new Vector3(tilePosX, tilePosY, tilePosZ);
	}
}
