using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GeneralGUI : MonoBehaviour {

	public Text goldLabel;
	private GameObject playerObject;
	private PlayerInfo playerInfoScript;
	private PlayerProductManagement ppm;

	private GameObject worldManagerObject;
	private WorldManager worldManagerScript;

	// Use this for initialization
	void Start () {
		playerObject = GameObject.Find ("Player");
		playerInfoScript = (PlayerInfo)playerObject.GetComponent (typeof(PlayerInfo));
		ppm = playerInfoScript.getPpm ();
		goldLabel.text = "Gold: " + playerInfoScript.getGold ().ToString ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
