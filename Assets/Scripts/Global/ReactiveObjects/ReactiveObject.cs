using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReactiveObject : MonoBehaviour 
{
	public abstract void OnGameStateChange(GameState state);

	public void Start() {
		GlobalGame.UnitManager.addReactiveObject(this);
	}

	public void OnDestroy() {
		GlobalGame.UnitManager.removeReactiveObject(this);
	}

}
