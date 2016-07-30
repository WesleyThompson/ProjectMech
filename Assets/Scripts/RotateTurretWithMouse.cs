using UnityEngine;
using System.Collections;
using Common;

public class RotateTurretWithMouse : MonoBehaviour {

	public float mouseSensitivity = 1.0F;

	void Update () {
		if (!ManageGameState.isPaused) {
			transform.Rotate (0, 0, Input.GetAxis("Mouse X") * mouseSensitivity);
		}
	}
}