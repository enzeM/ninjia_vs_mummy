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
	void Start () {
		tilePosX = this.transform.position.x;	
		tilePosY = this.transform.position.y;
		tilePosZ = this.transform.position.z;
		upperLimit = new Vector2 (this.transform.position.x + range, this.transform.position.y + range);
		lowerLimit = new Vector2 (this.transform.position.x - range, this.transform.position.y - range);
	}
	
	// Update is called once per frame
	void Update () {
		if(vertical) {
			VerticalMovement ();
		} else {
			HorizontalMovement ();			
		}
	}

	void OnCollisionStay2D (Collision2D collision) {
		if(collision.gameObject.name == "Player") {
			collision.transform.parent = this.transform;
		}
	}

	void OnCollisionExit2D (Collision2D collision) {
		collision.transform.parent = null;
	}

	private void HorizontalMovement() {
		if(transform.position.x < lowerLimit.x || transform.position.x > upperLimit.x) {
			changeDirection = !changeDirection;
		}
		if(changeDirection) {
			tilePosX -= moveSpeed * Time.deltaTime;
		} else {
			tilePosX += moveSpeed * Time.deltaTime;
		}
		this.transform.position = new Vector3(tilePosX, tilePosY, tilePosZ);
	}

	private void VerticalMovement() {
		if(transform.position.y < lowerLimit.y || transform.position.y > upperLimit.y) {
			changeDirection = !changeDirection;
		}
		if(changeDirection) {
			tilePosY -= moveSpeed * Time.deltaTime;
		} else {
			tilePosY += moveSpeed * Time.deltaTime;
		}
		this.transform.position = new Vector3(tilePosX, tilePosY, tilePosZ);
	}
}
