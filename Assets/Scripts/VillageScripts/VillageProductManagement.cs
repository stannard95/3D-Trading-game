using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class VillageProductManagement {

	private Product wood; // Wood
	private Product iron; //Iron
	private Product steel; //Steel
	private Product food;
	private Product weapons;
	private Product alcohol;
	private List<Product> products;
	private List<string> productIndustry;
	private List<string> productImprovements;

	private string economyStandard;
	private string region;

	public VillageProductManagement(string region, string standard) {
		this.region = region;
		this.economyStandard = standard;
		productIndustry = new List<string> ();
		productIndustry.Add ("Wood");
		productIndustry.Add ("Iron");
		productIndustry.Add ("Steel");
		productIndustry.Add ("Food");
		productIndustry.Add ("Weapons");
		productIndustry.Add ("Alcohol");

		productImprovements = new List<string> ();
		productImprovements.Add ("Wood");
		productImprovements.Add ("Iron");
		productImprovements.Add ("Steel");
		productImprovements.Add ("Food");
		productImprovements.Add ("Weapons");
		productImprovements.Add ("Alcohol");

		initialize ();


	}

	public void setEconomyStandard(string name) {
		this.economyStandard = name;
	}


	public string getStandard() {
		return economyStandard;
	}

	public List<string> getProductIndustryList() {
		return productIndustry;

	}

	public List<string> getProductImprovementsList() {
		return productImprovements;

	}


	public void initialize() {
		products = new List<Product> ();
	
		int[] values = null;

		values = setProductValues ("Wood");

		Product wood = new Product ("Wood", values [0], values [1], values [2], setProductScore("Wood"));
		products.Add (wood);

		values = setProductValues ("Iron");
		Product iron = new Product ("Iron", values [0], values [1], values [2], setProductScore("Iron"));
		products.Add (iron);

		values = setProductValues ("Steel");
		Product steel = new Product ("Steel", values [0], values [1], values [2], setProductScore("Steel"));
		products.Add (steel);

		values = setProductValues ("Food");
		Product food = new Product ("Food", values [0], values [1], values [2], setProductScore("Food"));
		products.Add (food);

		values = setProductValues ("Weapons");
		Product weapons = new Product ("Weapons", values [0], values [1], values [2], setProductScore("Weapons"));
		products.Add (weapons);

		values = setProductValues ("Alcohol");
		Product alcohol = new Product ("Alcohol", values [0], values [1], values [2], setProductScore("Alcohol"));
		products.Add (alcohol);

		for (int i = 0; i < products.Count; i++) {
			products [i].setBuyPrice ();
			products [i].setSellPrice ();
			products [i].setQuantity ();
			setAmountTraded (products [i]);
		}

	}


	//Player earns discount if they have influenced the war
	public void playerDiscount(int score) {
		for (int i = 0; i < products.Count; i++) {
			products [i].discountBuyPrice (score);
		}
	}


	//Change price of products if in state of war
	public void warProducts() {
		for (int i = 0; i < products.Count; i++) {
			if (products [i].getName ().Equals ("Weapons")) {
				products [i].winterShortage ();
			} else if (products [i].getName ().Equals ("Wood")) {
				products [i].winterShortage ();
			}
		}
	}

	//Change price of products if in winter
	public void winterProducts() {
		for (int i = 0; i < products.Count; i++) {
			if (products [i].getName ().Equals ("Food")) {
				products [i].winterShortage ();
			} else if (products [i].getName ().Equals ("Wood")) {
				products [i].winterShortage ();
			}
		}
	}

	public void summerProducts() {
		for (int i = 0; i < products.Count; i++) {
			if (products [i].getName ().Equals ("Food")) {
				products [i].summerIncrease ();
			} else if (products [i].getName ().Equals ("Alcohol")) {
				products [i].summerIncrease ();
			}
		}
	}


	public int[] setProductValues(string name) {
		int quantity = 0; //Quantity of the product;
		int buyPrice = 0; //Price to buy from them;
		int sellPrice = 0; //Price to sell to them;
		int[] values = {quantity, buyPrice, sellPrice};
		if (name.Equals ("Wood")) {
			quantity = Random.Range(10, 20);
			buyPrice = Random.Range (10, 15);
			sellPrice = Random.Range (8, 10);

	

		} 
		else if (name.Equals ("Iron")) {
			quantity = Random.Range(8, 15);
			buyPrice = Random.Range (15, 22);
			sellPrice = Random.Range (10, 18);

		}

		else if (name.Equals ("Steel")) {
			quantity = Random.Range(7, 12);
			buyPrice = Random.Range (20, 25);
			sellPrice = Random.Range (15, 22);

		}

		else if (name.Equals ("Weapons")) {
			quantity = Random.Range(2, 10);
			buyPrice = Random.Range (145, 157);
			sellPrice = Random.Range (140, 145);

		}

		else if (name.Equals ("Food")) {
			quantity = Random.Range(15, 30);
			buyPrice = Random.Range (10, 15);
			sellPrice = Random.Range (8, 14);

		}

		else if (name.Equals ("Alcohol")) {
			quantity = Random.Range(5, 12);
			buyPrice = Random.Range (25, 30);
			sellPrice = Random.Range (20, 25);

		}

		values [0] = quantity;
		values [1] = buyPrice;
		values [2] = sellPrice;

		return values;
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
		




	public int stockTotal() {
		int total = 0;
		for (int i = 0; i < products.Count; i++) {
			total += products [i].getQuantity ();
		}

		return total;
	}

	public double scoreTotal() {
		double total = 0;
		for (int i = 0; i < products.Count; i++) {
			total += products [i].getEcnomyScore ();
		}

		return total;
	}



	public double setProductScore(string productName) {
		double productScore = 0.0;

		if (this.region.Equals ("Grass")) {
			if (productName.Equals ("Wood")) {
				productScore = 0.7;
			} else if (productName.Equals ("Iron")) {
				productScore = 0.5;
			}
			else if (productName.Equals ("Steel")) {
				productScore = 0.4;
			}

			else if (productName.Equals ("Food")) {
				productScore = 0.8;
			}

			else if (productName.Equals ("Weapons")) {
				productScore = 0.3;
			}

			else if (productName.Equals ("Alcohol")) {
				productScore = 0.3;
			}
		}

		else if (this.region.Equals ("Snow")) {
			if (productName.Equals ("Wood")) {
				productScore = 0.8;
			} else if (productName.Equals ("Iron")) {
				productScore = 0.4;
			}
			else if (productName.Equals ("Steel")) {
				productScore = 0.3;
			}

			else if (productName.Equals ("Food")) {
				productScore = 0.6;
			}

			else if (productName.Equals ("Weapons")) {
				productScore = 0.4;
			}

			else if (productName.Equals ("Alcohol")) {
				productScore = 0.2;
			}
		}

		else if (this.region.Equals ("Midlands")) {
			if (productName.Equals ("Wood")) {
				productScore = 0.7;
			} else if (productName.Equals ("Iron")) {
				productScore = 0.5;
			}
			else if (productName.Equals ("Steel")) {
				productScore = 0.4;
			}

			else if (productName.Equals ("Food")) {
				productScore = 0.6;
			}

			else if (productName.Equals ("Weapons")) {
				productScore = 0.5;
			}

			else if (productName.Equals ("Alcohol")) {
				productScore = 0.3;
			}
		}

		else if (this.region.Equals ("Desert")) {
			if (productName.Equals ("Wood")) {
				productScore = 0.5;
			} else if (productName.Equals ("Iron")) {
				productScore = 0.7;
			}
			else if (productName.Equals ("Steel")) {
				productScore = 0.6;
			}

			else if (productName.Equals ("Food")) {
				productScore = 0.6;
			}

			else if (productName.Equals ("Weapons")) {
				productScore = 0.6;
			}

			else if (productName.Equals ("Alcohol")) {
				productScore = 0.3;
			}
		}



		return productScore + setStandardScore();


	}


	public double setStandardScore() {
		double standardScore = 0.0;

		if (this.economyStandard.Equals ("Poverty")) {
			standardScore = 0.2;
		} else if (this.economyStandard.Equals ("Poor")) {
			standardScore = 0.35;
		}
		else if (this.economyStandard.Equals ("Middle Class")) {
			standardScore = 0.50;
		}
		else if (this.economyStandard.Equals ("Rich")) {
			standardScore = 0.65;
		}
		else if (this.economyStandard.Equals ("Prosperous")) {
			standardScore = 0.8;
		}

		return standardScore;

	}



	public void setAmountTraded(Product p) {
		int amountTraded = 0;
		if (p.getEcnomyScore () <= 0.5) {
			amountTraded = Random.Range (20, 25);
		} else if (p.getEcnomyScore () > 0.5 && p.getEcnomyScore () <= 0.7) {
			amountTraded = Random.Range (25, 30);
		}
		else if (p.getEcnomyScore () > 0.7 && p.getEcnomyScore () <= 1.0) {
			amountTraded = Random.Range (30, 35);
		}
		else if (p.getEcnomyScore () > 1.0 && p.getEcnomyScore () <= 1.2) {
			amountTraded = Random.Range (35, 40);
		}
		else if (p.getEcnomyScore () > 1.2 && p.getEcnomyScore () <= 1.5) {
			amountTraded = Random.Range (40, 45);
		}
		else if (p.getEcnomyScore () > 1.5 && p.getEcnomyScore () <= 2.0) {
			amountTraded = Random.Range (45, 50);
		}

		p.setAmountTraded (amountTraded);
	}




}