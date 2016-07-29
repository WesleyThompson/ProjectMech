using UnityEngine;
using System.Collections;

public class droneDeath : MonoBehaviour {

	private int maxHealth = 100;
	public int health;
	public GameObject explosion;
	private Component[] child;
	private float timeDead = 0;

	void Start()
	{
		child = GetComponentsInChildren<ParticleSystem> ();
		health = maxHealth;
	
	}

	// Update is called once per frame
	void Update () {
		
		//var em = parts.emission;
		if (health <= 0) {
			
			//explode or something
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

				//part of the animation where the drone disappears
				if (timeDead > 1) {
					transform.position = new Vector3 (0f, 100f, 0f);
					reset ();
				}
				else 
				{
					timeDead += Time.deltaTime;
				}
			

		}

	
	}
	}
	private void reset()
	{
		health = maxHealth;

	}

	public int getHealth()
	{
		return health;
	}
}
