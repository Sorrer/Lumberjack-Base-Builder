	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBuilding : Building
{

	public override void Built() {
		GlobalGame.UnitManager.addBuilding(this);
	}

	public new bool SpecialPlacementCheck() {
		return true;
	}
}
