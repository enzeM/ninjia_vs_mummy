using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viewController : MonoBehaviour {

	[SerializeField]
	private float xMax;
	[SerializeField]
	private float yMax;
	[SerializeField]
	private float xMin;
	[SerializeField]
	private float yMin;
	[SerializeField]
	private float speed;

	private Transform target;
	private GameObject myCamera;
	// Use this for initialization
	void Start () {
		target = GameObject.Find ("Player").transform;
		myCamera = GameObject.Find ("Main Camera");

	}
	void Update(){
		yMin += speed * Time.deltaTime;
		yMin = Mathf.Max(yMin,target.position.y);
		transform.position = new Vector3(transform.position.x,transform.position.y + yMin,transform.position.z);
	}
	// Update is called once per frame
	void LateUpdate () {
		myCamera.transform.position = new Vector3 (Mathf.Clamp (target.position.x, xMin, xMax),Mathf.Clamp (target.position.y, yMin, yMax), myCamera.transform.position.z);
		transform.position = new Vector3 (transform.position.x, Mathf.Clamp (target.position.y, yMin, yMax), transform.position.z);
	}
}
