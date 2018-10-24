using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTrigger : MonoBehaviour {

	public Weapon weapon;

	private void OnTriggerEnter(Collider other) {
		if(weapon == null) {
			Debug.Log("No weapon declared for me! " + this.gameObject.name + " (" + this.transform.parent.gameObject + ") " );
			return;
		}

		DamagableMarker marker = other.GetComponent<DamagableMarker>();
		Damagable damageManager = other.GetComponent<Damagable>();


		if (marker != null) {
			weapon.WeaponTriggerEnter(marker);
		} else if(damageManager != null) {
			weapon.WeaponTriggerEnter(damageManager);
		}
		
	}
}
