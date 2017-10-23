#define USE_HEEP
#if USE_HEEP
using Heep;
#endif

using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartHeep : MonoBehaviour {

	HeepDevice myDevice;

	public GameObject GreenPreFab; 
	public GameObject RedPreFab;

	private int LastGreenControlValue = 0;
	private int LastRedControlValue = 0;

	void CreateHeepDevice()
	{
		List <byte> ID = new List<byte>();
		for(byte i = 0; i < 4; i++)
		{
			ID.Add(i);
		}

		DeviceID myID = new DeviceID(ID);
		myDevice = new HeepDevice (myID);

		Control theControl = Control.CreateControl (Control.CtrlInputOutput.input, Control.CtrlType.OnOff, "Green");
		myDevice.AddControl (theControl);
		Control newControl = Control.CreateControl (Control.CtrlInputOutput.input, Control.CtrlType.OnOff, "Red");
		myDevice.AddControl (newControl);
		myDevice.SetDeviceName ("UNITY");
		HeepCommunications.StartHeepServer (myDevice);
	}

	// Use this for initialization
	void Start () {
		CreateHeepDevice ();
	}
	
	// Update is called once per frame
	void Update () {
		int greenCurControlValue = myDevice.GetControlValueByID (0);
		int redCurControlValue = myDevice.GetControlValueByID (1);

		if (greenCurControlValue != LastGreenControlValue) {
			Debug.Log ("Spawn an box");
			Instantiate(GreenPreFab, new Vector3(3, 5, 3), Quaternion.identity);
			LastGreenControlValue = greenCurControlValue;
		}

		if (redCurControlValue != LastRedControlValue) {
			Debug.Log ("Spawn an box");
			Instantiate(RedPreFab, new Vector3(3, 5, 3), Quaternion.identity);
			LastRedControlValue = redCurControlValue;
		}
	}
}
