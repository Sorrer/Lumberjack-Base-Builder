using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public enum BuildingOverwriteType {Instant, Cost, Disabled}

public abstract class Building : Entity
{


	[Header("Objects")]

	[Space(2)]
	///  Object that represents the object that currently is being placed
	public GameObject	PlaceHolder			= null;

	[Space(2)]
	///  Object that represents the a invalid location to be built
	public GameObject	InvalidBuildObject	= null;
	
	[Space(2)]
	/// Damage system for enemies to break
	public Damagable	DamgeSystem			= null;

	[Space(2)]
	///  Bounds that are requied to place
	public Collider[] Bounds = null;

	[Space(5)]
	[Header("Build Cost Settings")]

	[Space(2)]

	public BuildCost BuildCost			= new BuildCost();

	[Space(2)]
	public BuildCost DestructionCost	= new BuildCost();

	[Space(2)]
	public BuildCost BreakCost			= new BuildCost();

	///  When placing a building on it, how to function? Break instantly? Unable to?
	public BuildingOverwriteType OverwiteType = BuildingOverwriteType.Disabled;


	[Space(5)]
	[Header("Build Placement Settings")]
	[Space(2)]

	//Offset is where the building should be placed at point (NOT HOW IT VISUAL LOOKS WHEN PLACING)
	public Vector3 PlacementOffset = new Vector3();

	
	


	public bool CollidesWith(Building building) {
		return false;
	}

	/// <summary>
	/// Called when object is offically built
	/// </summary>
	public abstract void Built();

	

	

}
