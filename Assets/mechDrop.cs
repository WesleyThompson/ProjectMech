using UnityEngine;
using System.Collections;


public class mechDrop : MonoBehaviour {
	private NavMeshAgent agent;
	public moveTo dropShip;
	public MonoBehaviour[] scripts;
	private GameObject player;
	public moveTo pScripts;

	void Start()
	{
		dropShip = GetComponentInParent<moveTo>();
		player = GameObject.FindGameObjectWithTag ("Player");
		agent = GetComponent<NavMeshAgent>();
		scripts = GetComponents<MonoBehaviour> ();
	}


	// Update is called once per frame
	void Update () {
		if (dropShip.getTimeAtDrop () > 4)
		{
			transform.parent = null;
			foreach (MonoBehaviour script in scripts) {
				if (!script.enabled)
				{
					script.enabled = true;
				}

			}
			agent.enabled = true;
			agent.SetDestination (player.transform.position);
		}
	
	}

}
