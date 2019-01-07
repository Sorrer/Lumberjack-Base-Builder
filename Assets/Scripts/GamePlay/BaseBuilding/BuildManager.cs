using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

	Rect buildingBounds = new Rect();




	public LayerMask BuildPlaneLayer;
	public LayerMask[] SelectableObjects;

	public Transform BuildingDir;
	public Transform BuildCursor;
	public Transform TargetCursor;
	
	//HOW HIGH IT SHOULD BE
	public Vector3 BuildingCursorOffset;

	//--

	public Building Selected_Building;
	public GameObject Placeholder_Placement;
	public BuildingPlaceholder Placeholder_Placement_Script;

	public bool isBuilding = false;
	public bool BuildActive = false;
	bool SpawnedPlaceholder = false;

	public float CurrentRotation = 0;

	List<Building> currentlyColliding = new List<Building>();

	void Start() {
		this.TargetCursor.position = new Vector3(0, -100, 0);
		GlobalGame.BuildManager = this;
    }

	// TODO: Figure out how rotation works (Like camera?)

    void Update()
    {
		if(GlobalGame.Player.controlPlayer.controlMode != ControlModes.BaseBuilding) {
			BuildActive = false;
			isBuilding = false;
			DisableBuild();
			Destroy(Placeholder_Placement);
			Selected_Building = null;
			TargetCursor.position = new Vector3(0, -130, 0);
		}
		
		if (isBuilding){
			TargetCursor.transform.position = new Vector3(0, -130, 0);
		}

		if (BuildActive && Selected_Building != null && isBuilding) {

			Vector3 point = this.FindMousePointOnPlane();

			TargetCursor.transform.position = point;

			point += Selected_Building.PlacementOffset;
			point += this.BuildingCursorOffset;

			
			this.BuildCursor.position = point;

			print(point);

			if (!CheckBuildable(this.Selected_Building, point)) {
				SpawnInvalid();
			} else {
				SpawnValid();
			}
			this.Placeholder_Placement.transform.localPosition = Vector3.zero;
			Placeholder_Placement.transform.rotation = Quaternion.Euler(Placeholder_Placement.transform.rotation.x, this.CurrentRotation, Placeholder_Placement.transform.rotation.z);


		}

	}

	// ------------- Selecting Control ---------------

	public bool Select() {

		Debug.Log("TRYING");

		RaycastHit hit;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		int SelectionLayers = SelectableObjects[0];

		foreach (int i in SelectableObjects) {
			SelectionLayers |= i;
		}


		if (Physics.Raycast(ray, out hit, 100, SelectionLayers)) {
			
			if (hit.collider != null) {
				Building building = hit.collider.GetComponent<Building>();
				if (building != null && building.type != UnitTypes.Scenery) {
						
					this.Selected_Building = building;
					this.TargetCursor.position = new Vector3(building.transform.position.x, 0, building.transform.position.z);
					Vector3 targetScale = new Vector3(0.35f, 0.5f, 0.35f);

					targetScale.x *= Mathf.Max(building.transform.localScale.x, building.transform.localScale.z)  * 1.62f;
					targetScale.z *= Mathf.Max(building.transform.localScale.x, building.transform.localScale.z) * 1.62f;
					TargetCursor.localScale = targetScale;
					return true;
				}
			} else {
				this.TargetCursor.position = new Vector3(0, -100, 0);
			}
		}

		return false;
	}

	// ------------- Placeholder Control -------------

	void SpawnInvalid() {
		if (!SpawnedPlaceholder) return;

		Destroy(this.Placeholder_Placement);
		this.Placeholder_Placement = Instantiate(this.Selected_Building.InvalidBuildObject);
		this.Placeholder_Placement.transform.parent = this.BuildCursor;
		this.Placeholder_Placement_Script = this.Placeholder_Placement.gameObject.GetComponent<BuildingPlaceholder>();
		SpawnedPlaceholder = false;
	}

	void SpawnValid() {
		if (SpawnedPlaceholder) return;

		if(this.Placeholder_Placement != null)
			Destroy(this.Placeholder_Placement);

		this.Placeholder_Placement = Instantiate(this.Selected_Building.PlaceHolder);
		this.Placeholder_Placement.transform.parent = this.BuildCursor;
		this.Placeholder_Placement_Script = this.Placeholder_Placement.gameObject.GetComponent<BuildingPlaceholder>();
		SpawnedPlaceholder = true;
	}

	// ------------- Building Control -------------

	public void ClearSelectedBuilding() {
		Destroy(this.Placeholder_Placement);
		this.Selected_Building = null;
	}

	public void SetSelectedBuilding(Building building1) {
		this.Selected_Building = building1;
		this.SpawnedPlaceholder = false;
		this.Selected_Building.transform.position = new Vector3(-100, -100, -100);
		Vector3 targetScale = new Vector3(0.35f, 0.5f, 0.35f);

		targetScale.x *= Mathf.Max(building1.transform.localScale.x, building1.transform.localScale.z) * 1.62f;
		targetScale.z *= Mathf.Max(building1.transform.localScale.x, building1.transform.localScale.z) * 1.62f;
		TargetCursor.localScale = targetScale;

		SpawnValid();
	}

	public void EnableBuild() {
		this.SpawnedPlaceholder = false;
		this.isBuilding = true;
	}

	public void EnableBuild(Building building1) {
		if (this.isBuilding) return;
		this.isBuilding = true;
		SetSelectedBuilding(building1);

	}

	public void DisableBuild() {
		this.TargetCursor.position = new Vector3(0, -100, 0);
		if (!this.BuildActive) return;
		this.isBuilding = false;
		this.SpawnedPlaceholder = false;
		Destroy(this.Placeholder_Placement);
	}

	public bool Build() {
		return Build(this.Selected_Building, GetBuildLocation(Selected_Building));
	}
	
	public bool Build(Building building1, Vector3 location) {

		if (CheckBuildable(building1, location, true)) {

			return ForceBuild(building1, location);

		} else { return false; }

	}
	

	//USED WHEN ALREADY CHECKED
	public bool ForceBuild(Building building1, Vector3 location) {

		Vector3 buildLocation = GetBuildLocation(building1);


		GameObject buildingBuilt = Instantiate(building1.Container.gameObject);
		Building builtScript = buildingBuilt.GetComponent<Building>();
		buildingBuilt.transform.SetParent(this.BuildingDir);
		buildingBuilt.transform.position = location;
		buildingBuilt.transform.rotation = Quaternion.Euler(Placeholder_Placement.transform.rotation.x, this.CurrentRotation, Placeholder_Placement.transform.rotation.z);
		builtScript.IsBuilt = true;

		builtScript.Built();
		
	

		return true;
	}
	
	//Rotation

	public void Rotate(float i) {
		this.CurrentRotation += i;
	}

	public void SetRotation(float i) {
		this.CurrentRotation = i;
	}

	// ----------- Utilities ---------------

	public Vector3 GetBuildLocation(Building building1) {
		Vector3 buildLocation = Vector3.zero;

		buildLocation = this.BuildCursor.position;

		buildLocation.y = 0;

		buildLocation += building1.PlacementOffset;

		return buildLocation;
	}


	bool mouseOutside = false;

	public Vector3 FindMousePointOnPlane() {

		Vector3 hitPoint = new Vector3(0, -130, 0);

		//Raycast
		RaycastHit hit;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		mouseOutside = true;

		if (Physics.Raycast(ray, out hit, 100, BuildPlaneLayer)) {
			if (hit.collider != null) {
				hitPoint = hit.point;
				mouseOutside = false;
			}
		}

		return hitPoint;
	}

	List<Building> InstantBreakBuildings = new List<Building>();
	public bool CheckBuildable(Building building1, Vector3 location) {
		return CheckBuildable(building1, location, false);
	}

	public bool CheckBuildable(Building building1, Vector3 location, bool CommitToBuild) {

		if (this.Placeholder_Placement_Script == null) return true;

		if (mouseOutside) return false;

		bool buildable = true;

		List<Building> CostBreakBuildings = new List<Building>();
		InstantBreakBuildings.Clear();

		//Check special conditions
		if (!building1.SpecialPlacementCheck()) {
			return false;
		}

		


			foreach (Building building2 in UnitManager._instance.BuildingList) {

			if (Placeholder_Placement_Script.CollidesWith(building2)) {
				switch (building2.OverwriteType) {
					case BuildingOverwriteType.Cost:
						CostBreakBuildings.Add(building2);
						continue;

					case BuildingOverwriteType.Instant:
						InstantBreakBuildings.Add(building2);
						continue;
				}


				return false;
			}
		}

		foreach (Building building2 in UnitManager._instance.TreeList) {
			if (Placeholder_Placement_Script.CollidesWith(building2)) {

				switch (building2.OverwriteType) {
					case BuildingOverwriteType.Cost:
						CostBreakBuildings.Add(building2);
						continue;

					case BuildingOverwriteType.Instant:
						InstantBreakBuildings.Add(building2);
						continue;
				}


				return false;
			}
		}

		//Second Scenery
		foreach (Building scenery in UnitManager._instance.SceneryList) {
			if (Placeholder_Placement_Script.CollidesWith(scenery)) {

				switch (scenery.OverwriteType) {
					case BuildingOverwriteType.Cost:
						CostBreakBuildings.Add(scenery);
						continue;

					case BuildingOverwriteType.Instant:
						InstantBreakBuildings.Add(scenery);
						continue;
				}

				return false;
			}
		}


		//Check total cost

		BuildCost totalCost = new BuildCost();

		totalCost.Add(building1.BuildCost);

		foreach (Building costBuildings in CostBreakBuildings) {
			totalCost.Add(costBuildings.BreakCost);
		}

		if (!totalCost.EnoughResources()) return false;

		//Commit to building so break and subtract cost

		//Subtract cost

		if (CommitToBuild && buildable) {

			foreach (Building instantBreak in InstantBreakBuildings) {
				instantBreak.DestoryBuilding();
			}

		}

		return buildable;
	}

	
}
