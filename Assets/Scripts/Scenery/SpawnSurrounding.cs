using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SpawnSurrounding : MonoBehaviour {
	// To Do
	//Batch Spawning (Spawn multiple in a group) (Make it an independent variable

	public bool Enabled = true;
	public Transform parent;
	public float height = 0.2f;

	public List<SpawnableLocation> SpawnLocations = new List<SpawnableLocation>();


	public List<SpawnableScenery> Spawnable = new List<SpawnableScenery>();

	public List<Collider> NoSpawn = new List<Collider>();


	List<Rect> spawned = new List<Rect>();
	

	void Start () {

		if (!Enabled) {
			this.spawnedAlready = true;
		}
	}
	
	bool spawnedAlready = false;

	bool calculatedAlready = false;

	

	void Update () {
		if (!calculatedAlready) {

			calculateSpawnable();
			//foreach (SpawnableLocation loc in SpawnLocations) {
			//	print(loc.Location.name + " : Percentage - " + loc.percentage + " Area - " + (loc.AreaRect.width * loc.AreaRect.height));
			//	foreach (KeyValuePair<SpawnableScenery, int> dict in loc.SpawnCounts) {
			//		print(dict.Key.SceneryObject.name + " - " + dict.Value);
			//	}
			//}
			calculatedAlready = true;
		}
		if (spawnedAlready) return;

		double timeStampSeconds = (new TimeSpan(DateTime.Now.Ticks)).TotalSeconds;

		foreach (SpawnableScenery item in Spawnable) {

			while (item.CurrentSpawnCount > 0) {
				if(timeStampSeconds + 20 < (new TimeSpan(DateTime.Now.Ticks)).TotalSeconds){
					break;
				}


				Rect loc = this.randomLocation(item);
				item.CurrentSpawnCount -= 1;
				this.Spawn(item, loc);
			}
			
			print(item.SceneryObject.name + " took " + ((new TimeSpan(DateTime.Now.Ticks)).TotalSeconds - timeStampSeconds) + " seconds to spawn!");
			timeStampSeconds = (new TimeSpan(DateTime.Now.Ticks)).TotalSeconds;
		}
		spawnedAlready = true;

	}



	void Spawn(SpawnableScenery item, Rect spawnLocation) {

		GameObject itemSpawned = Instantiate(item.SceneryObject, parent, true);
		itemSpawned.transform.localScale = item.RandomScale();
		itemSpawned.transform.eulerAngles = item.RandomRotation();

		float itemLength = item.GetLength() *1.25f;

		bool found = false;


		Rect rect = new Rect(randomPosition(spawnLocation), new Vector2(itemLength, itemLength));

		while (!found) {
			if (!TestColliding(rect)) {
				found = true;
			} else {
				rect.position = randomPosition(spawnLocation);
			}
		}

		spawned.Add(rect);
		itemSpawned.transform.position = new Vector3(rect.x, height, rect.y);
	}


	Rect randomLocation(SpawnableScenery scenery) {
		

		SpawnableLocation finalLoc = SpawnLocations[SpawnLocations.Count - 1];
		bool found = false;


		foreach(SpawnableLocation loc in SpawnLocations) {
			if(loc.SpawnCounts[scenery] > 0) {
				loc.SpawnCounts[scenery] -= 1;
				finalLoc = loc;
				found = true;
				break;
			}
		}

		if (!found) {
			scenery.CurrentSpawnCount = -1;
		}

		return finalLoc.AreaRect;
	}



	void calculateSpawnable() {

		//Get total area
		float totalArea = 0;

		foreach(SpawnableLocation loc in SpawnLocations) {
			Rect locRect = new Rect(new Vector2(loc.Location.position.x - loc.Location.localScale.x/2, loc.Location.position.z - loc.Location.localScale.z/2), new Vector2(loc.Location.localScale.x, loc.Location.localScale.z));
			loc.AreaRect = locRect;
			totalArea += locRect.width * locRect.height;
		}

		foreach (SpawnableLocation loc in SpawnLocations) {
			loc.percentage = (loc.AreaRect.width * loc.AreaRect.height) / totalArea;
		}

		foreach (SpawnableLocation loc in SpawnLocations) {
			foreach(SpawnableScenery scenery in Spawnable) {
				int spawnAmount = (int)(scenery.MaxSpawnAmount * scenery.SpawnRate * loc.percentage);
				loc.SpawnCounts.Add(scenery, spawnAmount);
				scenery.CurrentSpawnCount += spawnAmount;

			}
		}

	}



	Vector2 randomPosition(Rect rect) {
		Vector2 pos = new Vector2();
		pos.x = rect.x + (rect.size.x * UnityEngine.Random.Range(0.0f, 1.0f));
		pos.y = rect.y + (rect.size.y * UnityEngine.Random.Range(0.0f, 1.0f));


		return pos;
	}



	bool TestColliding(Rect rect1) {
		
		foreach(Rect rect2 in spawned) {
			if (rect1.Overlaps(rect2)) {
				return true;
			}
		}

		foreach(Collider collider in NoSpawn) {
			if(collider.GetType() == typeof(SphereCollider)) {
				SphereCollider sphere = (SphereCollider)collider;

				float sideLength = (rect1.width < rect1.height ? rect1.height : rect1.width);

				if(Vector2.Distance(new Vector2(rect1.x, rect1.y), new Vector2(sphere.transform.position.x, sphere.transform.position.z)) < sphere.radius + sideLength){
					return true;
				}

			} else if(collider.GetType() == typeof(BoxCollider)){
				BoxCollider box = (BoxCollider)collider;
				Rect rect2 = new Rect(box.transform.position.x, box.transform.position.z, box.size.x, box.size.z);
				if (rect1.Overlaps(rect2)) {
					return true;
				}
				
			}
		}

		return false;
	}
	

	Vector2 getCorner(Transform transform) {
		return new Vector2(transform.position.x - transform.localScale.x / 2, transform.position.z - transform.localScale.z / 2);
	}
}
