using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
	interface: enmey have different state
	idle,melee,ranged and patrol
*/
public interface IEnemyState
{
	void Execute();
	void Enter (Enemy enemy);
	void Exit();
	void OnTriggerEnter (Collider2D other);
}
