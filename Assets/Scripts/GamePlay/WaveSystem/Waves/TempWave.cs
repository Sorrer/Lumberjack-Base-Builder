using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempWave : WaveSegment {



	public override bool CanSpawn(ref WaveInfo info) {
		return true;
	}

	public override void OnWaveSpawned() {

	}

	public override bool Spawn(ref WaveInfo info) {

		return true;
	}
}
