using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class WorldManager : MonoBehaviour {

	public static Application level;
	public static GameObject[] villages;
	public static GameObject[] regions;
	public static GameObject[] kingdoms;
	public int day;
	private string[] month = {"Janurary", "February", "March", "April", "June", "July", "August", 
		"September", "October", "November", "December"};
	public int year;
	private int currentMonthElement;
	public string currentMonth;
	
	public GameObject dayNightObject;
	private DayCycle dayNightScript;

	public GameObject playerObject;
	private PlayerInfo playerInfoScript;

	private PlayerProductManagement ppm;
	private PlayerController pc;

	private bool noon = false;
	private bool winterStart = false;
	private bool summerEffect = false;

	public GameObject canvas;
	public Text foodShortageLabelPrefab;

	public GameObject warManagerObject;
	public GameObject warManagerPrefab;

	public Text battleResult, battleResultPrefab;
	private WarManager wmScript;

	private bool discountEffect = false;
	private bool warPriceEffect = false;

	public RectTransform saveState;
	private SaveGame saveGameScript;


	void Awake() {
		villages = GameObject.FindGameObjectsWithTag ("Village");
		regions = GameObject.FindGameObjectsWithTag ("Region");
		kingdoms = GameObject.FindGameObjectsWithTag ("Kingdom");
		foreach (GameObject i in regions) {
			if (i.transform.parent.name.Equals ("Kingdom of Essex")) {
				foreach (Transform j in i.transform) {
					if (j.tag.Equals ("Village")) {
						VillageSelect villageSelectScript = (VillageSelect)j.GetComponent (typeof(VillageSelect));
						villageSelectScript.setRegion ("Grass");
						villageSelectScript.getText ().color = Color.cyan;

					}

				}
			} else if(i.transform.parent.name.Equals ("Kingdom of Oxford")) {
				foreach (Transform j in i.transform) {
					if (j.tag.Equals ("Village")) {
						VillageSelect villageSelectScript = (VillageSelect)j.GetComponent (typeof(VillageSelect));
						villageSelectScript.setRegion ("Snow");
						villageSelectScript.getText ().color = Color.magenta;

					}
				}
			}
			else if(i.transform.parent.name.Equals ("Kingdom of Cambridge")) {
				foreach (Transform j in i.transform) {
					if (j.tag.Equals ("Village")) {
						VillageSelect villageSelectScript = (VillageSelect)j.GetComponent (typeof(VillageSelect));
						villageSelectScript.setRegion ("Midlands");
						villageSelectScript.getText ().color = Color.white;

					}
				}
			}
			else if(i.transform.parent.name.Equals ("Kingdom of Luton")) {
				foreach (Transform j in i.transform) {
					if (j.tag.Equals ("Village")) {
						VillageSelect villageSelectScript = (VillageSelect)j.GetComponent (typeof(VillageSelect));
						villageSelectScript.setRegion ("Desert");
						villageSelectScript.getText ().color = Color.yellow;

					}
				}
			}
		}
	}

	void Start () {
		
		day = 0;
		currentMonthElement = 0;
		year = 0;
		currentMonth = month [currentMonthElement];
		playerObject = GameObject.Find ("Player");
		pc = (PlayerController)playerObject.GetComponent (typeof(PlayerController));
		canvas = GameObject.Find ("Canvas");
		dayNightScript = (DayCycle)dayNightObject.GetComponent (typeof(DayCycle));

		//Set tax rate
		foreach (GameObject i in regions) {
			
				foreach (Transform j in i.transform) {
					if (j.tag.Equals ("Village")) {
						VillageSelect villageSelectScript = (VillageSelect)j.GetComponent (typeof(VillageSelect));
						if (villageSelectScript.getRegion ().Equals ("Grass")) {
							villageSelectScript.setTaxrate (Random.Range (100, 120));

							
						} 
						else if (villageSelectScript.getRegion ().Equals ("Snow")) {
							villageSelectScript.setTaxrate (Random.Range (80, 120));
							
						}
						else if (villageSelectScript.getRegion ().Equals ("Midlands")) {
						villageSelectScript.setTaxrate (Random.Range (80, 120));

						}
						else if (villageSelectScript.getRegion ().Equals ("Desert")) {
						villageSelectScript.setTaxrate (Random.Range (80, 120));

						}	
					}

				}
		}

		//If save file exists, load game
		saveGameScript = (SaveGame)saveState.GetComponent (typeof(SaveGame));
		saveGameScript.loadGame ();
	}

	
	// Update is called once per frame
	void Update () {

		//End of day
		if (dayNightScript.getDayEnded ()) {
			dayEnded ();
		}


		//Sets a war between two kingdoms	
		if (currentMonth.Equals ("February") && day == 1 && !noon || currentMonth.Equals ("September") && day == 1 && !noon) {
			startWar ();

		}
			
		//War manager
		if (wmScript != null) {

			//A kingdom has no troops left
			if (!wmScript.noTroops ()) {
				//Determines if attackers obtain a new settlement if they win
				if (day == 5 || day == 10) {
					if (!noon && !wmScript.warEnded ()) {
						if (wmScript.battle ()) {
							attackersWin ();
						} 
						else {
							defendersWin ();
						}
					}

				}
			}

			else {
				warOver ();
					
				}
			}

			//Stalemate of war, no winners
			if (currentMonth.Equals ("June") && !wmScript.warEnded () || currentMonth.Equals ("December") && !wmScript.warEnded ()) {
				battleResult = (Text)Instantiate (battleResultPrefab);
				battleResult.transform.SetParent (canvas.transform, false);
				battleResult.text = wmScript.getAttacking ().getName () + " and " + wmScript.getDefending ().getName () + " are at peace";
				wmScript.endWarStalemate ();
				playerInfluenceDiscount (wmScript.getAttacking ().getName (), wmScript.getAttacking().getPlayerInfluence());
				playerInfluenceDiscount (wmScript.getDefending ().getName (), wmScript.getDefending().getPlayerInfluence());

				discountEffect = true;

				warPriceEffect = false;
				Destroy (GameObject.Find ("warobject"));
			}

			//Ends discount if player has earned one
			if (currentMonth.Equals ("July") && discountEffect || currentMonth.Equals ("Janurary") && discountEffect) {
				discountEffect = false;
			}
			
			//Enables summer effect
			if (currentMonth.Equals ("June") && !summerEffect) {
				summerEffect = true;
				summer ();
			}

			//Enables winter effect
			if (currentMonth.Equals ("December") && !winterStart) {
				winterStart = true;
				winter ();
			}


			//Random food shortage initialize, player industry activated
			if (dayNightScript.getTimeTracker () >= 10.000 && !noon) {
				randomFoodPlayerIndustry ();
			}

			//Removes food shortage notification
			if (dayNightScript.getTimeTracker () >= 12.000) {
				Destroy (GameObject.Find ("FoodShortageText(Clone)"));
				Destroy (GameObject.Find ("BattleResult(Clone)"));
			}
	
		}



	//In state of war, change prices
	public void setWarPrices(string name) {
		List<GameObject> villages;
		villages = fill (name);
		for (int i = 0; i < villages.Count; i++) {
			VillageSelect villageSelectScript = (VillageSelect)villages [i].GetComponent (typeof(VillageSelect));
			villageSelectScript.getVillage ().warProducts ();
		}
	}

	//Fills list with kingdom named villages
	public List<GameObject> fill(string name) {
		List<GameObject> vils = new List<GameObject> ();
		for (int i = 0; i < villages.Length; i++) {
			if (villages [i].transform.parent.transform.parent.name.Equals (name)) {
				vils.Add (villages [i]);
			}
		}
		return vils;
	}


	//When the day changes, so do all the products in the settlements
	public void newDay() {
		foreach (GameObject i in regions) {
				foreach (Transform j in i.transform) {
					if (j.tag.Equals ("Village")) {
						VillageSelect villageSelectScript = (VillageSelect)j.GetComponent (typeof(VillageSelect));
						villageSelectScript.newDayNewMarket ();

					}

				}
		}

	}

	//Initialize winter effect, lower quantity, higher sell price
	public void winter() {
		foreach (GameObject i in regions) {
			foreach (Transform j in i.transform) {
				if (j.tag.Equals ("Village")) {
					VillageSelect villageSelectScript = (VillageSelect)j.GetComponent (typeof(VillageSelect));
					if (villageSelectScript.getLivingStandard () < 65) {
						villageSelectScript.winter ();
					}

				}

			}
		}
	}

	//Initialize summer effect, higher quantity, higher buy price
	public void summer() {
		foreach (GameObject i in regions) {
			if (!wmScript.getAttacking ().getName().Equals (i.transform.parent.name) && !wmScript.getDefending()
				.getName().Equals(
				     i.transform.parent.name)) {
				foreach (Transform j in i.transform) {
					if (j.tag.Equals ("Village")) {
						VillageSelect villageSelectScript = (VillageSelect)j.GetComponent (typeof(VillageSelect));
						villageSelectScript.summer ();
					}
				

				}

			}
		}
	}


	//Food shortage of random lower standard village
	public void randomEventNoon() {
		string[] names = { "Wood", "Iron", "Steel", "Food"};
		int randChoice = Random.Range (1, 10); //Decides which event
		int rand = Random.Range (0, names.Length); //Decides which product
		Text foodShortageLabel;
		GameObject randVillage = villages[Random.Range(0, villages.Length)];
		VillageSelect randVillageSelectScript = (VillageSelect)randVillage.GetComponent (typeof(VillageSelect));

		if (randChoice == 1) {
			if (randVillageSelectScript.getLivingStandard () < 55) {
				randVillageSelectScript.getVillage ().getProduct (names [rand]).randomShortage ();
				Debug.Log (names [rand] + " shortage in: " + randVillage.name);
				foodShortageLabel = (Text)Instantiate (foodShortageLabelPrefab);
				foodShortageLabel.transform.SetParent (canvas.transform, false);
				foodShortageLabel.text = names [rand] + " shortage in " + randVillage.name;
				foodShortageLabel.color = randVillageSelectScript.getText ().color;
			}
		} 
		else if(randChoice == 2) {
			randVillageSelectScript.winScore ();
			foodShortageLabel = (Text)Instantiate (foodShortageLabelPrefab);
			foodShortageLabel.transform.SetParent (canvas.transform, false);
			Debug.Log ("p " + randVillage.name);
			foodShortageLabel.text = randVillage.name + " has improved their standard of living";
			foodShortageLabel.color = randVillageSelectScript.getText ().color;


		}
	}

	//Returns the current date
	public string getDate() {
		return currentMonth + ", " + day.ToString ();
	}


	//Day ends
	public void dayEnded() {
		day += 1;

		//No winter
		if (!currentMonth.Equals ("December")  && !discountEffect && !warPriceEffect) {
			newDay ();
			winterStart = false;
		}

		if (!currentMonth.Equals ("August")  && !discountEffect && !warPriceEffect) {
			newDay ();
			summerEffect = false;
		}

		if (!discountEffect && !warPriceEffect) {
			newDay ();
		}
		dayNightScript.setDayEnded (false);
		noon = false;
		if (day == 11) {
			day = 0;
			currentMonthElement += 1;
			if (currentMonthElement >= month.Length) {
				currentMonthElement = 0;
				currentMonth = month [currentMonthElement];
				year++;
			}
			currentMonth = month [currentMonthElement];
		}
	}


	//Food shortage + industry pay-day
	public void randomFoodPlayerIndustry() {
		Debug.Log ("Half day");
		playerInfoScript = (PlayerInfo)playerObject.GetComponent(typeof(PlayerInfo));
		ppm = playerInfoScript.getPpm ();
		randomEventNoon ();
		//Player has industry, add profit per 3 days
		if (day % 3 == 0 && ppm.getVillageIndustrylist().Count > 0) {
			for (int i = 0; i < ppm.getVillageIndustrylist ().Count; i++) {
				playerInfoScript.addGold (ppm.getVillageIndustrylist () [i].getProfit ());
			}
		}
		noon = true;
	}



	//War starts betwween two kingdoms
	public void startWar()  {
		if (warManagerObject == null) {
			int index1 = Random.Range(0, kingdoms.Length);
			int index2 = Random.Range (0, kingdoms.Length);
			while (index2 == index1) {
				index2 = Random.Range (0, kingdoms.Length);
			}
			warManagerObject = (GameObject)Instantiate (warManagerPrefab);
			warManagerObject.name = "warobject";
			wmScript = (WarManager)warManagerObject.GetComponent (typeof(WarManager));
			wmScript.setup (kingdoms[index1], kingdoms[index2]);
			setWarPrices (kingdoms[index1].name);
			setWarPrices (kingdoms[index2].name);
			warPriceEffect = true;
			battleResult = (Text)Instantiate (battleResultPrefab);
			battleResult.transform.SetParent (canvas.transform, false);
			battleResult.text = wmScript.getAttacking ().getName () + " are at war with " + wmScript.getDefending ().getName ();
			noon = true;
		}
	}


	//Attackers win the battle
	public void attackersWin() {

		List<GameObject> winning = fill (wmScript.getAttacking ().getName ());
		List<GameObject> losing = fill (wmScript.getDefending ().getName ());
		if (losing.Count > 0) {

			GameObject losingVillage = losing [Random.Range (0, losing.Count)];
			GameObject winningVillage = winning [Random.Range (0, winning.Count)];
			VillageSelect losingVillageSelect = (VillageSelect)losingVillage.GetComponent (typeof(VillageSelect));
			VillageSelect winningVillageSelect = (VillageSelect)winningVillage.GetComponent (typeof(VillageSelect));

			Vector3 currentPos = losingVillage.transform.position;
			losingVillage.transform.SetParent (winningVillage.transform.parent, false);
			losingVillageSelect.getText ().color = winningVillageSelect.getText ().color;
			losingVillageSelect.loseScore ();
			winningVillageSelect.winScore ();


			losingVillage.transform.position = currentPos;
			battleResult = (Text)Instantiate (battleResultPrefab);
			battleResult.transform.SetParent (canvas.transform, false);
			battleResult.text = wmScript.getAttacking ().getName () + " defeats " + wmScript.getDefending ().getName () +
			" at " + losingVillage.name;
			noon = true;
			Debug.Log ("Attackers win");
		} else {
			
		}
	}



	//Defenders win the battle
	public void defendersWin() {
		Debug.Log ("defenders win");
		battleResult = (Text)Instantiate (battleResultPrefab);
		battleResult.transform.SetParent (canvas.transform, false);
		battleResult.text = wmScript.getDefending ().getName () + " defends from " + wmScript.getAttacking ().getName();
		noon = true;
	}


	//War is finished
	public void warOver() {
		battleResult = (Text)Instantiate (battleResultPrefab);
		battleResult.transform.SetParent (canvas.transform, false);
		battleResult.text = wmScript.getAttacking ().getName () + " and " + wmScript.getDefending ().getName () + " are at peace";
		Debug.Log ("WAR OVER");


		playerInfluenceDiscount (wmScript.getAttacking ().getName (), wmScript.getAttacking ().getPlayerInfluence ());
		playerInfluenceDiscount (wmScript.getDefending ().getName (), wmScript.getDefending ().getPlayerInfluence ());
		discountEffect = true;
		warPriceEffect = false;
		Destroy (GameObject.Find ("warobject"));
	}


	//Player earns a discount
	public void playerInfluenceDiscount(string name, int score) {
		List<GameObject> villagesInf = fill (name);
		for (int i = 0; i < villagesInf.Count; i++) {
			VillageSelect villageSelectScript = (VillageSelect)villagesInf [i].GetComponent (typeof(VillageSelect));
			villageSelectScript.discount (score);
		}
		Debug.Log ("Player Influence");
	}
		

	}

	

	
