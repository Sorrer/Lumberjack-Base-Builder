using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIController : MonoBehaviour {

	public GenericAI genericAI;
	public Damagable DamageManager;

	public string CurrentStatus = "";
	public AIMode Mode = AIMode.Idling;

	[HideInInspector]
	public bool foundAllBaseComponents = false;

	public void setBaseComponents() {
		if (foundAllBaseComponents) return;


		genericAI = GetComponent<GenericEnemy>();
		DamageManager = GetComponentInChildren<Damagable>();


		if (genericAI != null) {
			genericAI.controller = this;
		}
		

		if(genericAI != null && DamageManager != null) {
			this.foundAllBaseComponents = true;
			this.DamageManager.genericAI = genericAI;
		}


	}

	public PlayerMain GetPlayer() {
		return GlobalGame.Player;
	}

}
