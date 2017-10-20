﻿using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;


public class LoginToFirebase : MonoBehaviour {

	Firebase.Auth.FirebaseAuth auth;
	Firebase.Auth.FirebaseUser user;

	private string testUser = "waxjVMMDemMCfPrKKnXKMlhRofo1";
	public bool editorTesting = false;

	void Start() {
		// Set this before calling into the realtime database.
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://heep-3cddb.firebaseio.com/");
		FirebaseApp.DefaultInstance.SetEditorP12FileName(Application.dataPath + "/FirebaseAssetPackage/Editor Default Resources/Heep-ede80bde542a.p12");
		FirebaseApp.DefaultInstance.SetEditorServiceAccountEmail("unity-user@heep-3cddb.iam.gserviceaccount.com");
		FirebaseApp.DefaultInstance.SetEditorP12Password("notasecret");
		Debug.Log ("Using P12 for Firebase Access in Editor");

		InitializeFirebase ();
		ReadDatabase ();

	}


	void InitializeFirebase() {
		Debug.Log("Setting up Firebase Auth");
		auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
		auth.StateChanged += AuthStateChanged;
		AuthStateChanged(this, null);
	}

	void ReadDatabase() {

		DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

		if (editorTesting) {
			ReadTestUserPlaces ();
		}

	}

	void AuthStateChanged(object sender, System.EventArgs eventArgs) {
		if (auth.CurrentUser != user) {
			
			bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;

			if (!signedIn && user != null) {
				Debug.Log("Signed out " + user.UserId);
			}
			user = auth.CurrentUser;

			if (signedIn) {
				Debug.Log("Signed in " + user.UserId);
			}
		}
	}

	void ReadTestUserPlaces() {

		FirebaseDatabase.DefaultInstance
			.GetReference ("users")
			.Child(testUser)
			.Child ("places")
			.GetValueAsync().ContinueWith(task => {
				
				if (task.IsFaulted) {
					// Handle the error...
					Debug.Log("error");
				}
				else if (task.IsCompleted) {
					DataSnapshot snapshot = task.Result;

					var databaseItems = snapshot.Value as Dictionary<string, object>;

					foreach (var item in databaseItems)
					{
						ReadPlaceTruth(item.Key);
					}
				}
			});
	}

	void ReadPlaceTruth(string placeID) {
		
		FirebaseDatabase.DefaultInstance
			.GetReference ("places")
			.Child(placeID)
			.GetValueAsync().ContinueWith(task => {

				if (task.IsFaulted) {
					// Handle the error...
					Debug.Log("error");
				}
				else if (task.IsCompleted) {
					DataSnapshot snapshot = task.Result;

					var databaseItems = snapshot.Value as Dictionary<string, object>;
					Place newPlace = new Place();

					foreach (var item in databaseItems)
					{
						if (item.Key.Equals("name")) {
							newPlace.name = item.Value as string;
						} else if (item.Key.Equals("placeID")) {
							newPlace.placeID = item.Value as string;
						}
					}

					Debug.Log(newPlace.name + ": " + newPlace.placeID); 
					GlobalData.store.places.Add(newPlace);
				}
			});
	}

}
