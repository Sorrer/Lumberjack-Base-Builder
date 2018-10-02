using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLights : MonoBehaviour {

	public Transform light1;
	public Transform light2;
	public float speed = 0.5f;

	void Start () {
		
	}
	
	void Update () {
		light1.Rotate(new Vector3(speed * Time.deltaTime, 0, 0));
		light2.Rotate(new Vector3(speed * Time.deltaTime, 0, 0));
	}
}
