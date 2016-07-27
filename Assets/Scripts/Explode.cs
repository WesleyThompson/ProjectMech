using UnityEngine;
using System.Collections;
using Common;

public class Explode : MonoBehaviour {

	public GameObject explosionPrefab;
	public GameObject smokePrefab;
	private ParticleSystem smoke;
	private ObjectPooling poolScript;
	//private AudioSource explosionSound;

	private Rigidbody rb;
	void Start()
	{
		smoke = smokePrefab.GetComponent<ParticleSystem> ();
		//explosionSound = GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody>();
		poolScript = GameObject.Find ("TankShellPooler").GetComponent<ObjectPooling> ();
	}

	// need to change this to use Alex's pooling stuff
	void OnCollisionEnter(Collision col)
	{
		Destroy (Instantiate (explosionPrefab, transform.position, Quaternion.identity), 2);
		//explosionSound.Play ();
		smoke.Stop ();

		/*
		rb.isKinematic = true;
		transform.position = underMap;
		*/
		poolScript.ReturnObject (gameObject); // return to object pooler
	}
}
