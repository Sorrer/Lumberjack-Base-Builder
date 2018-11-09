using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : Entity
{

	public ControlPlayer controlPlayer;

    // Start is called before the first frame update
    void Start()
    {
		GlobalGame.Player = this;
		setBaseComponents();

	}


	bool foundAllBaseComponents = false;

    // Update is called once per frame
    void Update()
    {
		if (!foundAllBaseComponents) {
			setBaseComponents();
		}


    }


	public void setBaseComponents() {
		if (foundAllBaseComponents) return;

		GetComponent<ControlPlayer>();
		GetComponent<Damagable>();


	}
}
