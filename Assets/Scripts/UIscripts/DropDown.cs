using UnityEngine;
using System.Collections;

public class DropDown : MonoBehaviour {

	public RectTransform container;
	public bool isOpen;

	void Start () {
		
		container = transform.FindChild ("Container").GetComponent<RectTransform> ();
		isOpen = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (isOpen) {
			Vector3 localScale = container.localScale;
			localScale.y = Mathf.Lerp (localScale.y, 1, Time.deltaTime * 12);
			container.localScale = localScale;
		} 
		else {
			Vector3 localScale = container.localScale;
			localScale.y = Mathf.Lerp (localScale.y, 0, Time.deltaTime * 12);
			container.localScale = localScale;
		}

	
	}

	public void setIsOpen() {
		if (isOpen) {
			isOpen = false;
		} 
		else {
			isOpen = true;
		}
	
	}

}
