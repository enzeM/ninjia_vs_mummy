using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroductionController : MonoBehaviour {
	//retry button
	[SerializeField]
	private GameObject retryBtn;
	//text
	[SerializeField]
	private Text text;
	//states
	private enum State{
		objective, inputSet, skip
	}
	//myState
	private State myState;
	// Use this for initialization
	void Start ()
	{
		myState = State.objective;
		retryBtn.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		//print (myState);
		if(Player.Instance.IsDead){
			int currentSceneIndex = SceneManager.GetActiveScene ().buildIndex;
			SceneManager.LoadScene (currentSceneIndex);
		}
		if (myState == State.objective) {
			objective();
		} else if (myState == State.inputSet) {
			inputSet ();
		}

	}

	void objective(){
		text.text = "The Spring Land is controlled by six evil Mummy kings with their zombie legion..."+
		"\nA hero from far far away kingdom was assigned to fight against the evil allies and rescue people from the Spring Land."+
		"\nBefore starting the jurney, the hero must train ninjutsu from the ninja master";
		if (Input.GetKeyDown (KeyCode.Return)) {
			myState = State.inputSet;
		}
	}
	void inputSet(){
		text.text ="Followings the game control to finish the game tutorial: "+
		"\n\npress <left shift> to slide when you are moving"+
		"\npress <A> to the left, <B> to the right"+
		"\npress <Space> to jump"+
		"\nkeep pressing <W> to glide when you are falling or landing to the ground"+
		"\npress <J> to melee attack, <K> to throw kunai."+
		"\nyou can try melee attack and throw kunai when you are jumping"+
		"\n\nPress <Enter> to continue";
	}
}
