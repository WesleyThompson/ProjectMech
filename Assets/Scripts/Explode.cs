using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {

	public GameObject explosionPrefab;
	public GameObject smokePrefab;
	private ParticleSystem smoke;
	private Vector3 origin = new Vector3(0, 0, 0);
	//private AudioSource explosionSound;

	private Rigidbody rb;
	void Start()
	{
		smoke = smokePrefab.GetComponent<ParticleSystem> ();
		//explosionSound = GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody>();
	}

	// need to change this to use Alex's pooling stuff
	void OnCollisionEnter(Collision col)
	{
		Destroy (Instantiate (explosionPrefab, transform.position, Quaternion.identity), 2);
		//explosionSound.Play ();
		smoke.Stop ();

		rb.isKinematic = true;
		transform.position = origin;
	}
}
