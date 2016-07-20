using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;

public class ContinuousSpawning : MonoBehaviour {
    public List<GameObject> dropShipSpawnPoints;
    public float waveTime = 3;
    public float changeWaveTimeBy = 0.5f;
    public float spawnsUntilTimeChange = 3;
    public float minWaveTime = 1;
    private float spawnCount = 0;
    private float lastWaveTime;

	void Start ()
    {
        lastWaveTime = Time.time;
	}
	
	void Update ()
    {
	    if(Time.time - lastWaveTime > waveTime)
        {
            if(spawnCount > spawnsUntilTimeChange)
            {
                spawnCount = 0;
                waveTime -= changeWaveTimeBy;
                if(waveTime < minWaveTime)
                {
                    waveTime = minWaveTime;
                }
            }
            int rand = Random.Range(0, dropShipSpawnPoints.Count);
            foreach (Transform child in dropShipSpawnPoints[rand].transform)
            {
                GameObject temp;
                temp = SearchObjectPool.GetObject("Turret_Shooter").GetNextObject();
                temp.transform.position = child.transform.position;
                temp.transform.rotation = child.transform.rotation;
                temp.GetComponent<NavMeshAgent>().enabled = true;
            }
            lastWaveTime = Time.time;
            spawnCount++;
        }
	}
}
