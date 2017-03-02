using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {
	[SerializeField]
	protected Vector3 startPos;

	[SerializeField]
	public int health;

	public abstract bool IsDead { get;}

	[SerializeField]
	private EdgeCollider2D SwordCollider;

	[SerializeField]
	private List<string> damageSources;

	[SerializeField]
	protected Transform shootPoint;

	[SerializeField]
	protected int moveSpeed;

	[SerializeField]
	private GameObject bullet;

	protected bool faceRight;

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

	public abstract IEnumerator TakeDamage ();
	public abstract void Death ();

	// Use this for initialization
	public virtual void Start () {
		faceRight = true;
		MyAnimator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeDirection()
	{
		faceRight = !faceRight;
		transform.localScale = new Vector3 (transform.localScale.x * -1, 0.2f);
	}
	protected void Fire ()
	{
		if (faceRight) {
			Instantiate (bullet, shootPoint.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
		} else {
			Instantiate (bullet, shootPoint.position, Quaternion.Euler (new Vector3 (0, 0, 180)));
		}
	}
	public void MeleeAttack(){
		SwordCollider.enabled = !SwordCollider.enabled;
	}

	public virtual void OnTriggerEnter2D(Collider2D other){
		if(damageSources.Contains(other.tag)){
			StartCoroutine (TakeDamage ());
		}
	}
}
