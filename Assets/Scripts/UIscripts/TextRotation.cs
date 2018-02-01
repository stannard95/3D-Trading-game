using UnityEngine;
using System.Collections;



public class TextRotation : MonoBehaviour {


	private Camera cam;
	private Camera cam2;


	void Start() {
		cam = GameObject.Find("Camera").GetComponent<Camera>();
		cam2 = GameObject.Find ("CameraMap").GetComponent<Camera> ();

	}

	void Update() {

		if (cam.enabled) {
		
			transform.LookAt (transform.position + cam.transform.rotation * Vector3.forward,
				cam.transform.rotation * Vector3.up);
			transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, transform.eulerAngles.z);
		}
		else if(cam2.enabled){
			transform.LookAt (transform.position + cam2.transform.rotation * Vector3.forward,
				cam2.transform.rotation * Vector3.up);
			
			
		}


	}
}
	
		

