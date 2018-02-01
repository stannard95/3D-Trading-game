using UnityEngine;
using System.Collections;

public class SaveGame : MonoBehaviour {

	public GameObject playerObject;
	private PlayerInfo playerInfoScipt;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void saveGame() {
		Debug.Log ("Game saved");
		playerObject = GameObject.Find ("Player");
		playerInfoScipt = (PlayerInfo)playerObject.GetComponent (typeof(PlayerInfo));
		PlayerPrefs.SetInt ("Gold", playerInfoScipt.getGold ());

		PlayerPrefs.SetFloat ("X", playerObject.transform.position.x);
		PlayerPrefs.SetFloat ("Y", playerObject.transform.position.y);
		PlayerPrefs.SetFloat ("Z", playerObject.transform.position.z);



	}

	public void loadGame() {
		if (checkSave ()) {
			playerObject = GameObject.Find ("Player");
			playerInfoScipt = (PlayerInfo)playerObject.GetComponent (typeof(PlayerInfo));
			Vector3 playerPos = new Vector3 (PlayerPrefs.GetFloat ("X"), PlayerPrefs.GetFloat ("Y"), PlayerPrefs.GetFloat ("Z"));
			playerObject.transform.position = playerPos;
			playerInfoScipt.setGold (PlayerPrefs.GetInt ("Gold"));
		} 
	}


	public bool checkSave() {
		bool exists = true;
		if (!PlayerPrefs.HasKey ("Gold")) {
			exists = false;
		} 
		if (!PlayerPrefs.HasKey ("X")) {
			exists = false;
		}
		if (!PlayerPrefs.HasKey ("Y")) {
			exists = false;
		}
		if (!PlayerPrefs.HasKey ("Z")) {
			exists = false;
		}
		return exists;

	}

	public void clear() {
		if (checkSave ()) {
			PlayerPrefs.DeleteAll ();
		}
	}
}
