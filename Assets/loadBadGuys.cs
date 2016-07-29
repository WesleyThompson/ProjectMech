using UnityEngine;
using System.Collections;
using Common;

public class loadBadGuys : MonoBehaviour {

	public moveTo moveScript;
	public Vector3 wait;

	public Vector3[] dronePos = new Vector3[4];
	public Vector3[] tankPos = new Vector3[2];
	public Vector3[] mechPos = new Vector3[1];

	public ObjectPooling dronePool;
	public ObjectPooling tankPool;
	public ObjectPooling mechPool;

	//public BestPooling Deadpool;

	public bool loaded = false;


	void Start()
	{
		
		dronePool = GameObject.Find("dronePooler").GetComponent<ObjectPooling>();
		tankPool = GameObject.Find ("tankPooler").GetComponent<ObjectPooling> ();
		mechPool = GameObject.Find ("mechPooler").GetComponent<ObjectPooling> ();
	
		//wait = moveScript.waitPoint;
		//setDronePos ();
		//setTankPos ();
	//	setMechPos ();
	}

	// Update is called once per frame
	void Update ()
	{

		if (!loaded) {
			//front drone
			GameObject curDrone = dronePool.GetNextObject ();
			curDrone.SetActive (false);
			curDrone.transform.parent = transform;
			curDrone.transform.localPosition = new Vector3 (0.0005270604f,0.01357181f,0.009618854f);
			curDrone.SetActive (true);
			//mid right drone
			curDrone = dronePool.GetNextObject ();
			curDrone.SetActive (false);
			curDrone.transform.parent = transform;
			curDrone.transform.localPosition = new Vector3 (0.003557658f,0.01344004f,0f);
			curDrone.SetActive (true);
			//mid left drone
			curDrone = dronePool.GetNextObject ();
			curDrone.SetActive (false);
			curDrone.transform.parent = transform;
			curDrone.transform.localPosition = new Vector3 (-0.003952953f,0.01344004f,0f);
			curDrone.SetActive (true);
			//rear drone
			curDrone = dronePool.GetNextObject ();
			curDrone.SetActive (false);
			curDrone.transform.parent = transform;
			curDrone.transform.localPosition = new Vector3 (-0.0002635302f,0.01344004f,-0.009355323f);
			curDrone.SetActive (true);
			//rear tank
			GameObject curTank = tankPool.GetNextObject ();
			curTank.SetActive (false);
			curTank.transform.parent = transform;
			curTank.transform.localPosition = new Vector3 (0f,0.005797658f,-0.02477184f);
			curTank.SetActive (true);
			//front tank
			curTank = tankPool.GetNextObject ();
			curTank.SetActive (false);
			curTank.transform.parent = transform;
			curTank.transform.localPosition = new Vector3 (0f,-0.0007905988f,-0.006324726f);
			curTank.SetActive (true);
			//mech
			GameObject curMech = mechPool.GetNextObject ();
			curMech.SetActive (false);
			curMech.transform.parent = transform;
			curMech.transform.localPosition = new Vector3 (0f,-0.01172709f,0.0113318f);
			curMech.SetActive (true);

			loaded = true;
		}
		/*if (moveScript.startRespawn) 
		/*{
			//Debug.Log ("working");
			foreach (Vector3 location in dronePos) 
			{
				GameObject curDrone = dronePool.GetNextObject ();
				curDrone.SetActive (false);
				curDrone.transform.position = location;
				curDrone.transform.parent = transform;
				curDrone.SetActive (true);
			}
			foreach (Vector3 location in tankPos) 
			{
				GameObject curTank = tankPool.GetNextObject ();
				curTank.SetActive (false);
				placeDrones (curTank , location);
			}
			foreach (Vector3 location in mechPos) 
			{
				GameObject curMech = mechPool.GetNextObject ();
				curMech.SetActive (false);
				placeDrones (curMech , location);
			}
			loaded = true;
		}*/
		if (moveScript.readyToGo && transform.position == wait)
		{
			loaded = false;
			moveScript.readyToGo = false;
		}

	}
	/*
	void placeDrones(GameObject drone, Vector3 location)
	{
		drone.transform.position = location;
		drone.transform.parent = transform;
		drone.SetActive (true);
	}

	void placeTanks(GameObject tank, Vector3 location)
	{
		tank.transform.position = location;
		tank.transform.parent = transform;
		tank.SetActive (true);
	}

	void placeMech(GameObject mech, Vector3 location)
	{
		mech.transform.position = location;
		mech.transform.parent = transform;
		mech.SetActive (true);
	}
	*/
	/*
	void setDronePos()
	{
		int i = 0;
		foreach (Vector3 location in dronePos) 
		{
			if (i == 0) {
				location = new Vector3 (wait.x - (-0.4f), wait.y - (-10.3f), wait.z - (-7.3f));
				i++;
			}
			else if (i == 1) {
				location
				i++;
			}
			else if (i == 2) {
			
				i++;
			}
			else 
			{
			}

			dronePos [2] = new Vector3 (wait.x - (-2.9f), wait.y - (-10.3f), wait.z - (-1.2f));
			dronePos [3] = new Vector3 (wait.x - (3.6f), wait.y - (-10.3f), wait.z - (-1.2f));
			dronePos [4] = new Vector3 (wait.x - (0.23f), wait.y - (-10.3f), wait.z - (5.9f)); 
		}
	}

	void setTankPos()
	{
		tankPos [1] = new Vector3 (wait.x - (0.5f), wait.y - (0.8f), wait.z - (-3.5f));
		tankPos [2] = new Vector3 (wait.x - (0.5f), wait.y - (-3.9f), wait.z - (16.99f));
	}

	void setMechPos()
	{
		mechPos [1] = new Vector3 (wait.x, wait.y - (8.5f), wait.z - (-9.04f));
	}

*/
}
