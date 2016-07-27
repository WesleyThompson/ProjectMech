using UnityEngine;
using System.Collections;

public class boogaboogabooga : MonoBehaviour {

	// Use this for initialization

	private Component[] child;
	// Update is called once per frame
	void Update () {
		child = GetComponentsInChildren<ParticleSystem> ();
		//steve = GetComponentInChildren<ParticleSystem> ();
		//steve.Play();
		if (Input.GetKeyDown ("space"))
		{
			//steve = GetComponentInChildren<ParticleSystem> ();
			foreach(ParticleSystem cParts in child){
				var em = cParts.emission;
				if (em.enabled == true) {
					em.enabled = false;
					cParts.Stop ();
				}
				else {
					em.enabled = true;
					cParts.Play ();
				}


			}
		
		}
	}
}
