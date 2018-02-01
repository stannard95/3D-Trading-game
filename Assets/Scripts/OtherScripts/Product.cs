using UnityEngine;
using System.Collections;

public class Product {

	private string name;
	private int buyPrice;
	private int sellPrice;
	private int quantity;
	private double economyScore;
	private int amountTraded;

	public Product(string name, int quantity, int buyPrice, int sellPrice, double economyScore) {
		this.name = name;
		this.buyPrice = buyPrice;
		this.sellPrice = sellPrice;
		this.quantity = quantity;
		this.economyScore = economyScore;
		this.amountTraded = 0;
	}

	public string getName() {
		return name;
	}

	public void discountBuyPrice(int score) {
		double total = (double)this.buyPrice * getRewardMultiplier (score);
		this.buyPrice = (int)total;
	}

	public void setBuyPrice() {
		double total = (double) this.buyPrice * economyScore;
		this.buyPrice = (int)total;
	}

	public void setSellPrice() {
		double total = (double) this.sellPrice * economyScore;
		this.sellPrice = (int)total;
	}
		
	public int getSellPrice() {
		return sellPrice;
	}

	public int getBuyPrice() {
		return buyPrice;
	}

	public void setQuantity() {
		double total = (double) this.quantity * economyScore;
		this.quantity = (int)total;
	}

	public void randomShortage() {
		this.quantity = Random.Range (1, 3);
		double total = (double)this.sellPrice * 1.5;
		this.sellPrice = (int)total;
		this.buyPrice = sellPrice;
	}

	public void winterShortage() {
		this.quantity = Random.Range (2, 10);
		double total = (double)this.sellPrice * 1.5;
		this.sellPrice = (int)total;
		this.buyPrice = sellPrice;
	}

	public void summerIncrease() {
		this.quantity = Random.Range (20, 40);
		double total = (double)this.buyPrice * 0.7;
		this.buyPrice = (int)total;


	}

	public int getQuantity() {
		return quantity;

	}

	public double getEcnomyScore() {
		return economyScore;
	}

	public void changeQuantity(bool choice) {
		if (choice) {
			quantity++;
		} 

		else {
			quantity--;
			if (quantity < 0) {
				quantity = 0;			
			}
		}
			
	}

	public void setAmountTraded(int value) {
		this.amountTraded = value;
	}

	public int getAmountTraded() {
		return amountTraded;
	}

	public double getRewardMultiplier(int score) {
		double mult = 0.0;
		if (score >= 55 && score < 65) {
			mult = 0.95;
		}
		else if (score >= 65 && score < 75) {
			mult = 0.90;
		}
		else if (score >= 75 && score < 85) {
			mult = 0.85;
		}
		else if (score >= 85 && score  < 95) {
			mult = 0.80;
		}
		else if (score >= 95 && score  < 105) {
			mult = 0.75;
		}
		else if (score >= 105 && score  < 115) {
			mult = 0.70;
		}
		else if (score >= 115  && score < 125) {
			mult = 0.65;
		}
		else if (score >= 125) {
			mult = 0.6;
		}

		else if (score >= 35  && score < 45) {
			mult = 1.05;
		}
		else if (score >= 25  && score < 35) {
			mult = 1.10;
		}
		else if (score >= 15  && score < 25) {
			mult = 1.15;
		}
		else if (score >= 5  && score < 15) {
			mult = 1.20;
		}
		else if (score >= -5  && score < 5) {
			mult = 1.25;
		}
		else if (score <-5) {
			mult = 1.30;
		}

		return mult;
	}

}
