using UnityEngine;
using System.Collections;

namespace Player
{
	public class BulletDamage : MonoBehaviour {

		public float damage;
		
		private Rigidbody rb;
		private Vector3 underMap = new Vector3(0, -200, 0);
		// Use this for initialization
		void Start () {
			rb = GetComponent<Rigidbody> ();
		}

		// need to change this to use Alex's pooling stuff
		void OnCollisionEnter(Collision col)
		{
			rb.isKinematic = true;
			transform.position = underMap;
			GameObject rootObjectOfCollision = col.transform.root.gameObject;
			if (rootObjectOfCollision.CompareTag ("Player")) {
				Health script = rootObjectOfCollision.GetComponent<Health> ();
				script.TakeDamage ("bullet", damage);
			}
		}
	}
}
