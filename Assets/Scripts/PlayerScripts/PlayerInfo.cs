using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {

	public int gold;
	public static PlayerProductManagement ppm;

	// Use this for initialization
	void Start () {
		gold = 100;
		ppm = new PlayerProductManagement ();
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public PlayerProductManagement getPpm() {
		return ppm;
	}
		
	public int getGold() {
		return gold;
	}

	public void addGold(int value) {
		this.gold += value;
	}

	public void loseGold(int value) {
		this.gold -= value;
	}

	public void setGold(int value) {
		this.gold = value;
	}
}
