using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoIndicator : MonoBehaviour {

	public Text text;

	// Use this for initialization
	void Start () {
		if (text == null) text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	public void UpdateAmmo (int ammo) {
		text.text = "Ammo: " + ammo;
	}


}
