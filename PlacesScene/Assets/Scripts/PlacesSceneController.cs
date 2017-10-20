using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacesSceneController : MonoBehaviour {

	private List<Place> places = new List<Place>();

	void Update() {

		CheckForNewPlaces ();
	}

	void CheckForNewPlaces() {
		if (GlobalData.store.places.Count > places.Count) {
			places = GlobalData.store.places;
			Debug.Log ("I want to generate " + places.Count + " places");

		}
	}

	void DestroyAllExistingPlaces() {

	}

}