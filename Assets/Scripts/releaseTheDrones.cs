using UnityEngine;
using System.Collections;

public class releaseTheDrones : MonoBehaviour {
	private NavMeshAgent agent;
	public moveTo dropship;
	public float altitude;
	private int timeToFly = 0;
	public droneDeath death;
	public MonoBehaviour[] scripts;
	private AudioSource deathExp;
	public moveTo pScripts;
	// Use this for initialization
	void Start()
	{
		dropship = GetComponentInParent<moveTo>();
		agent = GetComponent<NavMeshAgent>();
		scripts = GetComponents<MonoBehaviour> ();
		deathExp = GetComponent<AudioSource> ();
	}
	// Update is called once per frame
	void Update () {
		if (dropship.getTimeAtDrop () > 2) 
		{
			transform.parent = null;
			timeToFly = 1;
			foreach (MonoBehaviour script in scripts) {
				script.enabled = true;
			}
			agent.enabled = true;


		}
		if(death.getHealth() <= 0)
		{
			timeToFly = 0;

			agent.enabled = false;
		}
	}


}
