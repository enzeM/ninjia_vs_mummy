using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
	this class used to control boss level
	check if all bosses are dead in each frame
	if all boss dead, then player win
*/
public class BossLevel3 : MonoBehaviour {
	//store all bosses in a list
	[SerializeField]
	private List<TombStone> bossList;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
	{	
		/**check if all bosses are dead 
			if dead, remove it from bosslist
		*/
		for (int i = 0; i < bossList.Count; i++) {
			if (bossList [i].IsDead) {
				bossList.Remove (bossList [i]);
			}
		}
		//if bossList is empty, then player win
		if(bossList.Count == 0){
			Player.Instance.winBoss = true;
		}
	}
}
