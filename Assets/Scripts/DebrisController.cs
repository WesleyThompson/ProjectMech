using UnityEngine;
using System.Collections;

public class DebrisController : MonoBehaviour {
    public float colliderOffDelay;
    public float objectDoneDelay;

    private float startTime;
    private float colliderOffTime;
    private float objectDoneTime;

    //So we don't burn useless cycles
    private bool colliderOff;
    private bool objectDone;

    private Collider[] colliders;

    void Start () {
        startTime = Time.time;
        colliderOffTime = startTime + colliderOffDelay;
        objectDoneTime = colliderOffTime + objectDoneDelay;
        colliderOff = objectDone = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time >= colliderOffTime && !colliderOff) {
            colliders = gameObject.GetComponentsInChildren<Collider>();
            foreach (Collider col in colliders) {
                col.enabled = false;
            }
            colliderOff = true;
        }
        if (Time.time >= objectDoneTime && !objectDone) {
            Destroy(gameObject);
            objectDone = true;
        }
	}
}
