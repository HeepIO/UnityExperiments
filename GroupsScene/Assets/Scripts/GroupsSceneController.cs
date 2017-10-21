using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupsSceneController : MonoBehaviour {

	public GameObject groupPrefab;
	public string placeID;

	private List<Group> groups = new List<Group>();
	private List<Group> groupsInThisPlace = new List<Group> ();

	void Update() {

		CheckForNew ();
	}

	void CheckForNew() {
		if (GlobalData.store.groups.Count > groups.Count) {
			groups = new List<Group>(GlobalData.store.groups);

			groupsInThisPlace = groups.Where (thisGroup => thisGroup.placeID == GlobalData.store.activePlace).ToList();

			Debug.Log (groupsInThisPlace);
			Debug.Log ("I want to generate " + groupsInThisPlace.Count + " groups");
			InstantiateGroups ();
		}
	}

	void InstantiateGroups() {
		float position = 0f - (4 * (groupsInThisPlace.Count - 1))/2;

		foreach (Group group in groupsInThisPlace) {
			
			if (GameObject.Find(group.groupID) == null)
			{
				var newGroupObject = Instantiate (groupPrefab);
				newGroupObject.GetComponent<GroupPrefabController> ().loadData(group);
				newGroupObject.transform.position = new Vector3 (position, 0, 0);
			}

			position += 4f;
		}
	}

}