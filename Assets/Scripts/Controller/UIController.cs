using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/**
	this is UI controller
	if can set two modes for this game
	easy mode: camera will not move
	hard mode: camera will move up automatically
*/
public class UIController : MonoBehaviour {
	//static value: camera move speed
	static public float cameraSpeed = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//easy mode
	public void EasyBtn(){
		cameraSpeed = 0;
		int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1; 
		SceneManager.LoadScene(nextSceneIndex);
	}
	//hard mode
	public void HardBtn(){
		cameraSpeed = 0.3F;
		int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1; 
		SceneManager.LoadScene(nextSceneIndex);
	}
}
