using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy1 : GenericEnemy {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (!this.controller.DamageManager.IsAlive()) {
			Destroy(this.transform.gameObject);
		}
	}

    public override void Attack(GameObject enemy) {
        print("It's working!");
    }

    public override void Special(GameObject enemy) {

    }
}
