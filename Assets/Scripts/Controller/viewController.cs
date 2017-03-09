using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
	this is view Controller
	it is used to move camera and background follow the player
*/
public class viewController : MonoBehaviour {
	//set the limit pos
	[SerializeField]
	private float xMax;
	private float yMax;
	[SerializeField]
	private float xMin;
	[SerializeField]
	private float yMin;
	//camer move speed
	private float speed;
	//how many layers
	[SerializeField]
	private int layerCount;
	//layers prefab
	[SerializeField]
	private GameObject[] prefabs;
	//target to follow with
	private Transform target;
	//camera
	private GameObject myCamera;
	// Use this for initialization
	void Start () {
		//init
		target = GameObject.Find ("Player").transform;
		myCamera = GameObject.Find ("Main Camera");
		//set maximum valur of camera
		yMax = layerCount * 15 + 3;
		initMap ();
		//different speed for different mode:easy is 0, hard is 0.3
		speed = UIController.cameraSpeed;
	}
	void Update(){
		yMin += speed * Time.deltaTime;
		yMin = Mathf.Max(yMin,target.position.y);
		transform.position = new Vector3(transform.position.x,transform.position.y + yMin,transform.position.z);
	}
	// Update is called once per frame
	void LateUpdate ()
	{
		myCamera.transform.position = new Vector3 (Mathf.Clamp (target.position.x, xMin, xMax), Mathf.Clamp (target.position.y, yMin, yMax), myCamera.transform.position.z);
		transform.position = new Vector3 (transform.position.x, Mathf.Clamp (target.position.y, yMin, yMax), transform.position.z);
	}

	//init map to generate layer randomly
	void initMap ()
	{
		for (int i = 0; i < layerCount; i++) {
			Vector3 pos = new Vector3 (0, i * 15, 0);
			int randomNum = Random.Range (0, prefabs.Length - 1);
			Instantiate (prefabs[randomNum], pos, Quaternion.Euler (new Vector3 (0, 0, 0)));
		}
		Vector3 bossPos = new Vector3 (0, layerCount * 15, 0);
		Instantiate (prefabs[prefabs.Length - 1], bossPos, Quaternion.Euler (new Vector3 (0, 0, 0)));
	}
}
