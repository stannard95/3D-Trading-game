using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStatsButton : MonoBehaviour {

	public GameObject playerObject;
	private PlayerInfo playerInfoScript;
	private PlayerProductManagement ppm;

	public Canvas canvas;


	public RectTransform playerMainPanelPrefab;
	public RectTransform playerMainPanel;

	public RectTransform tradePanelPrefab;
	public RectTransform tradePanel;

	public RectTransform generalPanelPrefab;
	public RectTransform generalPanel;

	public RectTransform industryPanelPrefab;
	public RectTransform industryPanel;
	public RectTransform elementPrefab;
	public RectTransform element;
	private Component[] components2;


	private Component[] components;


	// Use this for initialization
	void Start () {
		playerObject = GameObject.Find ("Player");

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadStats() {
		playerMainPanel = (RectTransform)Instantiate (playerMainPanelPrefab);
		playerMainPanel.transform.SetParent (canvas.transform, false);
		components = playerMainPanel.GetComponentsInChildren<Component> ();
		fillComponent ();

	}

	public void fillComponent() {
		for (int i = 0; i < components.Length; i++) {

			if(components[i].name.Equals("CancelButton")) {
				components[i].GetComponent<Button>().onClick.AddListener (() => Cancel ());
			}

			if (components [i].name.Equals ("InventoryButton")) {
				components [i].GetComponent<Button> ().onClick.AddListener (() => OnInventory ());
			}

			if (components [i].name.Equals ("GeneralButton")) {
				components [i].GetComponent<Button> ().onClick.AddListener (() => OnGeneral ());
			}

			if (components [i].name.Equals ("IndustryButton")) {
				components [i].GetComponent<Button> ().onClick.AddListener (() => OnIndustry ());
			}


		}
	}

	public void Cancel() {
		Destroy (GameObject.Find("PlayerMenuMain(Clone)"));
		PlayerController.enteredSett = false;

	}


	public void OnInventory() {
		if (tradePanel == null) {
			Destroy (GameObject.Find ("GeneralPanel(Clone)"));
			tradePanel = (RectTransform)Instantiate (tradePanelPrefab);
			components = playerMainPanel.GetComponentsInChildren<Component> ();
			tradePanel.transform.SetParent (playerMainPanel.transform, false);


		}
	
	}

	public void OnGeneral() {
		if (generalPanel == null) {
			generalPanel = (RectTransform)Instantiate (generalPanelPrefab);
			generalPanel.SetParent (playerMainPanel.transform, false);
		}
	}

	public void OnIndustry() {
		RectTransform content = null;
		playerInfoScript = (PlayerInfo)playerObject.GetComponent (typeof(PlayerInfo));
		ppm = playerInfoScript.getPpm ();
		Component[] c = null;
		if (industryPanel == null) {
			

			industryPanel = (RectTransform)Instantiate (industryPanelPrefab);
			components2 = industryPanel.GetComponentsInChildren<Component> ();
		
			for (int i = 0; i < components2.Length; i++) {
				if(components2[i].name.Equals("Content")) {
					content = components2 [i].GetComponent<RectTransform> ();
					break;
				}
			}

			industryPanel.transform.SetParent (playerMainPanel.transform, false);
			if (ppm.getVillageIndustrylist ().Count > 0) {
				for (int i = 0; i < ppm.getVillageIndustrylist ().Count; i++) {
					element = (RectTransform)Instantiate (elementPrefab);
					element.transform.SetParent (content.transform, false);
					c = element.GetComponentsInChildren<Component> ();
					for (int j = 0; j < c.Length; j++) {
						
						if (c [j].name.Equals ("Name")) {
							c [j].GetComponent<Text> ().text = ppm.getVillageIndustrylist () [i].getName ();

						}
						if (c [j].name.Equals ("Location")) {
							c [j].GetComponent<Text> ().text = ppm.getVillageIndustrylist () [i].getLocation ();
						}

						if (c [j].name.Equals ("Profit")) {
							c [j].GetComponent<Text> ().text = "Profit: "+ ppm.getVillageIndustrylist () [i].getProfit ().ToString();

						}

							
					}

				}
					


					
				}
			}
		}

	}


