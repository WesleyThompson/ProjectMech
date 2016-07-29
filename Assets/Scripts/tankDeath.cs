using UnityEngine;
using System.Collections;

public class tankDeath : MonoBehaviour {
	public bool inPlay = false;
	private int maxHealth = 200;
	public int health;
	private float timeDead = 0;
	private Component[] child;

	void Start()
	{
		child = GetComponentsInChildren<ParticleSystem> ();
		health = maxHealth;
	}

	// Update is called once per frame
	void Update () {
		if (health <= 0)
		{
			//explode or something
			foreach (ParticleSystem cParts in child) 
			{
				var em = cParts.emission;
				if (em.enabled == true)
				{
					em.enabled = false;
					cParts.Stop ();
				} 
				else 
				{
					em.enabled = true;
					cParts.Play ();
				}
				//measures time spent dead
				if (timeDead > 2) {
					transform.position = new Vector3 (0f, 100f, 0f);
					reset ();
				}
				else 
				{
					timeDead += Time.deltaTime;
				}


			}
		}
		else if (health <= 100) 
		{
			//have some fire turn on, eventually, maybe if we have time

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
