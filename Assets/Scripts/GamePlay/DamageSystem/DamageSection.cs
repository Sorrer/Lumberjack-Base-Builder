using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Shields serve same function of health bars, but maybe easy regen
//Armor reduces damage
public enum HealthType {Health, Shields ,Armor };

[System.Serializable]
public class DamageSection 
{

	public HealthType Type = HealthType.Health;

	public int MaxValue;
	public int Value;


	[Range(0f,1f)]
	/**
	 * 0% all damage is recieved, 100% no damage;
	 * */
	public float DamageReduction = 1;

}
