using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GeneralVillageGUI : MonoBehaviour {

	public Text fightingOxford, fightingEssex, fightingLuton, fightingCambridge;
	private GameObject kingdomObject;
	private KingdomManager kingdomManagerScript;

	private GameObject settlementObject;

	public GameObject playerObject;
	private PlayerInfo playerInfoScript;
	public Text foodAmount, troopsAmount, playerInfluence;
	public Button foodButton, troopsButton;
	public GameObject wmObject;
	private WarManager wmScript;

	// Use this for initialization
	void Start () {
		playerObject = GameObject.Find ("Player");
		playerInfoScript = (PlayerInfo)playerObject.GetComponent (typeof(PlayerInfo));
		setup ();
	
	}
	
	// Update is called once per frame
	void Update () {
		setup ();
	}

	public void setup() {
		settlementObject = PlayerController.settlement;
		kingdomObject = settlementObject.transform.parent.transform.parent.gameObject;
		kingdomManagerScript = (KingdomManager)kingdomObject.GetComponent (typeof(KingdomManager));

		if (kingdomManagerScript.getWarOxford ()) {
			fightingOxford.text = "Fighting Oxford" + kingdomManagerScript.getWarString ();
		} else {
			if (!kingdomManagerScript.getName ().Equals ("Kingdom of Oxford")) {
				fightingOxford.text = "Not fighting Oxford";
			} else {
				fightingOxford.text = "Belongs to Oxford";
			}
		}

		if (kingdomManagerScript.getWarEssex ()) {
			fightingEssex.text = "Fighting Essex" + kingdomManagerScript.getWarString ();
		} else {
			if (!kingdomManagerScript.getName ().Equals ("Kingdom of Essex")) {
				fightingEssex.text = "Not fighting Essex";
			}
			else {
				fightingEssex.text = "Belongs To Essex";
			}


		}
		if (kingdomManagerScript.getWarCambridge()) {
			fightingCambridge.text = "Fighting Cambridge" + kingdomManagerScript.getWarString ();
		} else {
			if (!kingdomManagerScript.getName ().Equals ("Kingdom of Cambridge")) {
				fightingCambridge.text = "Not fighting Cambridge";
			}
			else {
				fightingCambridge.text = "Belongs to Cambridge";
			}

		if (kingdomManagerScript.getWarLuton ()) {
			fightingLuton.text = "Fighting Luton" + kingdomManagerScript.getWarString ();
		} else {
				if (!kingdomManagerScript.getName ().Equals ("Kingdom of Luton")) {
				fightingLuton.text = "Not fighting Luton";
			}
			else {
				fightingLuton.text = "Belongs to Luton";
			}


		}

		}
		notAtWar ();


		foodAmount.text = kingdomManagerScript.getFood ().ToString();
		troopsAmount.text = kingdomManagerScript.getTroops ().ToString();
				
		}

	//Disables UI if the kingdom is not at war
	public void notAtWar() {
		if (kingdomManagerScript.notAtWar ()) {
			playerInfluence.text = "";
			foodButton.gameObject.SetActive (false);
			troopsButton.gameObject.SetActive (false);
	
		} else {
			foodButton.gameObject.SetActive (true);
			troopsButton.gameObject.SetActive (true);
			playerInfluence.text = "Your influence: " + kingdomManagerScript.getPlayerInfluence ().ToString ();


		}
	}

	//Lets player sell own food to the kingdom to help with war
	public void sellFood() {
		wmObject = GameObject.Find ("warobject");
		wmScript = (WarManager)wmObject.GetComponent (typeof(WarManager));
		if (playerInfoScript.getPpm ().getProduct ("Food").getQuantity() > 0) {
			playerInfoScript.getPpm ().changeQuantity ("Food", false);
			playerInfoScript.addGold (20);
			kingdomManagerScript.addFood (1);
			kingdomManagerScript.addPlayerInfluence (1);
			//Lose influence to enemy kingdom
			if (wmScript.getAttacking ().getName ().Equals (kingdomManagerScript.getName ())) {
				wmScript.getDefending ().losePlayerInfluence (1);
			} 
			else {
				wmScript.getAttacking ().losePlayerInfluence (1);
			}
		}
		Debug.Log (kingdomManagerScript.getFood ());
	}

	//Lets player invest in troops for the kindoms war
	public void addTroops() {
		wmObject = GameObject.Find ("warobject");
		wmScript = (WarManager)wmObject.GetComponent (typeof(WarManager));
		if (playerInfoScript.getGold() >= 200) {
			playerInfoScript.loseGold (200);
			kingdomManagerScript.addTroops (5);
			kingdomManagerScript.addPlayerInfluence (5);
			//Lose influence to enemy kingdom
			if (wmScript.getAttacking ().getName ().Equals (kingdomManagerScript.getName ())) {
				wmScript.getDefending ().losePlayerInfluence (5);
			} 
			else {
				wmScript.getAttacking ().losePlayerInfluence (5);
			}
		}
		Debug.Log (kingdomManagerScript.getTroops ());
	}



}
