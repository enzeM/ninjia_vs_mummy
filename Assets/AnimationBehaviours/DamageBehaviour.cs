﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBehaviour : StateMachineBehaviour {
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//set TakingDamage value to true
		animator.GetComponent<Character> ().TakingDamage = true;
		//stop moving when this character is taking damage
		animator.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		//if the character is player, then play hurt sound
		if (animator.tag == "Player") 
			Player.Instance.audio.PlayOneShot (Player.Instance.hurtSound, 1);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
//	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
//	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//reset value
		animator.GetComponent<Character> ().TakingDamage = false;
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
