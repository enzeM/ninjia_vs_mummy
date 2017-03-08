using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {
	//deine start position of character
	[SerializeField]
	protected Vector3 startPos;
	//define character max health
	[SerializeField]
	public int health;
	//get a bool value of if the character is dead
	public abstract bool IsDead { get;}
	//define sword collider, it will be used for check melee attack
	[SerializeField]
	private EdgeCollider2D SwordCollider;
	//define which kind of damage that the character can take, used in OnTriggerEnter2D function.
	//so far, we have two kinds of damage source: PlayerDamage, EnemyDamage. These can defined in tag.
	[SerializeField]
	private List<string> damageSources;
	//define where the bullet or knife will spawn.
	[SerializeField]
	protected Transform shootPoint;
	//move speed...
	[SerializeField]
	public int moveSpeed;
	//bullet or knife prefab
	[SerializeField]
	private GameObject bullet;
	//face right or face left,
	protected bool faceRight;
	//the prop below can define the current state of character, for example, when the attach is shooting, then this.Shoot = true, (defined in animationBehaviours)
	public bool Attack{
		get;
		set;
	}
	public bool Shoot {
		get; 
		set;
	}
	public bool TakingDamage {
		get; 
		set;
	}

	public Animator MyAnimator {
		get;
		private set;
	}
	//when character take damage, then invoke this function
	public abstract IEnumerator TakeDamage ();
	//define what will happen after the character is dead
	public abstract void Death ();

	// Use this for initialization
	public virtual void Start () {
		faceRight = true;
		MyAnimator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//change dir, in player or enemy, you can invoke this function when you want them to change dir.
	public void ChangeDirection()
	{
		faceRight = !faceRight;
		transform.localScale = new Vector3 (transform.localScale.x * -1, 0.2f);
	}
	//spawn the bullet prefab, it will be invoked in shoot animation(by adding the animation event)
	protected void Fire ()
	{
		if (faceRight) {
			Instantiate (bullet, shootPoint.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
		} else {
			Instantiate (bullet, shootPoint.position, Quaternion.Euler (new Vector3 (0, 0, 180)));
		}
	}
	//set the active of sword collider, it will be invoked in attack animation(by adding the animation event)
	public void MeleeAttack(){
		SwordCollider.enabled = !SwordCollider.enabled;
	}

	//check all objects which have trigger
	public virtual void OnTriggerEnter2D(Collider2D other){
		if(damageSources.Contains(other.tag)){
			StartCoroutine (TakeDamage ());
		}
	}
}
