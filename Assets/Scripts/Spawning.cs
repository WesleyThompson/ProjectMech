using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;

public class Spawning : MonoBehaviour {
	void Start ()
    {
        foreach (Transform child in transform)
        {
            GameObject temp;
            temp = SearchObjectPool.GetObject("Turret_Shooter").GetNextObject();
            temp.transform.position = child.transform.position;
            temp.transform.rotation = child.transform.rotation;
            temp.GetComponent<NavMeshAgent>().enabled = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
