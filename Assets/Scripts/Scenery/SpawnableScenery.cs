using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableScenery {

	public Vector3 MinScale = new Vector3(0, 0, 0);
	public Vector3 MaxScale = new Vector3(0, 0, 0);

	public Vector3 MinRotation = new Vector3(0, 0, 0);
	public Vector3 MaxRotation = new Vector3(0, 0, 0);

	public bool RadiusFlipZY = false;
	public bool lockXAndZ = false;

	[Range(0.0f, 1.0f)]
	public float SpawnRate = 1.0f;
	public int MaxSpawnAmount = 100;

	public GameObject SceneryObject;
	
	
	Vector3 LastScale = new Vector3(0, 0, 0);

	[HideInInspector]
	public float CurrentSpawnCount = 0;

	public float GetLength() {
		float x = LastScale.x;
		float yz = 0;
		float length = 0;

		if (RadiusFlipZY) {
			yz = LastScale.y;
		} else {
			yz = LastScale.z;
		}

		if(yz > x) {
			length = yz;
		} else {
			length = x;
		}

		return length;

	}

	public Vector3 RandomScale() {
		Vector3 Scale = new Vector3();

		Vector3 Difference = new Vector3();

		Difference.x = MaxScale.x - MinScale.x;
		Difference.y = MaxScale.y - MinScale.y;
		Difference.z = MaxScale.z - MinScale.z;


		Scale.y = MinScale.y + (Difference.y * Random.Range(0.0f, 1.0f));

		if (lockXAndZ) {
			Scale.x = MinScale.x + (Difference.x * Random.Range(0.0f, 1.0f));
			Scale.z = Scale.x;
		} else {
			Scale.x = MinScale.x + (Difference.x * Random.Range(0.0f, 1.0f));
			Scale.z = MinScale.z + (Difference.z * Random.Range(0.0f, 1.0f));
		}
		LastScale = Scale;
		return Scale;
	}

	public Vector3 RandomRotation() {
		Vector3 Rotation = new Vector3();

		Vector3 Difference = new Vector3();

		Difference.x = MaxRotation.x - MinRotation.x;
		Difference.y = MaxRotation.y - MinRotation.y;
		Difference.z = MaxRotation.z - MinRotation.z;

		Rotation.x = MinRotation.x + (Difference.x * Random.Range(0.0f, 1.0f));
		Rotation.y = MinRotation.y + (Difference.y * Random.Range(0.0f, 1.0f));
		Rotation.z = MinRotation.z + (Difference.z * Random.Range(0.0f, 1.0f));

		return Rotation;
	}
		
}
