using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {



	public int Damage = 0;

	public bool CurrentlyAttacking = false;

	//Seconds
	public float AttackTime = 0;

	public List<WeaponTrigger> Triggers = new List<WeaponTrigger>();
	Timer weaponTimer = new Timer(0);


	public bool Interrupt = false;

	//Clear list after attack is over
	List<Damagable> alreadyAttacked = new List<Damagable>();

	public void Initiate () {
		foreach(WeaponTrigger trigger in Triggers) {
			trigger.weapon = this;
		}

		weaponTimer.SetTimer(AttackTime);
	}

	
	bool executedFinish = false;


	// -------- MAIN LOOP -------------
	
	public void UpdateWeapon() {

		if (weaponTimer.Started) {
			weaponTimer.Update();
		}

		if (weaponTimer.IsDone() && !executedFinish) {
			SwingFinish();
			OnWeaponSwingEnd(false);
		}
		


		if (Interrupt) {
			SwingFinish();
			OnWeaponSwingEnd(true);
		}

	}

	public void Swing() {
		CurrentlyAttacking = true;
		weaponTimer.Start();
		OnWeaponSwingStart();
		executedFinish = false;
	}

	public void SwingFinish() {
		CurrentlyAttacking = false;
		alreadyAttacked.Clear();
		Interrupt = false;
		executedFinish = true;
		weaponTimer.Stop();
	}



	// -------- SETTERS AND GETTERS ---------------

	/// <summary>
	/// Amount of time it takes for the animation to finish (Seconds)
	/// </summary>
	public float SwingTime {
		get {
			return this.AttackTime;
		}

		set {
			if(value > 0) {
				this.AttackTime = value;
			}
		}
	}



	// -------- ABSTRACT METHODS ------------------

	public abstract void OnWeaponHit(Damagable damageManager);
	public abstract void OnWeaponSwingStart();
	public abstract void OnWeaponSwingEnd(bool Interrupted);


	// ------- COLLISION DETECTION ----------------

	public void WeaponTriggerEnter(DamagableMarker marker) {
		WeaponTriggerEnter(marker.DamageManager); 
	}

	public void WeaponTriggerEnter(Damagable damageManager) {

		if (!this.CurrentlyAttacking) return; 
		if (HasBeenAttacked(damageManager)) return;


		alreadyAttacked.Add(damageManager);
		OnWeaponHit(damageManager);



	}

	private bool HasBeenAttacked(Damagable damageManager) {

		foreach(Damagable damage in alreadyAttacked) {
			if (damage == damageManager) return true;
		}

		return false;
	}

	

}
