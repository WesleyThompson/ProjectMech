using UnityEngine;
using System.Collections;


public class drop : MonoBehaviour {
	private NavMeshAgent agent;
	public moveTo dropShip;
	public Rigidbody rb;
	public tankDeath death;
	public MonoBehaviour[] scripts;
	private GameObject player;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		agent = GetComponent<NavMeshAgent>();
		scripts = GetComponents<MonoBehaviour> ();
	}


	// Update is called once per frame
	void Update () {
		if (dropShip.getTimeAtDrop () > 4 && death.getHealth() > 0)
		{
			transform.parent = null;
			rb.useGravity = true;
			foreach (MonoBehaviour script in scripts) {
				if (!script.enabled)
				{
					script.enabled = true;
				}
			
			}
			agent.enabled = true;
			agent.SetDestination (player.transform.position);
		}
		else if(death.getHealth()<=0)
		{
			rb.useGravity = false;
			agent.enabled = false;
		}
	
	}
}
