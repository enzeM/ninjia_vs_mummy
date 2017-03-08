using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {

	private bool isPause;

	[SerializeField]
	private GameObject pauseMenuUI;
	[SerializeField]
	private GameObject gameoverMenuUI;
	[SerializeField]
	private GameObject bossMenuUI;
	// Use this for initialization
	void Start () {
		isPause = false;
		gameoverMenuUI.SetActive (false);
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
		if(Player.Instance.fightBoss){
			Time.timeScale = 0;

		}
		if(Player.Instance.IsDead){
			StartCoroutine (DeathMenu());
		}
		pauseMenuUI.SetActive (isPause);
		bossMenuUI.SetActive (Player.Instance.fightBoss);
	}
	public IEnumerator DeathMenu()
	{
		yield return new WaitForSeconds(1);
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

	public void LoadNextLevel() {
		int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1; 
		SceneManager.LoadScene(nextSceneIndex);
	}
}
