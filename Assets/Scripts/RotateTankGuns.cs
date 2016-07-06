using UnityEngine;
using System.Collections;

public class RotateTankGuns : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;

	private float speed = 75.0F;

	public Texture2D crosshairImage;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}

	// LateUpdate is called once per frame
	void LateUpdate () {
		// move turret with tank body
		//transform.position = player.transform.position + offset;
		//rotate turret
		transform.Rotate (new Vector3(Input.GetAxis("Mouse Y"), 0, 0) * Time.deltaTime * -speed);

		//only need this if turret is NOT a child of player
		//transform.position = player.transform.position + offset;
	}
	void OnGUI()
	{
		float xMin = (Screen.width / 2) - (crosshairImage.width / 2);
		float yMin = (Screen.height / 2) - (crosshairImage.height / 2);
		GUI.DrawTexture (new Rect (xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
	}
}
