using UnityEngine;
using System.Collections;

public class FrontLeftEngine : MonoBehaviour 
{

	public GameObject dropship;
	public Rigidbody rb;
	private float curSpeed;

	// Update is called once per frame
	void Update ()
	{
		rightTurn ();

		//Debug.Log (rb.angularVelocity.magnitude);
		//if the dropship is rotating
		/*if (rb.angularVelocity.magnitude != 0)
			{
			Debug.Log ("im turning");
			//if we rotate left

		}
		//if we aren't turning then we may be moving
		else if (curSpeed > 0) {
			forward ();
		}
		//if we aren't moving or tunring, the we are idle
		else
		{
			idle ();
		}
*/
	}

	void rightTurn()
	{

		transform.Rotate (Vector3.left);
	}

	void leftTurn()
	{
	

	}

	void forward()
	{


	}

	void idle()
	{
	
	}
}
