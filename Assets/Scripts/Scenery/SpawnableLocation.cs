using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableLocation {

	public Transform Location;


	[HideInInspector]
	public Rect AreaRect;
	[HideInInspector]
	public float percentage;
	[HideInInspector]
	public Dictionary<SpawnableScenery, int> SpawnCounts = new Dictionary<SpawnableScenery, int>();



}
