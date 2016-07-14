using UnityEngine;
using System.Collections;


public class drop : MonoBehaviour {
	public moveTo dropShip;
	public Rigidbody rb;
	// Update is called once per frame
	void Update () {
		if (dropShip.getTimeAtDrop () > 4)
		{
			transform.parent = null;
			rb.useGravity = true;
		}

	
	}
}
