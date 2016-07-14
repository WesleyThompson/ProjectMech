using UnityEngine;
using System.Collections;
public class moveTo : MonoBehaviour
{
	public int health;
	public Vector3 pointB;
	public float rSpeed;
	public GameObject waypoint;
	public int atDropzone = 0;
	private float isFacing = 0;
	private float timeDead = 0;
	private float timeAtDrop = 0;


	void Update(){
		if (health > 0) 
		{
			if (Vector3.Dot (transform.forward, (waypoint.transform.position - transform.position).normalized) > .99f && atDropzone == 0)
			{
				float step = 30 * Time.deltaTime;
				transform.position = Vector3.MoveTowards (transform.position, waypoint.transform.position, step);
				if (transform.position == waypoint.transform.position) 
				{
					atDropzone = 1;
					//Debug.Log ("im here");
				}
			}
			else if(atDropzone ==0) {

				Vector3 targetDir = waypoint.transform.position - transform.position;
				float step = rSpeed * Time.deltaTime;
				Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
				Debug.DrawRay (transform.position, newDir, Color.red);
				transform.rotation = Quaternion.LookRotation (newDir);

			}
			if (atDropzone == 1)
			{
				if (timeAtDrop < 5) {
					float step = 10 * Time.deltaTime;
					transform.position = Vector3.MoveTowards (transform.position, (waypoint.transform.position + (new Vector3 (0f, -30f, 0f))), step);
					timeAtDrop += Time.deltaTime;
				}
				else 
				{
					
					float step = 10 * Time.deltaTime;
					transform.position = Vector3.MoveTowards (transform.position, waypoint.transform.position, step);
					if (transform.position == waypoint.transform.position) 
					{
						atDropzone = 0;
						waypoint = GameObject.FindGameObjectWithTag ("exit");
					}
				}
			}
			health--;
		}
		else 
		{
			if (timeDead > 4) {
				transform.position = new Vector3 (0f, -50f, 0f);
			}
			else 
			{
				transform.position -= new Vector3 (0f, .3f, 0f);
				timeDead += Time.deltaTime;
			}
		}
			
	}

	public float getTimeAtDrop(){
		
		return timeAtDrop;
	}
}