using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTreeBuilding : Building {

	public override void Built() {
		GlobalGame.UnitManager.addTree(this);
	}

}
