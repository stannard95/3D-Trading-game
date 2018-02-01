using UnityEngine;
using System.Collections;

public class KingdomManager : MonoBehaviour {

	public string kingdomName;
	private bool essexWar, oxfordWar, cambridgeWar, lutonWar;
	private bool attackerState; //Determines if the kingdom is attacker(Have to get low score to win if true)
	public string warString;
	private int troops, food;
	public int playerInfluence;

	void Start () {
		kingdomName = transform.name;
		setup ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void losePlayerInfluence(int value) {
		this.playerInfluence -= value;
	}

	public void addPlayerInfluence(int value) {
		this.playerInfluence += value;
	}

	public int getPlayerInfluence() {
		return playerInfluence;
	}

	public string getName() {
		return kingdomName;
	}

	public void changeWarString(string choice) {
		this.warString = choice;
	}

	public string getWarString() {
		return warString;
	}



	//Sets the starting war bools
	public void setup() {
		if (kingdomName.Equals ("Kingdom of Essex")) {
			oxfordWar = false;
			cambridgeWar = false;
			lutonWar = false;
			this.troops = 150;
			this.food = 90;
		} 
		else if (kingdomName.Equals ("Kingdom of Oxford")) {
			essexWar = false;
			cambridgeWar = false;
			lutonWar = false;
			this.troops = 170;
			this.food = 100;
		}

		else if (kingdomName.Equals ("Kingdom of Cambridge")) {
			essexWar = false;
			oxfordWar = false;
			lutonWar = false;
			this.troops = 185;
			this.food = 85;

		}

		else if (kingdomName.Equals ("Kingdom of Luton")) {
			essexWar = false;
			oxfordWar = false;
			cambridgeWar = false;
			this.troops = 220;
			this.food = 60;
		}

		playerInfluence = 50;


	}

	//Starts a war with a kingdom
	public void startWar(string name) {
		if (!name.Equals (kingdomName)) {
			if (name.Equals ("Kingdom of Essex")) {
				essexWar = true;
			}
			else if (name.Equals ("Kingdom of Oxford")) {
				oxfordWar = true;
			}
			if (name.Equals ("Kingdom of Cambridge")) {
				cambridgeWar = true;
			}
			if (name.Equals ("Kingdom of Luton")) {
				lutonWar = true;
			}

		}
	}

	public void endWar(string name) {
		if (!name.Equals (kingdomName)) {
			if (name.Equals ("KingdomEssex")) {
				essexWar = false;
			}
			else if (name.Equals ("KingdomOxford")) {
				oxfordWar = false;
			}
			if (name.Equals ("KingdomCambridge")) {
				cambridgeWar = false;
			}
			if (name.Equals ("KingdomLuton")) {
				lutonWar = false;
			}

		}
	}


	public bool getWarOxford() {
		return oxfordWar;
		
	}
	public bool getWarEssex() {
		return essexWar;

	}
	public bool getWarCambridge() {
		return cambridgeWar;

	}public bool getWarLuton() {
		return lutonWar;

	}

	public void addTroops(int value) {
		this.troops += value;
	}

	public void loseTroops(int value) {
		this.troops -= value;
		if (this.troops <= 0) {
			this.troops = 0;
		}
	}

	public int getTroops() {
		return troops;
	}

	public void addFood(int value) {
		this.food += value;
	}

	public void loseFood(int value) {
		this.food -= value;
		if (this.food <= 0) {
			this.food = 0;
		}
	}

	public int getFood() {
		return food;
	}

	public bool notAtWar() {
		bool notAtWar = false;
		if (!getWarOxford () && !getWarEssex () && !getWarLuton () && !getWarCambridge ()) {
			notAtWar = true;
		}
		return notAtWar;
	}


}
