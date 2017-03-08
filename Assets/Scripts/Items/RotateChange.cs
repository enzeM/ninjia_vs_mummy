using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateChange : MonoBehaviour {
	public float smooth = 3.0F;
	private float angle = 90;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		angle += Time.deltaTime*90*smooth;
		if(angle >= 360)
			angle = 0;
		Quaternion target = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
	}
}
