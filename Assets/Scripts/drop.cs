using UnityEngine;
using System.Collections;


public class drop : MonoBehaviour {
	public moveTo dropShip;
	public Rigidbody rb;
	public tankDeath death;
	// Update is called once per frame
	void Update () {
		if (dropShip.getTimeAtDrop () > 4 && death.getHealth() > 0)
		{
			transform.parent = null;
			rb.useGravity = true;
		}
		else if(death.getHealth()<=0)
		{
			rb.useGravity = false;
		}
	
	}
}
