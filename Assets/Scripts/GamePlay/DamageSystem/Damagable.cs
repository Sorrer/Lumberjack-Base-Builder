using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {

	public float health = 0;
	public float shield = 0;
	public List<float> armourHold; // WIP: to hold armour objects that add to shield
	bool alive;
	float maxHealth = float.MaxValue;

	// Use this for initialization
	void Start() {
		armourHold = new List<float>();
	}

	// Update is called once per frame
	void Update() {
		if (health >= maxHealth) {
			health = maxHealth;
		}
		if (health <= 0) {
			health = 0;
		}
	}

	void isDead() {
		alive = false;
	}

	// Deals damage to object health / sheild
	public void damage(float damage) {
		if (shield == 0) {
			health -= damage;
		} else if (shield <= damage) {
			health -= damage - shield;
			shield = 0;
		} else if (shield > damage) {
			loseShield(damage);
		}

	}

	public void heal(float heal) {
		health += heal;
	}

	// Multiplies health by amt
	public void multiHeal(float heal) {
		health *= heal;
	}

	public void addShield(float amt) {
		shield += amt;
	}

	public void loseShield(float amt) {
		shield -= amt;
	}

	public void addArmour(float amt) {
		armourHold.Add(amt);
	}
}
