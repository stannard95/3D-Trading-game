using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public Camera playerCamera;
	public Camera mapCamera;
	public JoyStick joystick;
	public GameObject cameraMove;

	// Use this for initialization
	void Start () {
		mapCamera.enabled = false;
		playerCamera.enabled = true;
		cameraMove.SetActive (false);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setActive() {

		//Bird eyes camera
		if (playerCamera.enabled == true) {
			playerCamera.enabled = false;
			mapCamera.enabled = true;
			joystick.gameObject.SetActive (false);
			cameraMove.SetActive (true);
		

		}

		//Third person camera
		else {
			playerCamera.enabled = true;
			mapCamera.enabled = false;
			joystick.gameObject.SetActive (true);
			cameraMove.SetActive (false);

		}

	
	}
}
