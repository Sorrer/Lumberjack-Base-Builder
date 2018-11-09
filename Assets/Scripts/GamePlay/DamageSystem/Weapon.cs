using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {



	public bool CurrentlyAttacking = false;

	//MS
	public float AttackTime = 0;

	public List<WeaponTrigger> Triggers = new List<WeaponTrigger>();


	public bool Interrupt = false;

	//Clear list after attack is over
	List<Damagable> alreadyAttacked = new List<Damagable>();

	void Start () {
		foreach(WeaponTrigger trigger in Triggers) {
			trigger.weapon = this;
		}

		weaponTimer.SetTimer(AttackTime);
	}

	Timer weaponTimer = new Timer(0);
	
	bool executedFinish = false;

	void Update () {

		if (weaponTimer.Started) {
			weaponTimer.Update();
		}

		if (weaponTimer.IsDone() && !executedFinish) {
			SwingFinish();
			OnWeaponSwingEnd(false);
		}
		
		if(weaponTimer.Started && !weaponTimer.IsDone()) {

		}


		if (Interrupt) {
			SwingFinish();
			OnWeaponSwingEnd(true);
		}

	}

	public void Swing() {
		weaponTimer.Start();
		OnWeaponSwingStart();
	}

	void SwingFinish() {
		alreadyAttacked.Clear();
		Interrupt = false;
		executedFinish = true;
		weaponTimer.Stop();
	}

	public abstract void OnWeaponHit(Damagable damageManager);
	public abstract void OnWeaponSwingStart();
	public abstract void OnWeaponSwingEnd(bool Interrupted);




	public void WeaponTriggerEnter(DamagableMarker marker) {
		WeaponTriggerEnter(marker.DamageManager); 
	}

	public void WeaponTriggerEnter(Damagable damageManager) {

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
