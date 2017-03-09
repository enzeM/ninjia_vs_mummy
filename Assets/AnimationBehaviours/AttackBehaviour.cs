using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		//set Attack prop with true
		animator.GetComponent<Character> ().Attack = true;
		//when speed < 0.1, character will enter idle state
		animator.SetFloat ("speed", 0);
		//if the gameobject is Player, and player is on the ground, then play attack sound and stop moving
		if (animator.tag == "Player") {
			if (Player.Instance.OnGround) {
				Player.Instance.audio.PlayOneShot (Player.Instance.meleeSound, 1);
				Player.Instance.MyRigibody.velocity = Vector2.zero;
			}
		}
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//reset value
		animator.GetComponent<Character>().Attack = false;
		//set the active of melee attack collider to false;
		animator.GetComponent<Character> ().MeleeAttack ();
		//reset triggers
		animator.ResetTrigger ("attack");
		animator.ResetTrigger ("throw");
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
