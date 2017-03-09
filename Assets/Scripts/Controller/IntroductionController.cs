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
	private enum State {
		objective, page1, page2, skip
	}
	//myState
	private State myState;
	// Use this for initialization
	void Start () {
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
		} else if (myState == State.page1) {
			pageOne ();
		} else if (myState == State.page2) {
			pageTwo ();
		}
	}

	void objective(){
		text.text = "The Spring Land is controlled by six evil Mummy kings with their zombie legion..."+
		"\nA hero from far far away kingdom was assigned to fight against the evil allies and rescue people from the Spring Land."+
		"\nBefore starting the jurney, the hero must train ninjutsu from the ninja master";
		if (Input.GetKeyDown (KeyCode.Return)) {
			myState = State.page1;
		}
	}

	void pageOne() {
		text.text ="Please follow the instruction to finish your training: "+
			"\n\nLesson 1: Basic Movement:"+
			"\nPress <A> \"MOVE\" to the left, <B> to the right"+
			"\nPress <Space> to \"JUMP\""+
			"\nPress <J> to \"MELEE ATTACK\", <K> to \"THROW KUNAI\".";
		if (Input.GetKeyDown (KeyCode.Return)) {
			myState = State.page2;
		}
	}

	void pageTwo() {
		text.text ="Please follow the instruction to finish your training: "+
			"\n\nLesson 2: Advance Movement:"+
			"\n\nYou can ATTACK WHEN YOU ARE JUMPING: <SPACE> + <J> or <SPACE> + <K>"+
			"\nKeep pressing <W> to \"GLIDE\" when you are falling or landing to the ground"+
			"\nPress <LEFT SHIFT> to \"SLIDE\" when you are moving"+
			"\nSLIDING can help you AVOID HARMFUL OBSTACLE!";
		if (Input.GetKeyDown (KeyCode.Return)) {
			SceneManager.LoadScene("DifficultySetting");
		}
	}
}
