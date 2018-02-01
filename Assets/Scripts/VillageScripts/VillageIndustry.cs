using UnityEngine;
using System.Collections;

public class VillageIndustry{
	private string name, location;
	private int output;
	private int profit, buyPrice;


	public VillageIndustry(string name, int output, string location, int buyPrice) {
		this.name = name;
		this.output = output;
		this.location = location;
		this.buyPrice = buyPrice;

	}

	public string getName() {
		return name;
	}

	public string getLocation() {
		return location;
	}

	public int getProfit() {
		return output * buyPrice;
	}
}

