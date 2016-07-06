using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject explosionPrefab;

	void OnCollisionEnter(Collision col)
	{
		var explosion = Instantiate (explosionPrefab, transform.position, Quaternion.identity);
		Destroy (this);
		Destroy (explosion, 2);
	}
}
