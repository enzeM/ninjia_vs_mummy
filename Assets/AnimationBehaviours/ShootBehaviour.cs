using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehaviour : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		//set shoot value to true and set "speed" to 0 so than make the character stop moving when it is shooting
		animator.GetComponent<Character> ().Shoot = true;
		animator.SetFloat ("speed", 0);
		//if the character is player and it is on the ground, then play shoot sound
		if (animator.tag == "Player") {
			if (Player.Instance.OnGround) {
				Player.Instance.audio.PlayOneShot (Player.Instance.shootSound, 1);
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
		//reset values
		animator.GetComponent<Character>().Shoot = false;
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
