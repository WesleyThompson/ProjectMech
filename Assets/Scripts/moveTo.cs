using UnityEngine;
using System.Collections;
public class moveTo : MonoBehaviour
{
	private int maxHealth = 500;
	public int health;
	public float rSpeed;
	private GameObject curDest;
	public GameObject waypoint;
	public GameObject exitPoint;
	public int atDropzone = 0;
	private float isFacing = 0;
	private float timeDead = 0;
	private float timeAtDrop = 0;

	void Start()
	{
		health = maxHealth;
		curDest = waypoint;
	}


	void Update(){
		if (health > 0) 
		{
			if (Vector3.Dot (transform.forward, (curDest.transform.position - transform.position).normalized) > .99f && atDropzone == 0)
			{
				float step = 100 * Time.deltaTime;
				transform.position = Vector3.MoveTowards (transform.position, curDest.transform.position, step);
				if (transform.position == curDest.transform.position) 
				{
					atDropzone = 1;
					//Debug.Log ("im here");
				}
			}
			else if(atDropzone ==0) {

				Vector3 targetDir = curDest.transform.position - transform.position;
				float step = rSpeed * Time.deltaTime;
				Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
				Debug.DrawRay (transform.position, newDir, Color.red);
				transform.rotation = Quaternion.LookRotation (newDir);

			}
			if (atDropzone == 1)
			{
				if (timeAtDrop < 5) {
					float step = 10 * Time.deltaTime;
					transform.position = Vector3.MoveTowards (transform.position, (curDest.transform.position + (new Vector3 (0f, -30f, 0f))), step);
					timeAtDrop += Time.deltaTime;
				}
				else 
				{
					
					float step = 10 * Time.deltaTime;
					transform.position = Vector3.MoveTowards (transform.position, curDest.transform.position, step);
					if (transform.position == curDest.transform.position) 
					{
						atDropzone = 0;
						curDest = exitPoint
							;
					}
				}
			}

		}
		else 
		{
			if (timeDead > 4) {
				transform.position = new Vector3 (0f, -50f, 0f);
				reset ();
			}
			else 
			{
				transform.position -= new Vector3 (0f, .3f, 0f);
				timeDead += Time.deltaTime;
			}
		}
			
	}

	public void reset()
	{
		atDropzone = 0;
		curDest = waypoint;
		health = maxHealth;
	}

	public float getTimeAtDrop(){
		
		return timeAtDrop;
	}
}