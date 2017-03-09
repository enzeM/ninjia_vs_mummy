using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/**
	this class is used to control levels

*/
public class LevelManager : MonoBehaviour {

	//exit game
	public void Quit() {
		Application.Quit ();
	}

	//load to next level
	public void LoadNextLevel() {
		//get to next level based on build index
		int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1; 
		SceneManager.LoadScene(nextSceneIndex);
	}
}
