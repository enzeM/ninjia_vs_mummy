using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevel3 : MonoBehaviour {
	[SerializeField]
	private List<TombStone> bossList;
	private int killCount = 0;
	private int bossNum;
	// Use this for initialization
	void Start () {
		bossNum = bossList.Count;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (bossList.Count != 0) {
			for (int i = 0; i < bossList.Count; i++) {
				if (bossList [i].IsDead) {
					killCount++;
					bossList.Remove (bossList [i]);
				}
			}
		}
		if(killCount == bossNum){
			Player.Instance.winBoss = true;
		}
	}
}
