using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitTypes {Enemy, Player, Tree, Building, Scenery, Light, NONE}
[System.Serializable]
public class UnitManager : MonoBehaviour {

	//Holds current units (Enemies,Player,Trees [All seperate])


	[SerializeField] private List<GenericEnemy> Enemies	= new List<GenericEnemy>();
	[SerializeField] private List<GameObject> Player	= new List<GameObject>();
	[SerializeField] private List<Building> Trees		= new List<Building>();
	[SerializeField] private List<Building> Buildings	= new List<Building>();
	[SerializeField] private List<GameLight> Light = new List<GameLight>();
	[SerializeField] private List<ReactiveObject> ReactiveObjects = new List<ReactiveObject>();

	public List<Building> Scenery = new List<Building>();

	public static UnitManager _instance;

	void Start() {
		UnitManager._instance = this;
		GlobalGame.UnitManager = this;
	}

	void Update() {

	}

	// --------- Getters and Setters--------------

	public List<GenericEnemy> EnemyList {
		get {
			return Enemies;
		}
	}
	public List<GameObject> PlayerList {
		get {
			return Player;
		}
	}
	public List<Building> TreeList {
		get {
			return Trees;
		}
	}
	public List<Building> BuildingList {
		get {
			return Buildings;
		}
	}
	public List<Building> SceneryList {
		get {
			return Scenery;
		}
	}
	public List<GameLight> LightList {
		get {
			return Light;
		}
	}
	public List<ReactiveObject> ReactiveObjectList {
		get {
			return ReactiveObjects;
		}
	}


	// --------- Adding units ----------------

	public bool addUnit(Object obj, UnitTypes type) {

		bool added = false;

		switch (type) {
			case UnitTypes.Enemy:

				if (obj is GenericEnemy) {
					this.addEnemy( (GenericEnemy) obj );
					added = true;
				}

				break;

			case UnitTypes.Player:

				if (obj is GameObject) {
					this.addPlayer((GameObject)obj);
					added = true;
				}

				break;

			case UnitTypes.Tree:

				if (obj is Building) {
					this.addTree((Building)obj);
					added = true;
				}

				break;

			case UnitTypes.Building:

				if (obj is Building) {
					this.addBuilding((Building)obj);
					added = true;
				}

				break;

			case UnitTypes.Scenery:

				if (obj is Building) {
					this.addScenery((Building)obj);
					added = true;
				}

				break;

			case UnitTypes.Light:

				if (obj is GameLight) {
					this.addLight((GameLight)obj);
					added = true;
				}

				break;
		}

		if (!added) {
			Debug.Log("Unable to add " + obj);
			return false;
		}

		return true;

	}

	// ------------ Remove Unit ---------

	public bool removeUnit(Object obj, UnitTypes type) {

		bool removed = false;

		switch (type) {
			case UnitTypes.Enemy:

				if (obj is GenericEnemy) {
					this.removeEnemy((GenericEnemy)obj);
					removed = true;
				}

				break;

			case UnitTypes.Player:

				if (obj is GameObject) {
					this.removePlayer((GameObject)obj);
					removed = true;
				}

				break;

			case UnitTypes.Tree:

				if (obj is Building) {
					this.removeTree((Building)obj);
					removed = true;
				}

				break;

			case UnitTypes.Building:

				if (obj is Building) {
					this.removeBuilding((Building)obj);
					removed = true;
				}

				break;

			case UnitTypes.Scenery:

				if (obj is Building) {
					this.removeScenery((Building)obj);
					removed = true;
				}

				break;

			case UnitTypes.Light:

				if (obj is GameLight) {
					this.removeLight((GameLight)obj);
					removed = true;
				}

				break;
		}

		if (!removed) {
			Debug.Log("Unable to remove " + obj);
			return false;
		}

		return true;
	}

	//Enemies IO
	public void addEnemy(GenericEnemy enemy) {
		this.Enemies.Add(enemy);
	}

	public void removeEnemy(GenericEnemy enemy) {
		this.Enemies.Remove(enemy);
	}

	//Player IO
	public void addPlayer(GameObject player) {
		this.Player.Add(player);
	}
	public void removePlayer(GameObject player) {
		this.Player.Remove(player);
	}

	//Trees IO
	public void addTree(Building tree) {
		this.Trees.Add(tree);
	}
	public void removeTree(Building tree) {
		this.Trees.Remove(tree);
	}

	//Buildings IO
	public void addBuilding(Building building) {
		this.Buildings.Add(building);
	}
	public void removeBuilding(Building building) {
		this.Buildings.Remove(building);
	}


	//Scenery IO
	public void addScenery(Building scenery) {
		this.Scenery.Add(scenery);
	}
	public void removeScenery(Building scenery) {
		this.Scenery.Remove(scenery);
	}

	//Light IO
	public void addLight(GameLight light) {
		this.Light.Add(light);
	}
	public void removeLight(GameLight light) {
		this.Light.Remove(light);
	}

	//ReactiveObjects IO
	public void addReactiveObject(ReactiveObject obj) {
		this.ReactiveObjects.Add(obj);
	}

	public void removeReactiveObject(ReactiveObject obj) {
		this.ReactiveObjects.Remove(obj);
	}
}
