using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGame : MonoBehaviour {


	public static bool Paused = false;
	public static bool EnableEdgeScroll = false;

	public static GlobalGame _instance;
	public static UnitManager UnitManager_Instance;

	// Use this for initialization
	void Start () {
		GlobalGame._instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			TogglePause();

			if (Paused) {
				Cursor.lockState = CursorLockMode.None;
			}else {
				Cursor.lockState = CursorLockMode.Confined;
			}
		}
	}


	public void TogglePause() {
		Paused = Paused ? false : true;
	}

	public void Pause() {
		Paused = true;
	}

	public void Resume() {
		Paused = false;
	}
}
