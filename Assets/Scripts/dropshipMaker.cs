using UnityEngine;
using System.Collections;
using Common;
public class dropshipMaker : MonoBehaviour {

	public moveTo moveScript;
	public Vector3 wait;
	public ObjectPooling dropPool;
	public int dropCount = 0;
	public float timeSinceSpawn = 0;
	private GameObject curDrop;
	private GameObject[] dropzone;
	public GameObject target;
	public bool waiting = false;
	public float timeSinceCreation = 0;
	public bool startCounting =false;

	// Use this for initialization
	void Start () {
		dropPool = GameObject.Find("dropshipPooler").GetComponent<ObjectPooling>();
		dropzone = GameObject.FindGameObjectsWithTag ("dropzone");
		target = calcDropzone ();
		waiting = false;
	}
	
	// Update is called once per frame
	void Update () {
		//make a dropship
		if (dropCount == 0)
		{
				
			timeSinceSpawn = 0;
			curDrop = dropPool.GetNextObject ();
			curDrop.transform.position = transform.position;
			curDrop.transform.LookAt (target.transform);
			moveScript = curDrop.GetComponent<moveTo> ();
			moveScript.waypoint = target;
			moveScript.exitPoint = this.gameObject;
			waiting = false;
			dropCount = 1;
			startCounting = true;
		}
		if (curDrop != null) 
		{
			if (curDrop.transform.position == transform.position && timeSinceCreation > 10) 
			{
				startCounting = false;
				timeSinceCreation = 0;
				moveScript = null;
				curDrop = null;
				Destroy (curDrop);
				waiting = true;
			}
		}

		if (waiting)
		{
			timeSinceSpawn += Time.deltaTime;
			if (timeSinceSpawn >= 120)
			{
				dropCount = 0;
				waiting = false;
			}
		}

		if (startCounting) 
		{
			timeSinceCreation += Time.deltaTime;
		}

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
}
