using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

	Rect buildingBounds = new Rect();




	public LayerMask BuildPlaneLayer;

	public Transform BuildingDir;
	public Transform BuildCursor;
	public Transform TargetCursor;
	
	//HOW HIGH IT SHOULD BE
	public Vector3 BuildingCursorOffset;

	//--

	public Building Selected_Building;
	public GameObject Placeholder_Placement;


	bool BuildActive = false;
	bool SpawnedPlaceholder = false;


	void Start()
    {
		GlobalGame.BuildManager = this;
    }

	// TODO: Figure out how rotation works (Like camera?)

    void Update()
    {
		TargetCursor.transform.position = new Vector3(0, -100, 0);
		if (BuildActive && Selected_Building != null) {

			Vector3 point = this.FindMousePointOnPlane();

			TargetCursor.transform.position = point;

			point += Selected_Building.PlacementOffset;
			point += this.BuildingCursorOffset;
			this.BuildCursor.position = point;

			if (!CheckBuildable(this.Selected_Building, point)) {

				Destroy(this.Placeholder_Placement);
				this.Placeholder_Placement = Instantiate(this.Selected_Building.InvalidBuildObject);
				this.Placeholder_Placement.transform.parent = this.BuildCursor;
				SpawnedPlaceholder = false;

			} else if (!SpawnedPlaceholder) {

				this.Placeholder_Placement = Instantiate(this.Selected_Building.PlaceHolder);
				this.Placeholder_Placement.transform.parent = this.BuildCursor;
				SpawnedPlaceholder = true;

			}
			this.Placeholder_Placement.transform.localPosition = Vector3.zero;


		}

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
	}

	public void EnableBuild() {
		this.SpawnedPlaceholder = false;
		this.BuildActive = true;
	}

	public void EnableBuild(Building building1) {
		if (this.BuildActive) return;

		this.SpawnedPlaceholder = false;
		this.BuildActive = true;
		SetSelectedBuilding(building1);

	}

	public void DisableBuild() {
		if (!this.BuildActive) return;

		this.SpawnedPlaceholder = false;
		this.BuildActive = false;
		Destroy(this.Placeholder_Placement);
	}
	
	public bool Build(Building building1, Vector3 location) {

		if (CheckBuildable(building1, location)) {
			
			ForceBuild(building1);
			building1.Built();

		} else { return false; }

		return true;
	}


	//USED WHEN ALREADY CHECKED
	public bool ForceBuild(Building building1) {
		//P

		return true;
	}

	// ----------- Utilities ---------------

	public Vector3 FindMousePointOnPlane() {

		Vector3 hitPoint = new Vector3(0, -100, 0);

		//Raycast
		RaycastHit hit;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			
		if (Physics.Raycast(ray, out hit, 100, BuildPlaneLayer)) {

			hitPoint = hit.point;
		}

		return hitPoint;
	}

	public bool CheckBuildable(Building building1, Vector3 location) {

		bool buildable = true;

		//Check Cost

		//First Buildings

		foreach (Building building2 in UnitManager._instance.BuildingList) { 


			//Check if within range of building ((Building max size x 2) + ?? (2)?)

			//Check if bounding box collides with building2 (.CollideWith(building2);)


		}

		foreach (Building building2 in UnitManager._instance.TreeList) {

			//Check if within range of building ((Building max size x 2) + ?? (2)?)

			//Check if bounding box collides with building2 (.CollideWith(building2);)


		}

		//Second Scenery

		foreach (Building scenery in UnitManager._instance.SceneryList) {

		}


		return buildable;
	}

}
