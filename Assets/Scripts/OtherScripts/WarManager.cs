using UnityEngine;
using System.Collections;

public class WarManager : MonoBehaviour {

	public GameObject kingdomObject1, kingdomObject2;
	private KingdomManager kingdom1, kingdom2;
	private bool warWon;


	void Start () {
		
	}

	
	// Update is called once per frame
	void Update () {
		
	
	}

	//Initalizes kingdom war
	public void setup(GameObject k1, GameObject k2) {
		kingdomObject1 = k1;
		kingdomObject2 = k2;
		kingdom1 = (KingdomManager)kingdomObject1.GetComponent (typeof(KingdomManager)); //Atttacking
		kingdom2 = (KingdomManager)kingdomObject2.GetComponent (typeof(KingdomManager)); //Defending
		kingdom1.setup();
		kingdom2.setup();
		kingdom1.startWar (kingdom2.getName ());
		kingdom2.startWar (kingdom1.getName ());
		Debug.Log (k1.name + "< " + k2.name);

		warWon = false;
	}
		

	//Battle takes place between two kingdoms
	public bool battle() {
		bool attackWin = false;

		//Attackers have more food and more troops
		if (kingdom1.getFood () > kingdom2.getFood () && kingdom1.getTroops () > kingdom2.getTroops ()) {
			attackWin = true;
			kingdom1.loseTroops (Random.Range (0, 10));
			kingdom1.loseFood (Random.Range (0, 5));
			kingdom2.loseTroops (Random.Range (10, 20));
			kingdom2.loseFood (Random.Range(5, 10));
		}

		//Attackers have more food, less troops
		else if (kingdom1.getFood () > kingdom2.getFood () && kingdom1.getTroops () < kingdom2.getTroops ()) {
			int rand = Random.Range (1, 5);
			if (rand < 2) {
				attackWin = true;
				kingdom1.loseTroops (Random.Range (1, 15));
				kingdom1.loseFood (Random.Range (1, 5));

				kingdom2.loseTroops (Random.Range (10, 30));
				kingdom2.loseFood (Random.Range(5, 10));

			} else {
				kingdom2.loseTroops (Random.Range (1, 15));
				kingdom2.loseFood (Random.Range (1, 5));

				kingdom1.loseTroops (Random.Range (10, 30));
				kingdom1.loseFood (Random.Range (5, 10));

			}
		}

		//Attackers have less food more troops
		else if (kingdom1.getFood () < kingdom2.getFood () && kingdom1.getTroops () > kingdom2.getTroops ()) {
			int rand = Random.Range (1, 5);
			if (rand < 3) {
				attackWin = true;
				kingdom2.loseTroops (Random.Range (1, 10));
				kingdom2.loseFood (Random.Range (1, 5));

				kingdom1.loseTroops (Random.Range (10, 30));
				kingdom1.loseFood (Random.Range (7, 13));

			} else {
				kingdom1.loseTroops (Random.Range (1, 10));
				kingdom2.loseTroops (Random.Range (10, 20));
				kingdom1.loseFood (Random.Range (1, 5));
				kingdom2.loseFood (Random.Range (1, 5));


			}
		}

		//Attackers have less food and less troops
		else if (kingdom1.getFood () < kingdom2.getFood () && kingdom1.getTroops () < kingdom2.getTroops ()) {
			attackWin = false;
			kingdom2.loseTroops (Random.Range (1, 10));
			kingdom1.loseTroops (Random.Range (10, 20));
			kingdom1.loseFood (Random.Range (1, 10));
			kingdom2.loseFood (Random.Range (1, 5));
		}

		return attackWin;
	}

	//No troops left
	public bool noTroops() {
		bool ended = false;
		if (kingdom1.getTroops () <= 0 || kingdom2.getTroops () <= 0) {
			warWon = true;
			kingdom1.endWar (kingdom2.getName ());
			kingdom2.endWar (kingdom1.getName ());
			ended = true;
		}
		return ended;
	}


	public void endWarStalemate() {
		warWon = true;
		kingdom1.endWar (kingdom2.getName ());
		kingdom2.endWar (kingdom1.getName ());

	}

	public bool warEnded() {
		
		return warWon;
	}

	public KingdomManager getAttacking() {
		return kingdom1;
	}

	public KingdomManager getDefending() {
		return kingdom2;
	}



}
