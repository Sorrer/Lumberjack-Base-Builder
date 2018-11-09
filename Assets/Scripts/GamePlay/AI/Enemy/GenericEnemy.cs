using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericEnemy : GenericAI {
	
    public float wanderRange;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public abstract void Attack(GameObject enemy);
    //get the health controller from the enemy
    //something like this:
    //Health Controller healthController = enemy.GetComponent<HealthController>;



    public abstract void Special(GameObject enemy);

    
}
