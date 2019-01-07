using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Loading, Day, Night, Dead }

public class GameLoopManager : MonoBehaviour {

	public static GameLoopManager _instance;

	public LightManager LightingManager;

	GameState CurrentState = GameState.Day;

	void Start() {
		_instance = this;

	}

	void Update() {


		switch (CurrentState) {
			case GameState.Loading:
				UpdateLoad();
				break;
			case GameState.Day:
				UpdateDay();
				break;
			case GameState.Night:
				UpdateNight();
				break;
			case GameState.Dead:
				UpdateDead();
				break;
		}


	}

	//--------Update States---------

	void UpdateLoad() {

	}

	void UpdateDay() {

	}

	void UpdateNight() {

	}

	void UpdateDead() {

	}

	//---------- Init States -----------

	void InitLoad() {

	}
	void InitDay() {

	}
	void InitNight() {

	}
	void InitDead() {

	}

	//---------- Close States ----------

	void FinishLoad() {

	}
	void FinishDay() {
		
	}
	void FinishNight() {

	}
	void FinishDead() {

	}


	//-----------Utilities -------------------

	public GameState SwitchStateTo(GameState state) {

		switch (this.CurrentState) {
			case GameState.Loading:
				FinishLoad();
				break;
			case GameState.Day:
				FinishDay();
				break;
			case GameState.Night:
				FinishNight();
				break;
			case GameState.Dead:
				FinishDead();
				break;
		}

		switch (state) {
			case GameState.Loading:
				InitLoad();
				break;
			case GameState.Day:
				InitDay();
				break;
			case GameState.Night:
				InitNight();
				break;
			case GameState.Dead:
				InitDead();
				break;
		}

		LightingManager.ChangeLight(state);
		
		//Reactive Objects

		foreach(ReactiveObject obj in GlobalGame.UnitManager.ReactiveObjectList) {
			obj.OnGameStateChange(state);
		}

		//
		
		GameState lastState = this.CurrentState;
		this.CurrentState = state;
		GlobalGame.CurrentGameState = state;
		return lastState;

	}

	//-----------Setters and getters-------------

	public GameState CurrentGameState{
		get {
			return this.CurrentState;
		}
		set {
			SwitchStateTo(value);
		}

	}

}
