using UnityEngine;
using System.Collections;
using Common;

public class moveTo : MonoBehaviour
{
	public int health;
	public float rSpeed;
	private GameObject curDest;
	public GameObject waypoint;
	public GameObject exitPoint;
	public int atDropzone = 0;
	private float isFacing = 0;
	private float timeDead = 0;
	private float timeAtDrop = 0;

	private GameObject enemyMechDrop;
	private GameObject enemyMechAttach;
	private ObjectPooling mechPoolScript;

	private GameObject[] droneDrops = new GameObject[4];
	private GameObject[] droneAttaches = new GameObject[4];
	private ObjectPooling dronePoolScript;

	void Start()
	{
		curDest = waypoint;

		enemyMechDrop = transform.FindChild ("EnemyMech").gameObject;
		mechPoolScript = GameObject.Find ("EnemyMechPooler").GetComponent<ObjectPooling> ();
		enemyMechAttach = GameObject.Find("EnemyMech_Dropship_Spawn");


		droneDrops [0] = transform.FindChild ("drone1").gameObject;
		droneDrops [1] = transform.FindChild ("drone2").gameObject;
		droneDrops [2] = transform.FindChild ("drone3").gameObject;
		droneDrops [3] = transform.FindChild ("drone4").gameObject;

		dronePoolScript = GameObject.Find ("DronePooler").GetComponent<ObjectPooling> ();

		droneAttaches [0] = GameObject.Find ("drone_spawn1");
		droneAttaches [1] = GameObject.Find ("drone_spawn2");
		droneAttaches [2] = GameObject.Find ("drone_spawn3");
		droneAttaches [3] = GameObject.Find ("drone_spawn4");
	}


	void Update(){
		if (health > 0) 
		{
			if (Vector3.Dot (transform.forward, (curDest.transform.position - transform.position).normalized) > .99f && atDropzone == 0)
			{
				float step = 30 * Time.deltaTime;
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
				print ("DROP");
			}
			if (atDropzone == 1)
			{
				if (timeAtDrop < 5) {
					float step = 10 * Time.deltaTime;
					transform.position = Vector3.MoveTowards (transform.position, (curDest.transform.position + (new Vector3 (0f, -30f, 0f))), step);
					timeAtDrop += Time.deltaTime;
					//print ("dropping the payload");
				}
				else 
				{
					
					float step = 10 * Time.deltaTime;
					transform.position = Vector3.MoveTowards (transform.position, curDest.transform.position, step);
					if (transform.position == curDest.transform.position) 
					{
						atDropzone = 0;
						curDest = exitPoint;
					}
				}
			}

			if (timeAtDrop > 4) {
				print ("you know our motto, we deliva!");
				enemyMechDrop.transform.parent = null;
				enemyMechDrop.GetComponent<NavMeshAgent> ().enabled = true;

				foreach (GameObject drone in droneDrops) {
					drone.transform.parent = null;
					MonoBehaviour[] droneScripts = drone.GetComponents<MonoBehaviour> ();
					foreach (MonoBehaviour script in droneScripts) {
						script.enabled = true;
					}
					drone.GetComponent<NavMeshAgent> ().enabled = true;
					drone.GetComponent<BoxCollider> ().enabled = true;
				}
				timeToReloadEnemies = false;
			}

			if (transform.position == exitPoint.transform.position) {
				reset ();
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

	public void AttachEnemies() {
		print ("attach");
		enemyMechDrop = mechPoolScript.GetNextObject ();
		enemyMechDrop.transform.parent = gameObject.transform;
		enemyMechDrop.transform.position = enemyMechAttach.transform.position;
		enemyMechDrop.transform.rotation = enemyMechAttach.transform.rotation;

		for (int i = 0; i < droneAttaches.Length; i++) {
			GameObject drone = dronePoolScript.GetNextObject ();
			GameObject attachPoint = droneAttaches [i];
			drone.transform.parent = gameObject.transform;
			drone.transform.position = attachPoint.transform.position;
			drone.transform.rotation = attachPoint.transform.rotation;
			droneDrops [i] = drone;
		}
		timeAtDrop = 0;
	}


	private bool timeToReloadEnemies = false;
	public void reset()
	{
		if (!timeToReloadEnemies)
		{
			timeToReloadEnemies = true;
			AttachEnemies ();
		}
		else
		{
			timeToReloadEnemies = true;
			print ("you're calling me to many times brah! :(");
		}
			
		atDropzone = 0;
		curDest = waypoint;
		health = 150;
	}

	public float getTimeAtDrop(){
		
		return timeAtDrop;
	}
}