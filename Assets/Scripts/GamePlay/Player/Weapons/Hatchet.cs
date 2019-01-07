using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatchet : Weapon
{

	public Animator animator;
	public ControlPlayer playerController;

	// Start is called before the first frame update
	void Start()
    {
		this.Initiate();
	}

    // Update is called once per frame
    void Update() {
		if (Input.GetKey(KeyCode.Mouse0) && !this.CurrentlyAttacking && GlobalGame.ControlMode == ControlModes.Player) {
			this.Swing();
			this.animator.SetTrigger("Attack");
			//playerController.RotationEnabled = false;
		}


		UpdateWeapon();
	}

	public override void OnWeaponHit(Damagable damageManager) {
		print("Triggered");
		damageManager.Damage(Damage);
		this.animator.SetTrigger("Stop");
	}

	public override void OnWeaponSwingEnd(bool Interrupted) {
		playerController.RotationEnabled = true;
	}

	public override void OnWeaponSwingStart() {

	}
}
