using UnityEngine;
using System.Collections;

public class ProjectileShooter : MonoBehaviour {

	GameObject prefab;

	// Use this for initialization
	void Start () {
		prefab = Resources.Load ("projectile") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			GameObject projectile = Instantiate (prefab) as GameObject;
			Vector3 localForward = transform.worldToLocalMatrix.MultiplyVector (transform.forward);
			projectile.transform.position = transform.position + localForward;
			Rigidbody rb = projectile.GetComponent<Rigidbody> ();

			rb.velocity = transform.forward * 80;
		}
	}
}
