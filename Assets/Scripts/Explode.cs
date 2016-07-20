using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {

	public GameObject explosionPrefab;
	public GameObject smokePrefab;
	private ParticleSystem smoke;
	private Vector3 origin = new Vector3(0, 0, 0);

	void Start()
	{
		smoke = smokePrefab.GetComponent<ParticleSystem> ();
	}

	// need to change this to use Alex's pooling stuff
	void OnCollisionEnter(Collision col)
	{
		print ("collision");

		Destroy (Instantiate (explosionPrefab, transform.position, Quaternion.identity), 2);
		smoke.Stop ();

		Destroy (gameObject, 2);
	}
}
