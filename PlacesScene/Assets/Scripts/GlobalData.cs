using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour {

	public static GlobalData store;

	public int numPlaces = 0;

	void Awake () {
		
		if (store == null) {
			DontDestroyOnLoad (gameObject);
			store = this;
		} else if (store != this) {
			Destroy (gameObject);
		}

	}

	void OnGUI() {
		GUI.Label (new Rect (10, 10, 100, 30), "NumPlaces: " + numPlaces);
	}
}


