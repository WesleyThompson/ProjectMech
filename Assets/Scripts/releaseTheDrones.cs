﻿using UnityEngine;
using System.Collections;

public class releaseTheDrones : MonoBehaviour {

	public moveTo dropship;
	public float altitude;
	private int timeToFly = 0;
	// Use this for initialization

	// Update is called once per frame
	void Update () {
		if (dropship.getTimeAtDrop () > 2) 
		{
			transform.parent = null;
			timeToFly = 1;
		}
		if (timeToFly == 1) {
			fly ();
		}
	}


	void fly(){
		if (transform.position.y != altitude)
		{
			float step = 50 * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, (new Vector3 (transform.position.x, altitude, transform.position.y)), step);
		}
	
	}
}
