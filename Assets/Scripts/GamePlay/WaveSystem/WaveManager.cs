using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

	public List<Wave> Waves = new List<Wave>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public static bool IsDark(Vector3 location) {
		return IsDark(location, null);
	}
	public static bool IsDark(Vector3 location, Transform target) {

		List<GameLight> lights = GlobalGame.UnitManager.LightList;
		List<GameLight> collidingLights = new List<GameLight>();

		
		//Check if location is inside any light range
		foreach(GameLight light in lights) {
			if (light.RangeCollider.bounds.Contains(location)) {
				collidingLights.Add(light);
			}
		}

		if (collidingLights.Count == 0) return true;


		////Check if location is visible by light
		//foreach(GameLight light in collidingLights) {

		//	RaycastHit hit;

		//		print("OOFFF");
		//		Debug.DrawRay(light.transform.position, (location - light.transform.position).normalized * Vector3.Distance(light.transform.position, location));
		//	if (Physics.Raycast(light.transform.position, (location - light.transform.position).normalized, out hit,  Vector3.Distance(light.transform.position, location), 1 << 29) ){
		//		if (hit.transform == target) continue;
		//		return true;
		//	}

		//}

		//return false;

		return true;
	}
}
