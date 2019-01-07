using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WaveUnit
{

	public abstract bool Spawn(ref WaveInfo info);
	public abstract void OnAfterSpawned();

}
