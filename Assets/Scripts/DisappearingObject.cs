using UnityEngine;
using System.Collections;

public class DisappearingObject : MonoBehaviour {
	//Apply this class to the prefab containing the broken pieces

	[Tooltip("Time in seconds until this object fades")]
	[SerializeField] private float fadeTime;
	[Tooltip("Time in seconds until object disappears after fading")]
	[SerializeField] private float disappearTime;
	//Time of instantiation of this object
	private float startTime;
	//Time of disappearing
	private float endTime;

	void Start () {
		startTime = Time.time;
		endTime = startTime + fadeTime;
	}

	void Update () {
		if (Time.time >= endTime)
		{
			Collider[] colliders = gameObject.GetComponentsInChildren<Collider> ();
			foreach (Collider i in colliders)
				i.enabled = false;

			if (Time.time >= endTime + disappearTime) 
			{
				Destroy (gameObject);
			}
		}
	}
}
