using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

	[SerializeField]
	private int enemyMaxHealth;
	[SerializeField]
	private int enemyCurHealth;
	[SerializeField]
	private Slider enemyHealthSlider;

	private Enemy enemy;

	void Start() 
	{
		enemy = GameObject.FindObjectOfType<Enemy> ();
		enemyCurHealth = enemyMaxHealth;
		enemyHealthSlider.maxValue = enemyMaxHealth;
		enemyHealthSlider.value = enemyCurHealth;
	}

	public void addDamage (int damage) 
	{
		//uncheck enemy health slider as default, and activate it in first damage
		enemyHealthSlider.gameObject.SetActive(true);
		enemyCurHealth -= damage;
		enemyHealthSlider.value = enemyCurHealth;

		if (enemyCurHealth <= 0)
		{
			EnemyDead();
		}
	}

	void EnemyDead () 
	{
		Destroy (this.gameObject);
	}
}
