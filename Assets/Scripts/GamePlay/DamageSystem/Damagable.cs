using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {

	public float health = 0;
	public float shield = 0;
	bool alive = true;
	float maxHealth = float.MaxValue;

	// Use this for initialization
	void Start() {
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

	bool isDead() {
		return alive;
	}

	// Deals damage to object health / sheild
	public void damage(float damage) {
		if (shield == 0) {
			health -= damage;
		} else if (shield <= damage) {
			health -= damage - shield;
			shield = 0;
		} else if (shield > damage) {
			damageShield(damage);
		}

		if(health <= 0) {
			alive = false;
		}

	}

	public void heal(float heal) {
		health += heal;
	}

	// Multiplies health by amt
	public void multiplyHeal(float heal) {
		health *= heal;
	}

	public void addShield(float amt) {
		shield += amt;
	}

	public void damageShield(float amt) {
		shield -= amt;
	}
	
}
