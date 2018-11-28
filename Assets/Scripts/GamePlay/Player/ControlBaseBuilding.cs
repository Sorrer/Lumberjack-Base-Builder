using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBaseBuilding : MonoBehaviour
{

	public GameObject TestBuilding;

	void Start() {
		
	}

	private void Update() {
		if(GlobalGame.ControlMode == ControlModes.BaseBuilding) {
			UpdateControl();
		}
	}

	public void UpdateControl() {
		
		

		if (Input.GetKeyDown(KeyCode.K)) {
			GlobalGame.BuildManager.EnableBuild(TestBuilding.GetComponent<Building>());
		}

		if (Input.GetKeyDown(KeyCode.L)) {
			GlobalGame.BuildManager.DisableBuild();

		}
	}

}
