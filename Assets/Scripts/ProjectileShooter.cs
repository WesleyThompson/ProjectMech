using UnityEngine;
using System.Collections;
using Enemy;

public class ProjectileShooter : MonoBehaviour {
	public GameObject tankGunA;
	public GameObject tankGunB;

	GameObject prefab;
	public float projectileSpeed;
	private float rayDebugTime = 5F;
	private float maxRayDistance = 1000000F;

	public float timeBetweenShots = 1.0F;
	private bool fromGunA = true;
	private bool canShoot = true;

	// Use this for initialization
	void Start () {
        //Screen.lockCursor = true;
        //Cursor.visible = true;
        prefab = Resources.Load("projectileWithSmoke") as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
		if (canShoot) {
			if (Input.GetMouseButton (0)) {
				Ray ray = new Ray ();
				ray.origin = transform.position;
				ray.direction = transform.forward;
				RaycastHit hit;


				if (Physics.Raycast (ray, out hit, maxRayDistance)) {
					Debug.DrawRay (ray.origin, ray.direction * maxRayDistance, Color.green, rayDebugTime);
                    if(hit.transform.tag == GlobalVariables.EnemyTag)
                    {
                        hit.transform.GetComponent<EnemyHealth>().TakeDamage(10);
                    }

					GameObject gun = (fromGunA ? tankGunA : tankGunB);

					GameObject projectile = Instantiate (prefab) as GameObject;
					projectile.transform.position = gun.transform.position + gun.transform.forward;
					projectile.transform.LookAt (hit.point);

					Rigidbody rb = projectile.GetComponent<Rigidbody> ();
					rb.velocity = projectile.transform.forward * projectileSpeed;
					Debug.DrawRay (gun.transform.position, gun.transform.forward * maxRayDistance, Color.red, rayDebugTime);
				} else
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
