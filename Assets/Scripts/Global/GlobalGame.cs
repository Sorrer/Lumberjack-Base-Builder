﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGame : MonoBehaviour {


	public static bool Paused = false;
	public static bool EnableEdgeScroll = false;
	public static ControlModes ControlMode = ControlModes.Player;

	public static PlayerMain Player;

	public static GlobalGame _instance;
	public static UnitManager UnitManager;
	public static BuildManager BuildManager;
	public static GameState CurrentGameState;

	// Use this for initialization
	void Start () {
		
		GlobalGame._instance = this;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.F1)) {
			GameLoopManager._instance.SwitchStateTo(GameState.Day);
		}
		if (Input.GetKeyDown(KeyCode.F2)) {
			GameLoopManager._instance.SwitchStateTo(GameState.Night);
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			TogglePause();

			if (Paused) {
			}else {
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
