using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour 
{
	[SerializeField]
	private int curHealth;
	[SerializeField]
	private int maxHealth;
	[SerializeField]
	private Slider healthSlider;
	/*
	[SerializeField]
	private Sprite[] healthSprites;	
	[SerializeField]
	private Image healthImage;
	*/

	private Player player;
	private Animator playerAnimator;

	void Start() 
	{
		player = GameObject.FindObjectOfType<Player> ();
		playerAnimator = GetComponent<Animator> ();

		curHealth = maxHealth;
		healthSlider.maxValue = maxHealth;
		healthSlider.value = curHealth;
	}

	void Update() 
	{
		//healthImage.sprite = healthSprites [curHealth];
	}

	void FixedUpdate()
	{
		HandleHealth();
	}

	//handle player health
	private void HandleHealth() 
	{
		if(curHealth > maxHealth)
		{
			curHealth = maxHealth;
		}
		if (curHealth <= 0) 
		{
			if (!this.playerAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Die")) {
				Die ();
			}
		}
		healthSlider.value = curHealth;
	}

	//getters
	public int GetHealth() 
	{
		return this.curHealth;
	}

	public int GetMaxHealth() 
	{
		return this.maxHealth;
	}

	//current health minus one when each harm
	public void Harm()
	{
		this.curHealth --;
		player.setHarm (true);
	}

	//current health 
	public void AddHealth()
	{
		if (this.curHealth < this.maxHealth)
		{
			this.curHealth ++;
		}
	}

	void Die() 
	{
		player.setDie (true);
		playerAnimator.SetTrigger ("die");
		//delay time to destory player object
		StartCoroutine (DestroyPlayer());
	}

	//destory player after 2.0f
	private IEnumerator DestroyPlayer() 
	{
		yield return new WaitForSeconds (2.0f);
		Destroy (gameObject);
		yield return 0;
	}
}
