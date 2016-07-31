using UnityEngine;
using System.Collections;
using Common;

public class RotateTankGuns : MonoBehaviour {
	
	public float mouseSensitivity = 1.0F;
	public Texture2D crosshairImage;

	float currAngle = 0.0F;
	private float maxAngle = 50.0F;
	private float minAngle = -3.0F;

	private float xMin;
	private float yMin;

	void Start () {
		xMin = (Screen.width / 2) - (crosshairImage.width / 2);
		yMin = (Screen.height / 2) - (crosshairImage.height / 2);
	}

	void Update () {
		if (!ManageGameState.isPaused) {
			float rotDegrees = Input.GetAxis ("Mouse Y");

			if ((currAngle >= maxAngle && rotDegrees > 0) || (currAngle <= minAngle && rotDegrees < 0)) {
				rotDegrees = 0;
			} else {
				currAngle += rotDegrees;
			}
			transform.Rotate (rotDegrees * -mouseSensitivity, 0, 0);
		}
	}
	void OnGUI()
	{
		if (!ManageGameState.isPaused) {
			GUI.DrawTexture (new Rect (xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
		}
	}
}
