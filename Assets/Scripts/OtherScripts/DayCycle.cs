using UnityEngine;
using System.Collections;

public class DayCycle : MonoBehaviour {

	public float timeSpeed, timeTracker;
	public GameObject playerObject;
	private PlayerController playerScript;
	public bool dayEnded;

	void Start () {
		playerObject = GameObject.Find ("Player");
		playerScript = (PlayerController)playerObject.GetComponent (typeof(PlayerController));
		timeSpeed = 2f;
		timeTracker = 0f;
		dayEnded = false;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (timeTracker >= 26f) {
			timeTracker = 0f;
			dayEnded = true;
		}

		if (playerScript.getMoving ()) {

		   timeTracker += 0.05f;
		  // timeTracker += 1f;

		}
	}


	public float getTimeTracker() {
		return timeTracker;
	}

	public bool getDayEnded() {
		return dayEnded;
	}

	public void setDayEnded(bool val) {
		this.dayEnded = val;
	}


	}

