using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BuildCost
{

	public List<ResourceCost> ResourceCosts = new List<ResourceCost>();

	/**
	 * Buildings that require special items
	 * */
	public List<SpecialResourceCost> SpecialResourceCosts = new List<SpecialResourceCost>();


	public void Add(BuildCost cost) {

		foreach(ResourceCost resource1 in ResourceCosts) {

			foreach (ResourceCost resource2 in cost.ResourceCosts) {

				if (resource1.Type == resource2.Type) resource1.Cost += resource2.Cost;

			}

		}

		foreach (SpecialResourceCost resource1 in SpecialResourceCosts) {

			foreach (SpecialResourceCost resource2 in cost.SpecialResourceCosts) {

				if(resource1.item == resource2.item) {
					resource1.Amount += resource2.Amount;
				}

			}

		}



	}

	public bool EnoughResources() {
		return true;
	}


}
