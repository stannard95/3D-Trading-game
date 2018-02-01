using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GUIHandler : MonoBehaviour {

	public Canvas canvas;
	public string settlementName;
	public GameObject worldManager;

	public RectTransform mainPanelPrefab;
	public RectTransform mainPanel;

	public RectTransform tradePanelPrefab;
	public RectTransform tradePanel;

	public RectTransform economyPanelPrefab;
	public RectTransform economyPanel;

	public RectTransform industryPanelPrefab;
	public RectTransform industryPanel;

	public RectTransform generalPanelPrefab;
	public RectTransform generalPanel;

	private Component[] components;
	public GameObject settlementObject;
	public static bool guiInUse;

	// Use this for initialization
	void Start () {
		worldManager = GameObject.Find ("WorldManager");
	
	}
	
	// Update is called once per frame
	void Update () {
		settlementObject = PlayerController.settlement;
		if (PlayerController.enteredSett && mainPanel==null) {
			createPanel ();
		}
	
	}

	public void createPanel() {
		mainPanel = (RectTransform)Instantiate (mainPanelPrefab);
		components = mainPanel.GetComponentsInChildren<Component> ();
		mainPanel.transform.SetParent (canvas.transform, false);
		VillageSelect villageSelectScript = (VillageSelect)settlementObject.GetComponent (typeof(VillageSelect));
		fillComponent (villageSelectScript);
		guiInUse = true;


	}


	public void fillComponent(VillageSelect villageSelectScript) {
		string name = villageSelectScript.getName ();
		int pop = 5;
		for (int i = 0; i < components.Length; i++) {

			if (components [i].name.Equals ("NameOf")) {
				components [i].GetComponent<Text> ().text = name;
			}

			if(components[i].name.Equals("Number")) {
				components [i].GetComponent<Text> ().text = pop.ToString();
			}

			if (components [i].name.Equals ("GeneralButton")) {
				components [i].GetComponent<Button> ().onClick.AddListener (() => OnGeneral ());
				
			}

			if(components[i].name.Equals("CancelButton")) {
				components[i].GetComponent<Button>().onClick.AddListener (() => Cancel ());
			}

			if(components[i].name.Equals("TradingButton")) {
				components[i].GetComponent<Button>().onClick.AddListener (() => OnTrading ());
			}

			if(components[i].name.Equals("EconomyButton")) {
				components[i].GetComponent<Button>().onClick.AddListener (() => OnEconomy ());
			}

			if (components [i].name.Equals ("IndustryButton")) {
				components[i].GetComponent<Button>().onClick.AddListener (() => OnIndustry ());
			}
		}
	}

	public void Cancel() {
		Destroy (GameObject.Find("SettlementMain(Clone)"));
		PlayerController.enteredSett = false;
		guiInUse = false;
	}

	public void OnTrading() {
		Destroy (GameObject.Find("EconomyPanel(Clone)"));
		Destroy (GameObject.Find("IndustryPanel(Clone)"));
		Destroy (GameObject.Find ("GeneralVillagePanel(Clone)"));
		if (tradePanel == null) {
			tradePanel = (RectTransform)Instantiate (tradePanelPrefab);
			components = mainPanel.GetComponentsInChildren<Component> ();
			tradePanel.transform.SetParent (mainPanel.transform, false);


		}
	}

	public void OnEconomy() {
		 Destroy (GameObject.Find("TradingPanel(Clone)"));
		 Destroy (GameObject.Find("IndustryPanel(Clone)"));
		Destroy (GameObject.Find ("GeneralVillagePanel(Clone)"));
		if (economyPanel == null) {
			economyPanel = (RectTransform)Instantiate (economyPanelPrefab);
			components = mainPanel.GetComponentsInChildren<Component> ();
			economyPanel.transform.SetParent (mainPanel.transform, false);

		}
	}

	public void OnIndustry() {
		Destroy (GameObject.Find("EconomyPanel(Clone)"));
		Destroy (GameObject.Find("TradingPanel(Clone)"));
		Destroy (GameObject.Find ("GeneralVillagePanel(Clone)"));
		if (industryPanel == null) {
			industryPanel = (RectTransform)Instantiate (industryPanelPrefab);
			components = mainPanel.GetComponentsInChildren<Component> ();
			industryPanel.transform.SetParent (mainPanel.transform, false);

		}
	}

	public void OnGeneral() {
		Destroy (GameObject.Find("EconomyPanel(Clone)"));
		Destroy (GameObject.Find("TradingPanel(Clone)"));
		Destroy (GameObject.Find ("IndustryPanel(Clone)"));
		if (generalPanel == null) {
			generalPanel = (RectTransform)Instantiate (generalPanelPrefab);
			components = mainPanel.GetComponentsInChildren<Component> ();
			generalPanel.transform.SetParent (mainPanel, false);
		}
	}


}
