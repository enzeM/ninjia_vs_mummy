﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/**
	this class is used to control pause menu
	when click esc, pause menu display
	when player enter door, then boosMenu display
	when player dead, gameover menu display
	when player defeat all bosses, then win menu display
*/
public class PauseMenuManager : MonoBehaviour {
	//bool value : is paused
	private bool isPause;
	//bind menus
	[SerializeField]
	private GameObject pauseMenuUI;
	[SerializeField]
	private GameObject gameoverMenuUI;
	[SerializeField]
	private GameObject bossMenuUI;
	[SerializeField]
	private GameObject winMenuUI;
	// Use this for initialization
	void Start () {
		//init valuse
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
		if (isPause || Player.Instance.fightBoss) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}

		if(Player.Instance.IsDead){
			StartCoroutine (DeathMenu());
		}
		pauseMenuUI.SetActive (isPause);
		bossMenuUI.SetActive (Player.Instance.fightBoss);
		if(Player.Instance.winBoss){
			winMenuUI.SetActive (Player.Instance.winBoss);
			Time.timeScale = 0;
		}
	}
	//delay to show gameove menu after player dead
	public IEnumerator DeathMenu()
	{
		yield return new WaitForSeconds(1);
		isPause = true;
		gameoverMenuUI.SetActive (Player.Instance.IsDead);
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
	//load next level
	public void LoadNextLevel() {
		int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1; 
		SceneManager.LoadScene(nextSceneIndex);
	}
}
