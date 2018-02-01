using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {
	private GameObject playerObject;
	private string name;
	private PlayerInfo playerInfoScript;
	private PlayerProductManagement ppm;
	public Text nameLabel, quantity;

	// Use this for initialization
	void Start () {
		playerObject = GameObject.Find ("Player");
		name = gameObject.name;
		Debug.Log (name);
		Setup ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Setup() {
		playerInfoScript = (PlayerInfo)playerObject.GetComponent (typeof(PlayerInfo));
		ppm = playerInfoScript.getPpm ();
		nameLabel.text = name;
		quantity.text = ppm.getProduct (name).getQuantity ().ToString();

	}
}
