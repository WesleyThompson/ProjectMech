using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public float health;

	private Renderer[] allRenders;
	private Color originalColor;
	void Start() {
		allRenders = gameObject.GetComponentsInChildren<Renderer> ();
		originalColor = allRenders[0].material.color;
	}

	void FixedUpdate () {
		if (health <= 0) {
			print ("Player killed me!!!!!!!!");
			Destroy (gameObject);
		}
	}

	public void TakeDamage(float dam) {
		health -= dam;
		print ("Enemy health: " + health);
		StartCoroutine (FlashRed ());
	}

	IEnumerator FlashRed() {
		foreach (Renderer r in allRenders) {
			r.material.color = Color.red;
		}
		yield return new WaitForSeconds(0.2F);
		foreach (Renderer r in allRenders) {
			r.material.color = originalColor;
		}
	}
}
