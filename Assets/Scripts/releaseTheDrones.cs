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
	// Use this for initialization
	void Start()
	{
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
		if (timeToFly == 1 && death.getHealth() > 0) 
		{
			fly ();

		}
		else if(death.getHealth() <= 0)
		{
			timeToFly = 0;

			agent.enabled = false;
		}
	}


	void fly(){
		if (transform.position.y != altitude) {
			float step = 50 * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, (new Vector3 (transform.position.x, altitude, transform.position.y)), step);
		}
		else 
		{
		}
	
	
	}
}
