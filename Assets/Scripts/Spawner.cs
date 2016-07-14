using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject enemy;
	public float spawnTime = 3f;
	public Transform spawnPoint;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}
	
	// Update is called once per frame
	void Spawn () {
		Instantiate (enemy, spawnPoint.position, spawnPoint.rotation);
	}
}
