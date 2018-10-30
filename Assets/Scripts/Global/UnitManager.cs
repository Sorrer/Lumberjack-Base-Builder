using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {

	//Holds current units (Enemies,Player,Trees [All seperate])

	public List<GameObject> Enemies = new List<GameObject>();
	public List<GameObject> Player = new List<GameObject>();
	public List<GameObject> Trees = new List<GameObject>();
	public List<GameObject> Buildings = new List<GameObject>();

	public static UnitManager _instance;
	
	void Start () {
		UnitManager._instance = this;
	}
	
	void Update () {
		
	}

	//Enemies IO
	public void addEnemy(GameObject enemy) {
		this.Enemies.Add(enemy);
	}

	public void removeEnemy(GameObject enemy) {
		this.Enemies.Remove(enemy);
	}

	//Player IO
	public void addPlayer(GameObject enemy) {
		this.Player.Add(enemy);
	}
	public void removePlayer(GameObject enemy) {
		this.Player.Remove(enemy);
	}

	//Trees IO
	public void addTree(GameObject enemy) {
		this.Trees.Add(enemy);
	}
	public void removeTree(GameObject enemy) {
		this.Trees.Remove(enemy);
	}

	//Buildings IO
	public void addBuilding(GameObject enemy) {
		this.Buildings.Add(enemy);
	}

	public void removeBuilding(GameObject enemy) {
		this.Buildings.Remove(enemy);
	}


}
