using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
	public List<WaveSegment> WaveSegments = new List<WaveSegment>();
}

public abstract class WaveSegment {


	/// <summary>
	/// Triggered when this wave is spawned
	/// </summary>

	public abstract void OnWaveSpawned();

	/// <summary>
	/// Called until returned true
	/// </summary>
	/// <param name="info"></param>
	/// <returns></returns>
	public abstract bool CanSpawn(ref WaveInfo info);

	/// <summary>
	/// Wave spawning (Return true if finished spawned and false if not) Constant updates
	/// </summary>
	/// <param name="info"></param>
	/// <returns></returns>
	public abstract bool Spawn(ref WaveInfo info);


	


}