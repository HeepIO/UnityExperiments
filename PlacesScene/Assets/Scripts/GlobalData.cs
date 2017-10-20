using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour {

	public static GlobalData store;
	public List<Place> places = new List<Place>();

	void Awake () {
		
		if (store == null) {
			DontDestroyOnLoad (gameObject);
			store = this;
		} else if (store != this) {
			Destroy (gameObject);
		}

	}

	void OnGUI() {
		GUI.Label (new Rect (10, 10, 100, 30), "NumPlaces: " + places.Count);
	}
}

public class Place {

	public string name;
	public string placeID;

	public Place(string name, string placeID){}

	public Place() {
		name = "empty";
		placeID = "empty";
	}

}


