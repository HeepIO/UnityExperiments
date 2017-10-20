using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacePrefabController : MonoBehaviour {

	public void loadData(Place place) {
		
		gameObject.name = place.placeID;
		var canvas = gameObject.GetComponent<Canvas> ();

		Text namefield = gameObject.GetComponentsInChildren<Text> ()[0] as Text;
		namefield.text = place.name;

	}

}
