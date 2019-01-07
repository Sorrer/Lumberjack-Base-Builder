using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlaceholder : MonoBehaviour
{
	/// <summary>
	/// Checks if this building collides with the specified one
	/// </summary>
	/// <param name="building2">Building that is tested against</param>
	/// <returns>True - Is colliding| False - Is not colliding</returns>
	public bool CollidesWith(Building building2) {
		

		if (this.GetComponent<Collider>().bounds.Intersects(building2.GetComponent<Collider>().bounds)) { 
			return true;
		}

		return false;
	}



}
