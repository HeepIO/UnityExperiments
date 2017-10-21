using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenPlacesScene : MonoBehaviour {

	public Button back;

	void Start()
	{
		Button btn = back.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		SceneManager.LoadScene ("PlacesScene");
	}

}
