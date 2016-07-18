using UnityEngine;
using System.Collections;

public class StartMainScene : MonoBehaviour {
    public GameObject player;
	void Start ()
    {
        Instantiate(player, Vector3.zero, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
