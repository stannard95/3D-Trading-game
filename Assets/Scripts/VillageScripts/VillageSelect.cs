using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VillageSelect : MonoBehaviour {


	private VillageProductManagement village;
	public int population, color;
	public TextMesh textName;
	public string region, standard;
	private int taxrate;
	public int livingStandardScore;


	void Start () {
		setLivingStandardByScore ();
		village = new VillageProductManagement (region, standard);
		textName.text = transform.name;
	

	}
	
	// Update is called once per frame
	void Update () {
		standard = standard;
	}

	public VillageProductManagement getVillage() {
		return village;
	}

	public string getName() {
		return textName.ToString ();
	}

	void OnMouseDown(){
		Debug.Log (transform.name);
	}

	public void newDayNewMarket() {
		setLivingStandardByScore ();
			village.initialize();
	}

	public void winter() {
		setLivingStandardByScore ();
		village.initialize ();
		village.winterProducts ();
	}

	public void summer() {
		setLivingStandardByScore ();
		village.initialize ();
		village.summerProducts ();
	}

	public void discount(int score) {
		village.playerDiscount (score);
	}

	public void setRegion(string name) {
		this.region = name;
	}

	public string getRegion() {
		return region;
	}


	//Sets the living standard
	public void setLivingStandardByScore() {
		
		if (livingStandardScore > 70) {
			this.standard = "Prosperous";

		} else if (livingStandardScore >= 60 && livingStandardScore < 70) {
			this.standard = "Rich";
		} else if (livingStandardScore >= 50 && livingStandardScore < 60) {
			this.standard = "Middle Class";
		} else if (livingStandardScore >= 40 && livingStandardScore < 50) {
			this.standard = "Poor";
		} else if (livingStandardScore < 40) {
			this.standard = "Poverty";
		}
	}

	public void loseScore() {
		this.livingStandardScore -= 10;
		setLivingStandardByScore ();

	}

	public void winScore() {
		this.livingStandardScore += 10;
		setLivingStandardByScore ();

	}

	public void improveScore() {
		this.livingStandardScore += 5;
		setLivingStandardByScore ();

	}

	public int getLivingStandard() {
		return livingStandardScore;
	}

	public string getLivingStandardText ()
	{
		return standard;
	}

	//Sets the taxrate
	public void setTaxrate(int value) {
		this.taxrate = value;
	}


	//Returns the taxrate
	public int getTaxrate() {
		return taxrate;
	}

	public TextMesh getText() {
		return textName;
	}
}
