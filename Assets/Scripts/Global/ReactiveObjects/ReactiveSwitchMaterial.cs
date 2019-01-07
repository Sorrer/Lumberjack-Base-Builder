using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveSwitchMaterial : ReactiveObject

{
	
	public List<MaterialGameStatePair> pairs = new List<MaterialGameStatePair>();

	public override void OnGameStateChange(GameState state) {
		
		foreach(MaterialGameStatePair cur in pairs) {
			if(cur.state == state) {
				GetComponent<Renderer>().material = cur.material;
				break;
			}
		}

	}
}

[System.Serializable]

public class MaterialGameStatePair {
	public GameState state;
	public Material material;
}
