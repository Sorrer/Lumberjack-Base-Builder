 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveInfo {
	//Enemies Current out of Enemies Spawned

	public KeyValuePair<EnemyType, int> EnemyCurrent = new KeyValuePair<EnemyType, int>();

	public KeyValuePair<EnemyType, int> EnemyTotal = new KeyValuePair<EnemyType, int>();



	//Trees Current out of Trees Spawned

	public KeyValuePair<TreeType, int> TreeCurrent = new KeyValuePair<TreeType, int>();

	public KeyValuePair<TreeType, int> TreeTotal = new KeyValuePair<TreeType, int>();




}
