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
	[SerializeField]
	private GameObject[] prefabs;

	private Transform target;
	private GameObject myCamera;
	// Use this for initialization
	void Start () {
		target = GameObject.Find ("Player").transform;
		myCamera = GameObject.Find ("Main Camera");
		initMap ();
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

	void initMap ()
	{
		for (int i = 0; i < 8; i++) {
			Vector3 pos = new Vector3 (0, i * 15, 0);
			int randomNum = Random.Range (0, prefabs.Length);
			print (randomNum);
			Instantiate (prefabs[randomNum], pos, Quaternion.Euler (new Vector3 (0, 0, 0)));
		}
	}
}
