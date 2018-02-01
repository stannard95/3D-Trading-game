using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {

	public JoyStick cameraRotateJoystick;
	private float distance = 50.0f;
	private float currentX = 0.0f;
	private float currentY = 0.0f;
	private float sensivityX = 3.0f;
	private float sensivityY = 1.0f;

	public Transform lookAt;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		currentX += cameraRotateJoystick.InputDirection.x * sensivityX;
		if (currentY + cameraRotateJoystick.InputDirection.z * sensivityY < -15.0f) {
			currentY = -15.0f;
		} else if (currentY + cameraRotateJoystick.InputDirection.z * sensivityY > 30.0f) {
			currentY = 30.0f;
		}
		else {
			currentY += cameraRotateJoystick.InputDirection.z * sensivityY;
		}

	
	}

	private void LateUpdate() {
		
			Vector3 dir = new Vector3 (0, 30, -distance);
			Quaternion rotation = Quaternion.Euler (currentY, currentX, 0);

			transform.position = lookAt.position + rotation * dir;
			transform.LookAt (lookAt);
		



	}
}
