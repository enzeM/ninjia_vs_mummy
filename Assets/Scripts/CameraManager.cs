using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	public Transform target; //target that camera flow
	public float smooth; //how smooth the camera will flow
	private Vector3 offset; //movement of the camera
	private float screenRate = 1.333f;
	private float cameraSize; 
	private float leftLimit;
	private float rightLimit;

	// Use this for initialization
	void Start () {
		cameraSize = GetComponent<Camera>().orthographicSize;
		leftLimit = 0f + (cameraSize * screenRate);
		rightLimit = 16f - (cameraSize * screenRate);
		offset = this.transform.position - target.position;	

	}

	void FixedUpdate() {
		Vector3 nextCamPos = target.position + offset;
		this.transform.position = Vector3.Lerp (this.transform.position, nextCamPos, smooth * Time.deltaTime);

		if(transform.position.x < leftLimit) {
			transform.position = new Vector3 (leftLimit, transform.position.y, transform.position.z);
		} else if (transform.position.x > rightLimit) {
			transform.position = new Vector3 (rightLimit, transform.position.y, transform.position.z);
		} else {
			transform.position = new Vector3 (nextCamPos.x, transform.position.y, transform.position.z);
		}
	}
}
