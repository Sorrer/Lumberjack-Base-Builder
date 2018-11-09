using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public enum BuildingOverwriteType {Instant, Cost, Disabled}

public class Building : Entity
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

	[Header("Settings")]
	[Space(5)]


	public BuildCost Cost			= new BuildCost();

	[Space(2)]
	public BuildCost BuildCost		= new BuildCost();

	[Space(2)]
	public BuildCost Destruction	= new BuildCost();

	[Space(2)]
	public BuildCost BreakCost		= new BuildCost();

	[Space(2)]
	
	///  When placing a building on it, how to function? Break instantly? Unable to?
	public BuildingOverwriteType OverwiteType = BuildingOverwriteType.Disabled;
	
	


	public bool CollidesWith(Building building) {
		return false;
	}
	

	

	

}
