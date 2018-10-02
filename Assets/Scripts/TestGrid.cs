using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour {

	public GameObject prefabGridSquare;
	public Transform dir;
	public bool Disabled = false;


	void Start () {

		if (Disabled) { 
			return;
		} 

		int xLength = 100;
		int zLength = 100;

		for(int x = 0; x < xLength; x++) {
			for(int z = 0; z < zLength; z++) {
				CreateSquare(x - xLength/2, z - zLength/2);
			}
		}


	}
	
	void CreateSquare(float x, float z) {
		GameObject gridSquareCopy = Instantiate(prefabGridSquare, dir);

		Vector3 GridPosition = new Vector3(x, 0.01f, z);

		gridSquareCopy.transform.position = GridPosition;
	}

	void Update () {
		
	}
}
