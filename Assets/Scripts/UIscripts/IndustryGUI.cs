using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IndustryGUI : MonoBehaviour {

	public GameObject playerObject;
	private PlayerInfo playerInfoScript;
	private PlayerProductManagement ppm;

	private GameObject settlementObject;
	private VillageSelect villageSelectScript;
	private VillageProductManagement vpm;
	private int price, profit;
	public Button buyButton;

	public Text nameLabel, priceLabel, playerGoldLabel, profitLabel;




	// Use this for initialization
	void Start () {
		playerObject = GameObject.Find ("Player");
		name = gameObject.name;
		Setup ();
	}
	
	// Update is called once per frame
	void Update () {
		playerGoldLabel.text = "Your gold: " + playerInfoScript.getGold ().ToString();
	}

	public void Setup() {
		settlementObject = PlayerController.settlement;
		playerInfoScript = (PlayerInfo)playerObject.GetComponent (typeof(PlayerInfo));
		villageSelectScript = (VillageSelect)settlementObject.GetComponent (typeof(VillageSelect));
		ppm = playerInfoScript.getPpm ();
		playerGoldLabel.text = "Your gold: " + playerInfoScript.getGold ().ToString();

		vpm = villageSelectScript.getVillage ();
		price = vpm.getProduct (name).getBuyPrice () * 100;
		profit = vpm.getProduct (name).getAmountTraded () * vpm.getProduct (name).getSellPrice ();

		profitLabel.text = "£ 3 days: "+ profit.ToString ();
		priceLabel.text = "Buy Price: " + price.ToString ();
		nameLabel.text = setIndustryName (name);
		industryPurchased (name);

	}

	public string setIndustryName(string name) {
		string finalName = "";
		if (name.Equals ("Wood")) {
			finalName = "Lumber Mill";
		} else if (name.Equals ("Iron")) {
			finalName = "Iron Mine";
		} else if (name.Equals ("Steel")) {
			finalName = "Steel Factory";
		} else if (name.Equals ("Food")) {
			finalName = "Farm";
		} else if (name.Equals ("Weapons")) {
			finalName = "Smith";
		} else if (name.Equals ("Alcohol")) {
			finalName = "Brewery";
		}

		return finalName;

	}

	//Player buys an industry, removes it from the village so it can't be bought again
	public void buy() {
		if (playerInfoScript.getGold () >= price) {
			playerInfoScript.loseGold (price);

			for (int i = 0; i < vpm.getProductIndustryList().Count; i++) {
				if (name.Equals (vpm.getProductIndustryList()[i])) {
					Debug.Log ("Purchased" + setIndustryName (name));
					vpm.getProductIndustryList ().RemoveAt (i);
					ppm.getVillageIndustrylist ().Add (new VillageIndustry (setIndustryName(name), vpm.getProduct (name).getAmountTraded (), 
						settlementObject.name, vpm.getProduct(name).getBuyPrice()));
					Destroy (GameObject.Find (name));
					break;

				}
			}}
	}

	public void industryPurchased(string name) {
		if (vpm.getProductIndustryList ().Contains(name)) {
			Debug.Log ("True");
		} else {
			Debug.Log ("False");
			Destroy (GameObject.Find (name));
		}
	}
}
