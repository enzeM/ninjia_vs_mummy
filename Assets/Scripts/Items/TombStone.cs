using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TombStone : MonoBehaviour {
	[SerializeField]
	private List<GameObject> monsters;
	[SerializeField]
	private float spawnTime;
	[SerializeField]
	private string damageSource;
	[SerializeField]
	private int health;
	public bool IsDead {
		get {
			return health <= 0;
		}
	}
	[SerializeField]
	private Slider healthSlider;
	private float timer = 0;
	// Use this for initialization
	void Start () {
		healthSlider.maxValue = health;	
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
	public void OnTriggerEnter2D(Collider2D other){
		if(damageSource==other.tag){
			TakeDamage (10);
		}
	}
	public void TakeDamage (int atk)
	{
		
		health -= atk;
		healthSlider.value = health;
		if (!IsDead) {
			healthSlider.gameObject.SetActive (true);
		} else {
			Destroy(gameObject);
		}

	}
}
