using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildCost
{
	// Wood? Energy?
	public int Cost1 = 0, Cost2 = 0, Cost3 = 0;

	/**
	 * Buildings that require special items
	 * */
	public Item[] SpecialCost;
	


}
