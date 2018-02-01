using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	public bool moveLeft, moveRight, moveForward, moveBack;
	private Rigidbody rb;
	public Camera mapCamera;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody>();


	}
	
	// Update is called once per frame
	void Update () {

		if (moveLeft) {

			transform.Translate (Vector3.left * 40f * Time.deltaTime);

		} else if (moveRight) {
			transform.Translate (Vector3.right * 40f * Time.deltaTime);
		}
		else if (moveForward) {
			transform.Translate (Vector3.forward * 40f * Time.deltaTime);
		}
		else if (moveBack) {
			transform.Translate (Vector3.back * 40f * Time.deltaTime);
		}
	}

	public void MoveMeLeft()
	{
		moveLeft = true;
	}

	public void StopMeLeft()
	{
		moveLeft = false;
	}
	public void MoveMeRight()
	{
		moveRight = true;
	}

	public void StopMeRight()
	{
		moveRight = false;
	}
	public void MoveMeForward()
	{
		moveForward = true;
	}

	public void StopMeForward()
	{
		moveForward = false;
	}
	public void MoveMeBack()
	{
		moveBack = true;
	}

	public void StopMeBack()
	{
		moveBack = false;
	}

}


