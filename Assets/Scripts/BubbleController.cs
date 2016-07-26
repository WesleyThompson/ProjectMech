using UnityEngine;
using System.Collections;
using Player;

public class BubbleController : MonoBehaviour {

	public Material shieldMaterial;
	public Material speedMaterial;
	public Material damageMaterial;

	public Energy playerEnergy;

	void Start () {
		playerEnergy = GetComponentInParent<Energy> ();
	}

	void Update () {
		
	}
}
