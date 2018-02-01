using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinchZoom : MonoBehaviour {

	public float perpZoomSpeed = .5f;
	public float orthZoomSpeed = .5f;
	public int touchCount;


	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {

		touchCount = Input.touchCount;

		Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 25, 80);

		if (touchCount >= 2) {
			Debug.Log ("2 finger");
			Touch touchZero = Input.GetTouch (0);
			Touch touchOne = Input.GetTouch (1);

			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			if (Camera.main.orthographic) {
				// ... change the orthographic size based on the change in distance between the touches.
				Camera.main.orthographicSize += deltaMagnitudeDiff * orthZoomSpeed;

				// Make sure the orthographic size never drops below zero.
				Camera.main.orthographicSize = Mathf.Max (Camera.main.orthographicSize, 0.1f);
			} else {
				// Otherwise change the field of view based on the change in distance between the touches.
				Camera.main.fieldOfView += deltaMagnitudeDiff * perpZoomSpeed;


			}

				
		} 
		if (touchCount == 1) {
			Debug.Log ("1 finger");
		}
		}

	}

