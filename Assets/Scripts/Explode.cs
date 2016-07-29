using UnityEngine;
using System.Collections;
using Common;

public class Explode : MonoBehaviour {

	public GameObject explosionPrefab;
	public GameObject smokePrefab;
	private ParticleSystem smoke;
	private ObjectPooling poolScript;

	private Rigidbody rb;

	void Awake()
	{
		smoke = smokePrefab.GetComponent<ParticleSystem> ();
		smoke.Play ();
		//explosionSound = GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody>();
		poolScript = GameObject.Find ("TankShellPooler").GetComponent<ObjectPooling> ();
	}

	// need to change this to use Alex's pooling stuff
	void OnCollisionEnter(Collision col)
	{
		Destroy (Instantiate (explosionPrefab, transform.position, Quaternion.identity), 2);
		StartCoroutine(poolScript.ReturnObject (gameObject)); // return to object pooler
	}
}