using UnityEngine;
using System.Collections;

public class ProjectileShooter : MonoBehaviour {
	public GameObject tankGunA;
	public GameObject tankGunB;

	GameObject prefab;
	private float projectileSpeed = 10F;
	private float rayDebugTime = 5F;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
		Cursor.visible = true;
		prefab = Resources.Load ("projectile") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {

			Ray ray = new Ray();
			ray.origin = transform.position;
			ray.direction = transform.forward;
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 10000F))
			{
				Debug.DrawRay (ray.origin, hit.point, Color.green, rayDebugTime);

				GameObject projectile = Instantiate (prefab, tankGunA.transform.position, Quaternion.identity) as GameObject;
				GameObject projectile2 = Instantiate (prefab, tankGunB.transform.position, Quaternion.identity) as GameObject;
				Rigidbody rb = projectile.GetComponent<Rigidbody> ();
				Rigidbody rb2 = projectile2.GetComponent<Rigidbody> ();


				rb.velocity = projectile.transform.TransformDirection (hit.point * projectileSpeed);
				rb2.velocity = projectile2.transform.TransformDirection (hit.point * projectileSpeed);
				print ("hit");
			}
			else
				print ("nothing");
			/*
			projectile.transform.position = transform.position + localForward;
			Rigidbody rb = projectile.GetComponent<Rigidbody> ();

			rb.velocity = transform.forward * 80;
			*/
		}
	}
}
