using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DateScript : MonoBehaviour {

	public GameObject wmObject;
	private WorldManager worldManagerScript;
	public Text dateLabel;
	void Start () {
		worldManagerScript = (WorldManager)wmObject.GetComponent (typeof(WorldManager));
	}
	
	// Update is called once per frame
	void Update () {
		dateLabel.text = worldManagerScript.getDate ();
	}
}
