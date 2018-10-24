using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {


	public List<WeaponTrigger> Triggers = new List<WeaponTrigger>();
	
	//Clear list after attack is over
	List<Damagable> alreadyAttacked = new List<Damagable>();

	void Start () {
		foreach(WeaponTrigger trigger in Triggers) {
			trigger.weapon = this;
		}
	}
	
	void Update () {

	}


	public void WeaponTriggerEnter(DamagableMarker marker) {
		WeaponTriggerEnter(marker.DamageManager);
	}

	public void WeaponTriggerEnter(Damagable damageManager) {
		print("oof");


	}

	

}
