﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevel2 : MonoBehaviour {
	[SerializeField]
	private List<AmazingEnemy> bossList;
	[SerializeField]
	private GameObject winMenuUI;
	private int killCount = 0;
	private int bossNum;
	// Use this for initialization
	void Start () {
		bossNum = bossList.Count;
		winMenuUI.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < bossList.Count; i++) {
			if(bossList[i].IsDead){
				killCount++;
				bossList.Remove (bossList [i]);
			}
		}
		if(killCount == bossNum){
			winMenuUI.SetActive (true);
			Time.timeScale = 0;
		}
	}
}
