using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EconomyGUI : MonoBehaviour {

	public Text nameLabel;
	private Text tradedLabel, productScoreLabel, taxrateLabel;
	private Component[] productComponents;

	private Text standardOfLivingLabel;
	private VillageProductManagement vpm;
	private VillageSelect villageSelectScript;
	private GameObject settlementObject;
	private Button cancelButton, investButton;
	private Text investText;

	public RectTransform productPanelPrefab;
	public RectTransform productPanel;

	public RectTransform parent;
	public GameObject playerObject;
	private PlayerInfo playerInfoScript;


	// Use this for initialization
	void Start () {
		setup ();

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Creates the Economics panel

	public void setup() {
		playerObject = GameObject.Find ("Player");
		playerInfoScript = (PlayerInfo)playerObject.GetComponent (typeof(PlayerInfo));
		settlementObject = PlayerController.settlement;
		villageSelectScript = (VillageSelect)settlementObject.GetComponent (typeof(VillageSelect));
		vpm = villageSelectScript.getVillage();
		nameLabel.text = transform.name;
		standardOfLivingLabel = GameObject.Find ("StandardOfLiving").GetComponent<Text> ();
		taxrateLabel = GameObject.Find ("Taxrate").GetComponent<Text> ();

		taxrateLabel.text = "Tax rate per m: " + villageSelectScript.getTaxrate ().ToString ();
		standardOfLivingLabel.text = "Living standard: " + villageSelectScript.getLivingStandardText() + 
			" (" + villageSelectScript.getLivingStandard () + ")";
		transform.GetComponent<Button> ().onClick.AddListener (() => showProductStats());


	}

	//Creates panel from the product the user clicked on

	public void showProductStats() {

		productPanel = (RectTransform)Instantiate (productPanelPrefab);
		productPanel.transform.SetParent (parent, false);
		productComponents = productPanel.GetComponentsInChildren<Component> ();

		tradedLabel = getComponentInList ("AmountTraded").GetComponent<Text>();
		productScoreLabel = getComponentInList ("ProductScore").GetComponent<Text> ();
		cancelButton = getComponentInList ("Cancel").GetComponent<Button> ();
		investButton = getComponentInList ("InvestImprove").GetComponent<Button> ();

		cancelButton.onClick.AddListener (() => OnCancel ());
		investButton.onClick.AddListener (() => OnInvest ());
		investText = investButton.GetComponentInChildren<Text> ();
		investButton.onClick.AddListener (() => OnInvest ());
		investText.text = "Invest " + getInvestPrice ().ToString () + "g" + " to improve production";
		if (industryPurchased (name)) {
			investText.text = "Invest " + getInvestPrice ().ToString () + "g" + " to improve production";

		} else {
			Destroy (GameObject.Find(investButton.name));

		}


		tradedLabel.text = "Amount traded today: " + vpm.getProduct (name).getAmountTraded ().ToString ();
		productScoreLabel.text = "Product score: " + vpm.getProduct (name).getEcnomyScore ().ToString () + "/2";


	}

	//returns a specified component by name

	public Component getComponentInList(string name) {
		Component c = null;
		for (int i = 0; i < productComponents.Length; i++) {
			if (productComponents [i].name.Equals (name)) {
				c = productComponents [i];
				break;
			}
		}

		return c;
	}

	//Exits a product panel

	public void OnCancel() {
		Destroy (GameObject.Find ("EconomyProductPanel(Clone)"));
	}

	public void OnInvest() {
		if (playerInfoScript.getGold () >= getInvestPrice ()) {
			playerInfoScript.loseGold (getInvestPrice ());
			villageSelectScript.improveScore ();
			investText.text = "Invested";
			for (int i = 0; i < vpm.getProductImprovementsList ().Count; i++) {
				if (name.Equals (vpm.getProductImprovementsList () [i])) {
					vpm.getProductImprovementsList ().RemoveAt (i);
					break;

				}
			}
		}



	}

	public int getInvestPrice() {
		return vpm.getProduct (name).getSellPrice () * 100;
	}

	public bool industryPurchased(string name) {
		bool exists = false;
		if (vpm.getProductImprovementsList ().Contains(name)) {
			exists = true;
		} else {

			exists = false;
		}
		return exists;

	}



}
