using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {
	public float speed = 3;
	void Update () {
		
		transform.Rotate (0, Time.deltaTime * speed, 0);
	}
}
