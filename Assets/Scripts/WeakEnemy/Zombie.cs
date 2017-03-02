using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : WeakEnemy 
{
	//singleton design for zombie object
	public static Zombie instance;

	private static Zombie Instance {
		get 
		{
			if (instance == null) 
			{
				instance = GameObject.FindObjectOfType<Zombie> ();
			}
			return instance;
		}
	}

	//zombie is an melee attack enemy, it will walk toward player
	public override void EnemyAttack ()
	{
		//move toward the facing area
		print("attack");
		if(!faceRight) 
		{
			EnemyBody.velocity = new Vector2 (-1 * enemyMoveSpeed, EnemyBody.velocity.y);
		}
		else
		{
			EnemyBody.velocity = new Vector2 ( 1 * enemyMoveSpeed, EnemyBody.velocity.y);
		}
		EnemyAnimator.SetBool("attack", true);
	}
}
