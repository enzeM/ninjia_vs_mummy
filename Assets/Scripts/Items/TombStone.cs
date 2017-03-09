using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombStone : MonoBehaviour {
	[SerializeField]
	private List<GameObject> monsters;
	[SerializeField]
	private float spawnTime;
	private float timer = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer>=spawnTime){
			int randomNum = Random.Range (0, monsters.Count);
			Instantiate (monsters[randomNum], transform.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
			timer = 0;
		}

	}
}
