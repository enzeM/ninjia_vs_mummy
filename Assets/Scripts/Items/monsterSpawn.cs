using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterSpawn : MonoBehaviour {
	//monster prefabs
	[SerializeField]
	private List<GameObject> monsters;
	[SerializeField]
	protected Transform spawnPos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		int randomNum = Random.Range (0, monsters.Count);
		Instantiate (monsters[randomNum], spawnPos.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
		Destroy (this.gameObject);
	}
}
