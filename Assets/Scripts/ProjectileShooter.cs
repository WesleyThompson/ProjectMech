using UnityEngine;
using System.Collections;

public class ProjectileShooter : MonoBehaviour {
	public GameObject tankGunA;
	public GameObject tankGunB;

	GameObject prefab;
	private float projectileSpeed = 10F;
	private float rayDebugTime = 5F;

	public float timeBetweenShots = 1.0F;
	private bool fromGunA = true;
	private bool canShoot = true;

	// Use this for initialization
	void Start () {
		//Screen.lockCursor = true;
		//Cursor.visible = true;
		prefab = Resources.Load ("projectile") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (canShoot) {
			if (Input.GetMouseButton (0)) {
				Ray ray = new Ray ();
				ray.origin = transform.position;
				ray.direction = transform.forward;
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 10000F)) {
					Debug.DrawRay (ray.origin, ray.direction * 40, Color.green, rayDebugTime);

					GameObject projectile;
					if (fromGunA) {
						projectile = Instantiate (prefab, tankGunA.transform.position, Quaternion.identity) as GameObject;
					} else {
						projectile = Instantiate (prefab, tankGunB.transform.position, Quaternion.identity) as GameObject;
					}
					Rigidbody rb = projectile.GetComponent<Rigidbody> ();


					rb.velocity = projectile.transform.TransformDirection (hit.point * projectileSpeed);
					print ("hit");
				} else
					print ("nothing");
				/*
			projectile.transform.position = transform.position + localForward;
			Rigidbody rb = projectile.GetComponent<Rigidbody> ();

			rb.velocity = transform.forward * 80;
			*/
				fromGunA = !fromGunA;
				StartCoroutine (chamberShot ());
			}
		}
	}
	IEnumerator chamberShot() {
		canShoot = false;
		yield return new WaitForSeconds (timeBetweenShots);
		canShoot = true;
	}
}
