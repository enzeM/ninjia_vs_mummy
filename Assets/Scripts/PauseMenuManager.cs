using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {

	private bool isPause;

	[SerializeField]
	private GameObject pauseMenuUI;

	// Use this for initialization
	void Start () {
		isPause = false;
	}

	// Update is called once per frame
	void Update () {
		HandleInput ();		
		PauseManager ();
	}

	//handle key input 
	void HandleInput() {
		//active and deactive pause menu 
		if (Input.GetKeyDown (KeyCode.Escape)) {
			isPause = !isPause;
		}
	}

	//listen isPause param stop game if is pause
	void PauseManager() {
		if (isPause) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
		pauseMenuUI.SetActive (isPause);
	}

	//resume game
	public void Resume() {
		isPause = false;
	}

	//reload current scene
	public void Restart() { 
		int currentSceneIndex = SceneManager.GetActiveScene ().buildIndex;
		SceneManager.LoadScene (currentSceneIndex);
	}

	//load to main menu
	public void MainMenu() {
		SceneManager.LoadScene (0); 
	}

	//exit game
	public void Quit() {
		Application.Quit ();
	}
}
