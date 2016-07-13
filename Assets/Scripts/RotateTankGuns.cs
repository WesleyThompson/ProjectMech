using UnityEngine;
using System.Collections;

public class RotateTankGuns : MonoBehaviour {
	
	public float mouseSensitivity = 1.0F;
	public Texture2D crosshairImage;

	float currAngle = 0.0F;
	private float maxAngle = 50.0F;
	private float minAngle = -3.0F;

	void Update () {
		// move turret with tank body
		//transform.position = player.transform.position + offset;
		//rotate turret
		print(Input.GetAxis("Mouse Y"));
		/*
		float rot = transform.rotation;
		Quaternion eulerRot = new Quaternion.Euler (rot.x, rot.y, rot.z);
		print (transform.rotation);
		*/
		float rotDegrees = Input.GetAxis ("Mouse Y");

		print (currAngle);
		if ((currAngle >= maxAngle && rotDegrees > 0) || (currAngle <= minAngle && rotDegrees < 0)) {
			rotDegrees = 0;
		} else {
			currAngle += rotDegrees;
		}
		transform.Rotate(rotDegrees * -mouseSensitivity, 0, 0);

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
