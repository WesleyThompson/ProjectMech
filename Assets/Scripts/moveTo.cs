using UnityEngine;
using System.Collections;
public class moveTo : MonoBehaviour
{
	//public GameObject spawnPoint;
	//public Vector3 waitPoint;
	private int maxHealth = 1000;
	public int health;
	public float rSpeed;
	private GameObject curDest;
	public GameObject waypoint;
	public GameObject exitPoint;
	public int atDropzone = 0;
	private float isFacing = 0;
	private float timeDead = 0;
	private float timeAtDrop = 0;
	private Component[] child;
	private GameObject[] dropzone;
	private GameObject[] exit;
	public bool readyToGo = false;
	//public float respawnTime = 10;
	public bool startRespawn = false;
	//private Quaternion startRotation;

	void Start()
	{
		//startRotation = transform.rotation;
		setUpDropship ();
	}


	void Update(){
		if (health > 0) 
		{
			if (Vector3.Dot (transform.forward, (curDest.transform.position - transform.position).normalized) > .999f && atDropzone == 0)
			{
				Debug.Log ("moving forward");
				float step = 75 * Time.deltaTime;
				transform.position = Vector3.MoveTowards (transform.position, curDest.transform.position, step);
				if (transform.position == curDest.transform.position) 
				{
					if (readyToGo) {
						//transform.position = new Vector3 (0f, -150f, 0f);
						startRespawn = true;
					}
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
						curDest = exitPoint;
						readyToGo = true;
							
					}
				}
			}

		}
		else 
		{
			if (timeDead > 4) {
				foreach(ParticleSystem cParts in child){
					var em = cParts.emission;
					if (em.enabled == true) {
						em.enabled = false;
						cParts.Stop ();
					}
					else {
						em.enabled = true;
						cParts.Play ();
					}

					//part of the animation where the drone disappears
					if (timeDead > 1) {
						//transform.position = waitPoint;
						reset ();
					}
					else 
					{
						timeDead += Time.deltaTime;
					}


				}


				//transform.position = waitPoint;
				reset ();
			}
			else 
			{
				transform.position -= new Vector3 (0f, .3f, 0f);
				timeDead += Time.deltaTime;
			}
		}

		/*if (startRespawn)
		{
			transform.position = waitPoint;
			respawnTime -= Time.deltaTime;
			transform.rotation = startRotation;
			if (respawnTime <= 0)
			{
				setUpDropship ();
				readyToSpawn ();
			}
		}
		*/

	}

	public void reset()
	{
		atDropzone = 0;
		curDest = waypoint;
		health = maxHealth;
	}

	public float getTimeAtDrop()
	{

		return timeAtDrop;
	}

	private GameObject calcDropzone()
	{
		float distance = 1000000;
		float curDistance = 0;
		GameObject curTarget = dropzone[0];

		foreach (GameObject drop in dropzone)
		{
			curDistance = Vector3.Distance (transform.position, drop.transform.position);
			if (curDistance < distance)
			{
				curTarget = drop;
				distance = curDistance;
			}
		}

		return curTarget;
	}
		
	private GameObject calcExit()
	{
		float distance = 1000000;
		float curDistance = 0;
		GameObject curTarget = exit[0];

		foreach (GameObject leave in exit)
		{
			//Debug.Log ("fjkejwwqofjekwqo");
			curDistance = Vector3.Distance (transform.position, leave.transform.position);
			if (curDistance < distance)
			{
				curTarget = leave;
				distance = curDistance;
			}
		}

		return curTarget;
	}

	public void setUpDropship()
	{
		//dropzone = GameObject.FindGameObjectsWithTag ("dropzone");
		//waypoint = calcDropzone ();
		//exit = GameObject.FindGameObjectsWithTag ("exit");
		//exitPoint = calcExit ();
		child = GetComponentsInChildren<ParticleSystem> ();
		health = maxHealth;
		curDest = waypoint;
		//transform.position = spawnPoint.transform.position;
		//respawnTime = 10;
		atDropzone = 0;
		timeAtDrop = 0;
		readyToGo = false;
		startRespawn = false;
	}

	public void readyToSpawn()
	{
		startRespawn = false;
	}
}