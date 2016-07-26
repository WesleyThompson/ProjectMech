using UnityEngine;
using System.Collections;

public class ExplosionDamage : MonoBehaviour {

	public float radius;
	private bool didOnce = false;
	private ArrayList parents = new ArrayList();
	void FixedUpdate () {
		if (!didOnce) {
			Collider[] hitColliders = Physics.OverlapSphere (transform.position, radius, 1 << 11); 
			foreach (Collider col in hitColliders) {
				GameObject parent = col.transform.root.gameObject;
				if (!parents.Contains (parent)) {
					parents.Add (parent);
					print (parent.name + " was hit at a distance of: " + Vector3.Distance (transform.position, col.transform.position) + " from the explosion.");
				}
			}
			didOnce = true;
		}
	}
}
