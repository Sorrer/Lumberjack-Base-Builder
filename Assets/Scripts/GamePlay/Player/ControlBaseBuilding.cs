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

		BuildManager buildManager = GlobalGame.BuildManager;

		buildManager.BuildActive = true;


		if (Input.GetKeyDown(KeyCode.K)) {
			buildManager.EnableBuild(TestBuilding.GetComponent<Building>());
		}

		if (Input.GetKeyDown(KeyCode.L)) {
			buildManager.DisableBuild();

		}
		
		

		if (buildManager.BuildActive && buildManager.isBuilding) {

			if (Input.GetKey(KeyCode.Comma)) {
				buildManager.Rotate(500 * Time.deltaTime);
			}

			if (Input.GetKey(KeyCode.Period)) {
				buildManager.Rotate(-500 * Time.deltaTime);

			}


			if (Input.GetKeyDown(KeyCode.Mouse0)) {
				Debug.Log("Trying to build");
				Debug.Log(GlobalGame.BuildManager.Build());
			}
		} else if(buildManager.BuildActive) {
			if(Input.GetKeyDown(KeyCode.Mouse0)) {
				buildManager.Select();
			}
		}
	}

}
