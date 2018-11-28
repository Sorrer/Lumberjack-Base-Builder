using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Damagable : MonoBehaviour {

	[Header("Settings")]
	[Space(5)]

	public bool Invincible = false;

	[Space(2)]
	public bool PermDeath = true; // Can not be revived
	[Space(2)]
	public bool MaxOutCurrentHealth = true;
	[Space(2)]
	public bool IsPlayer = false;


	[Space(2)]
	[SerializeField] private bool alive = false;

	[Space(5)]
	///Health - Last to first
	[SerializeField]
	private List<DamageSection> _healthArray = new List<DamageSection>();

	[Space(5)]

	[ReadOnly] [SerializeField] private int TotalMaxHealth = 0;
	[ReadOnly] [SerializeField] private int TotalHealth = 0;
	[ReadOnly] public int LastRemainingDamage = 0;

	[HideInInspector]
	public GenericAI genericAI;
	

	public void Start() {

		Health();
		UpdateMaxHealth();

		if (MaxOutCurrentHealth) {
			GodHeal();
		}

	}


	//------------ Main Functions ------------------
		// 1. Damage
		// 2. Regen
		// 3. Revive
		// 4. Misc


	/// <summary>
	///	Used to damage class overall
	/// </summary>
	/// <param name="Damage">Amount of damage to inflict onto this class</param>
	/// <returns>If not alive returns false</returns>
	public bool Damage(int value) {
		if (Invincible) return true;


		int remainingValue = value;

		for(int i = healthArray.Count - 1; i > -1; i--) {
			DamageSection section = healthArray[i];

			if (section.Value > 0) {

				//If armor, thant reduce damage by armor amount
				if(section.Type == HealthType.Armor && section.Value > 0) {

					float temp = (float)remainingValue * (1 - section.DamageReduction);

					if(temp % 1 == 0) {
						remainingValue = (int)temp;
					} else {
						remainingValue = (int)Math.Ceiling(temp);
					}


				}

				remainingValue -= section.Value;

				if(remainingValue < 0) {
					section.Value -= section.Value + remainingValue;
					break;
				} else {
					section.Value = 0;
				}

			}

		}

		

		return IsAlive();
	}
	

	//REGEN CAN BE Optimized
	public void RegenTotal(int amount) {

		if (!IsAlive()) return;

		foreach (DamageSection section in healthArray) {
			if(section.Value >= section.MaxValue) {
				section.Value = section.MaxValue;
				continue;
			}

			amount -= section.MaxValue - section.Value;

			if (amount < 0) {
				section.Value -= amount;
				break;
			} else {
				section.Value = section.MaxValue;
			}

		}
		
	}
	
	public void Regen(int health, int shields, int armor) {

		if (!IsAlive()) return;

		foreach (DamageSection section in healthArray) {

			if (section.Value >= section.MaxValue) {
				section.Value = section.MaxValue;
				continue;
			}
			//Sets up amount to be used (Saving lines of code)
			int amount = 0;

			switch (section.Type) {
				case HealthType.Health:
					amount = health;
					break;
				case HealthType.Shields:
					amount = shields;
					break;
				case HealthType.Armor:
					amount = armor;
					break;
			}

			if(amount == 0) {
				continue;
			}

			amount -= section.MaxValue - section.Value;

			if (amount < 0) {
				section.Value -= amount;
				amount = 0;
			} else {
				section.Value = section.MaxValue;
			}

			//Replaces type value with amount used
			switch (section.Type) {
				case HealthType.Health:
					health = amount;
					break;
				case HealthType.Shields:
					shields = amount;
					break;
				case HealthType.Armor:
					armor = amount;
					break;
			}

		}
		
	}

	public void MaxHealth() { Regen(Health(), 0, 0); }
	public void MaxShields() { Regen(0, Health(), 0); }
	public void MaxArmor() { Regen(0, 0, Health()); } 

	public void RegenHealth(int health) {
		Regen(health, 0, 0);
	}

	public void RegenShields(int shields) {
		Regen(0, shields, 0);
	}
	
	public void RegenArmor(int armor) {
		Regen(0, 0, armor);
	}
	

	


	public bool Revive() {
		return Revive(false, 1);
	}

	public bool Revive(bool reviveFull) {
		return Revive(reviveFull, -1);
	}

	public bool Revive(int amount) {
		return Revive(false, amount);
	}

	private bool Revive(bool reviveFull, int amount) {

		if ((!IsAlive() && PermDeath) || IsAlive()) {
			return false;
		}

		if (reviveFull) { GodHeal(); } else {
			foreach (DamageSection section in healthArray) {
				if (section.Value >= section.MaxValue) {
					section.Value = section.MaxValue;
					continue;
				}

				amount -= section.MaxValue - section.Value;

				if (amount < 0) {
					section.Value -= amount;
					break;
				} else {
					section.Value = section.MaxValue;
				}

			}
		}
		
		

		return true;
	}

	//Revives even if perm death
	public bool GodHeal() {

		foreach (DamageSection section in healthArray) {
			section.Value = section.MaxValue;
		}

		return IsAlive();
	}

	// ------------ Getters and Setters ------------------

	public List<DamageSection> healthArray {
		get {
			return _healthArray;
		}
		set {

			if(value == null || value.Count == 0) {
				Debug.LogWarning("Health array is null or count is 0 when set!");
				return;
			}

			healthArray = value;
			Health();
			UpdateMaxHealth();
		}
	}


	// ------------ Health values section ----------------

	public float HealthPercent() {
		return (float)Health() / (float)TotalMaxHealth;
	}


	public int Health() {
		TotalHealth = 0;

		foreach (DamageSection sec in healthArray) {
			TotalHealth += sec.Value;
		}

		return TotalHealth;
	}

	public int UpdateMaxHealth() {
		TotalMaxHealth = 0;

		foreach(DamageSection sec in healthArray) {
			TotalMaxHealth += sec.MaxValue;
		}

		return TotalMaxHealth;
	}
	
	public bool IsAlive() {
		alive = false;

		if (Health() > 0) alive = true;

		return alive;

	}
	//Implement other damage class when nessecary (Specifically health, shields, or armor)

}
