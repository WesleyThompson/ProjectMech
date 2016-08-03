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

	private GameObject[] enemyMechDrops = new GameObject[4];
	private GameObject[] enemyMechAttaches = new GameObject[4];
	private ObjectPooling mechPoolScript;

	private GameObject[] droneDrops = new GameObject[4];
	private GameObject[] droneAttaches = new GameObject[4];
	private ObjectPooling dronePoolScript;

	void Start()
	{
		curDest = waypoint;

		enemyMechDrops [0] = transform.FindChild ("EnemyMech1").gameObject;
		enemyMechDrops [1] = transform.FindChild ("EnemyMech2").gameObject;
		enemyMechDrops [2] = transform.FindChild ("EnemyMech3").gameObject;
		enemyMechDrops [3] = transform.FindChild ("EnemyMech4").gameObject;

		enemyMechAttaches [0] = transform.FindChild ("EnemyMech_Dropship_Spawn1").gameObject;
		enemyMechAttaches [1] = transform.FindChild ("EnemyMech_Dropship_Spawn2").gameObject;
		enemyMechAttaches [2] = transform.FindChild ("EnemyMech_Dropship_Spawn3").gameObject;
		enemyMechAttaches [3] = transform.FindChild ("EnemyMech_Dropship_Spawn4").gameObject;

		mechPoolScript = GameObject.Find ("EnemyMechPooler").GetComponent<ObjectPooling> ();

		droneDrops [0] = transform.FindChild ("drone1").gameObject;
		droneDrops [1] = transform.FindChild ("drone2").gameObject;
		droneDrops [2] = transform.FindChild ("drone3").gameObject;
		droneDrops [3] = transform.FindChild ("drone4").gameObject;

		droneAttaches [0] = transform.FindChild ("drone_spawn1").gameObject;
		droneAttaches [1] = transform.FindChild ("drone_spawn2").gameObject;
		droneAttaches [2] = transform.FindChild ("drone_spawn3").gameObject;
		droneAttaches [3] = transform.FindChild ("drone_spawn4").gameObject;

		dronePoolScript = GameObject.Find ("DronePooler").GetComponent<ObjectPooling> ();
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
				DropEnemies ();
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

	private void DropEnemies() {
		print ("you know our motto, we deliva!");
		DropEnemyOfType (enemyMechDrops);
		DropEnemyOfType (droneDrops);
	}

	private void DropEnemyOfType(GameObject[] enemies) {
		foreach (GameObject enemy in enemies) {
			enemy.transform.parent = null;
			MonoBehaviour[] scripts = enemy.GetComponents<MonoBehaviour> ();
			foreach (MonoBehaviour script in scripts) {
				script.enabled = true;
			}
			enemy.GetComponent<NavMeshAgent> ().enabled = true;
			Collider col = enemy.GetComponent<BoxCollider> ();
			if (col != null) {
				col.enabled = true;
			}
		}
	}

	private void AttachEnemies() {
		print ("attach");
		attachEnemiesOfType (mechPoolScript, enemyMechAttaches, enemyMechDrops);
		attachEnemiesOfType (dronePoolScript, droneAttaches, droneDrops);
		timeAtDrop = 0;
	}

	private void attachEnemiesOfType(ObjectPooling poolScript, GameObject[] attaches, GameObject[] enemies) {
		for (int i = 0; i < attaches.Length; i++) {
			GameObject enemy = poolScript.GetNextObject ();
			GameObject attachPoint = attaches [i];
			enemy.transform.parent = gameObject.transform;
			enemy.transform.position = attachPoint.transform.position;
			enemy.transform.rotation = attachPoint.transform.rotation;
			enemies [i] = enemy;
		}
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