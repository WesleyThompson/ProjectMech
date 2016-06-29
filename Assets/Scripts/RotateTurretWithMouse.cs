﻿using UnityEngine;
using System.Collections;

public class RotateTurretWithMouse : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;

	private float speed = 50.0F;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}

	// LateUpdate is called once per frame
	void LateUpdate () {
		// move turret with tank body
		//transform.position = player.transform.position + offset;
		//rotate turret
		transform.Rotate (new Vector3(0, 0, Input.GetAxis("Mouse X")) * Time.deltaTime * speed);
		transform.position = player.transform.position + offset;
	}
}