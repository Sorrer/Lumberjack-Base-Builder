using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveSwitchPrefab : ReactiveObject
{	

	public GameObject currentObject;

	public List<PrefabGameStatePair> pairs = new List<PrefabGameStatePair>();

	public override void OnGameStateChange(GameState state) {

		foreach (PrefabGameStatePair cur in pairs) {
			if (cur.state == state) {
				Vector3 lastPosition = currentObject.transform.position;
				Quaternion lastQuat = currentObject.transform.rotation;
				Destroy(currentObject);

				currentObject = Instantiate(cur.prefab, lastPosition, lastQuat, this.transform);

				break;
			}
		}

	}
}

[System.Serializable]
public class PrefabGameStatePair {

	public GameState state;
	public GameObject prefab;
}