using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachTransform : MonoBehaviour {


	// Transforms that attach to this object (Via Position)
	public List<Transform> Transforms = new List<Transform>();

	void Start () {
		
	}
	
	void Update () {
		foreach(Transform tran in Transforms) {
			tran.position = this.transform.position;
		}
	}
}
