using UnityEngine;
using System.Collections;

public class MenuCameraManager : MonoBehaviour {

	private Transform cameraTransform;
	private Transform cameraDesiredLookAt;

	// Use this for initialization
	void Start () {
		cameraTransform = Camera.main.transform;

	}

	// Update is called once per frame
	private void Update () {
		if (cameraDesiredLookAt != null) {
			cameraTransform.rotation = Quaternion.Slerp (cameraTransform.rotation,
				cameraDesiredLookAt.rotation, 3 * Time.deltaTime);
		}
	}

	public void LookAtMenu(Transform menuTransform)
	{
		cameraDesiredLookAt = menuTransform;
	}
}

