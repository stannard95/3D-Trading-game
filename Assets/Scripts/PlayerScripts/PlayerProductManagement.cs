using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerProductManagement {


		private Product wood; // Wood
		private Product iron; //Iron
		private Product steel; //Steel
		private Product food;
		private Product weapons;
		private Product alcohol;
		private List<Product> products;
		private List<VillageIndustry> villageIndustries;
		private int soldCount;

		private string economyStandard;
		private double economyScoreValue;
		private string region;

	public PlayerProductManagement() {
			
			products = new List<Product> ();
			villageIndustries = new List<VillageIndustry> ();
			initialize ();

	}


		/*When the game is started the products are filled with*/
	public void initialize() {

		
	wood = new Product ("Wood", 0, 0, 0, 0.0);


	iron = new Product ("Iron", 0 , 0 , 0 , 0.0);

	
	steel = new Product ("Steel", 0, 0, 0, 0.0);


	weapons = new Product ("Weapons", 0, 0, 0, 0.0);

		
	food = new Product ("Food", 0, 0, 0, 0.0);


	alcohol = new Product ("Alcohol", 0, 0, 0, 0.0);

		products.Add (wood);
		products.Add (iron);
		products.Add (steel);
		products.Add (weapons);
		products.Add (food);
		products.Add (alcohol);


	}
			
	public List<VillageIndustry> getVillageIndustrylist() {
		return villageIndustries;
	}

	public VillageIndustry getVillageIndustry(string name, string location) {
		VillageIndustry vi = null;
		for (int i = 0; i < villageIndustries.Count; i++) {
			if (villageIndustries [i].getName ().Equals (name) && villageIndustries[i].getLocation().Equals(location)) {
				vi = villageIndustries [i];
				break;
			}
		}
		return vi;
	}

	public Product getProduct(string name) {
		Product p = null;
		for (int i = 0; i < products.Count; i++) {
			if (products [i].getName ().Equals (name)) {
				p = products [i];
				break;
			}
		}
		return p;
	}

		//Changes the quantity of a product
	public void changeQuantity(string name, bool choice) {

		if (choice) {
			getProduct(name).changeQuantity (true);

		} 

		else {
			getProduct(name).changeQuantity (false);
		}

	}


	}


