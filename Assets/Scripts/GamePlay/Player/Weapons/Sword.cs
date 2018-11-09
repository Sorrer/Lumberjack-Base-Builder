using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {

	}

	public override void OnWeaponHit(Damagable damageManager) {
		print("Triggered");
		damageManager.Damage(25);
	}

	public override void OnWeaponSwingEnd(bool Interrupted) {

	}

	public override void OnWeaponSwingStart() {

	}
}
