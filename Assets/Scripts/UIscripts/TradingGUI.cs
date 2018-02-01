using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TradingGUI : MonoBehaviour {

	public Button buyButton, sellButton;
	public Text nameLabel, buyPrice;
	public Text sellPrice, quantity, playerGold;
	private string name;
	private PlayerProductManagement ppm;
	private GameObject settlementObject;
	private VillageProductManagement vpm;
	private VillageSelect villageSelectScript;
	private PlayerInfo playerInfoScript;
	public GameObject player;



	// Use this for initialization
	void Start () {

		player = GameObject.Find ("Player");
		name = gameObject.name;
		Debug.Log (name);
		Setup ();

	
	}

	void Update() {
		vpm = villageSelectScript.getVillage ();
		buyPrice.text = vpm.getProduct (name).getBuyPrice ().ToString ();
		sellPrice.text = vpm.getProduct (name).getSellPrice ().ToString ();
		quantity.text = vpm.getProduct (name).getQuantity ().ToString ();

		
	}

	public void Setup() {
		settlementObject = PlayerController.settlement;
		playerInfoScript = (PlayerInfo)player.GetComponent (typeof(PlayerInfo));
		villageSelectScript = (VillageSelect)settlementObject.GetComponent (typeof(VillageSelect));
		ppm = playerInfoScript.getPpm ();
		playerGold.text = "Your gold: " + playerInfoScript.getGold ().ToString();

		vpm = villageSelectScript.getVillage ();
		buyPrice.text = vpm.getProduct (name).getBuyPrice ().ToString ();
		sellPrice.text = vpm.getProduct (name).getSellPrice ().ToString ();
		quantity.text = vpm.getProduct (name).getQuantity ().ToString ();

		nameLabel.text = name;

	}

	public void buy() {

		if (vpm.getProduct(name).getQuantity() > 0 && playerInfoScript.getGold () >= vpm.getProduct (name).getBuyPrice ()) {
			vpm.getProduct (name).changeQuantity (false);
			ppm.getProduct (name).changeQuantity (true);
			playerInfoScript.loseGold (vpm.getProduct (name).getBuyPrice ());
			playerGold.text = "Your gold: " + playerInfoScript.getGold ().ToString();
			Debug.Log (playerInfoScript.getGold ().ToString());
		}
		//Debug.Log(ppm.getProduct(name).getQuantity());

	}

	public void sell() {
		if (ppm.getProduct (name).getQuantity () > 0) {
			vpm.getProduct (name).changeQuantity (true);
			ppm.getProduct (name).changeQuantity (false);
			playerInfoScript.addGold (vpm.getProduct (name).getSellPrice ());
			playerGold.text = "Your gold: " + playerInfoScript.getGold ().ToString();
		}
	}


	

}
