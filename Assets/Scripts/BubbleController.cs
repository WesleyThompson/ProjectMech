﻿using UnityEngine;
using System.Collections;

public class BubbleController : MonoBehaviour {
	//
	public bool activated;
	public int bubbleType;
	public Material shieldMaterial;
	public Material speedMaterial;
	public Material damageMaterial;

	//time in seconds
	public float scaleSpeed;
	private float startScale;
	private float endScale;

	void Start () {
		//Start bubble at 0
		transform.localScale = new Vector3(0f,0f,0f);
		startScale = 0f;
		endScale = 10f;
		scaleSpeed = 1f;

		activated = false;
	}

	void Update () {
		if (Input.GetKeyUp (KeyCode.Alpha1)) {
			ActivateBubble (0);
		} else if (Input.GetKeyUp (KeyCode.Alpha2)) {
			ActivateBubble (1);
		} else if (Input.GetKeyUp (KeyCode.Alpha3)) {
			ActivateBubble (2);
		}

		if (activated) {
			ExpandBubble ();
		} else {
			ShrinkBubble ();
		}
	}

	void ActivateBubble(int type){
		activated = true;
		bubbleType = type;
		//Reset bubble to 0
		transform.localScale.Set (0, 0, 0);

		//Setting the material aka the color
		switch (type) {
		case 0:
			GetComponent<Renderer> ().material = shieldMaterial;
			break;
		case 1:
			GetComponent<Renderer> ().material = speedMaterial;
			break;
		case 2:
			GetComponent<Renderer> ().material = damageMaterial;
			break;
		}
	}

	void ExpandBubble(){
		Debug.Log ("Expand");
		float newScale = Mathf.Lerp (startScale, endScale, Time.deltaTime / scaleSpeed);
		transform.localScale = new Vector3 (newScale, newScale, newScale);
	}

	void ShrinkBubble(){
		Debug.Log ("Shrink");
		//Same as expand but in reverse
		float newScale = Mathf.Lerp (endScale, startScale, Time.deltaTime / scaleSpeed);
		transform.localScale = new Vector3 (newScale, newScale, newScale);
	}
}
