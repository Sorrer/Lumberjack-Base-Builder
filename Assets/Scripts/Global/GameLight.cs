using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLight : MonoBehaviour 
{

	public Collider RangeCollider;


	public void Start() {
		GlobalGame.UnitManager.addLight(this);
	}

	public void OnDestroy() {
		GlobalGame.UnitManager.removeLight(this);
	}

}
