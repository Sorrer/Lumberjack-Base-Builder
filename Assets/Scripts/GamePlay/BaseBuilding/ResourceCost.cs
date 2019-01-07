using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerResourceTypes { Wood, Steel, NONE }

[System.Serializable]
public class ResourceCost
{
	[SerializeField]
	public PlayerResourceTypes Type;
	[SerializeField]
	public int Cost = 0;

}
