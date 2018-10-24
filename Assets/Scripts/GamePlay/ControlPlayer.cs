using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlModes { Player, RTS}

public class ControlPlayer : MonoBehaviour {

	CharacterController playerController;
	public float speed = 7f;

	public Transform CameraPos;
	public Transform PlayerGroup;

	void Start () {
		playerController = this.GetComponent<CharacterController>();
		playerController.detectCollisions = true;
		playerController.enabled = true;
		playerController.enableOverlapRecovery = true;
	}

	public ControlModes controlMode = ControlModes.Player;

	void RTSMovement() {
		if (initSwitchMode) {

			initSwitchMode = false;
		}


	}

	bool initSwitchMode = false;
	
	void Update () {
		switch (controlMode) { 
			case ControlModes.RTS:
				initSwitchMode = true;
				RTSMovement();
				return;
		}

		if (initSwitchMode) {

			initSwitchMode = false;
		}


		curMovement = Vector3.zero;

		if (Input.GetKey(KeyCode.A)) {
			SideMove(-1);
		}

		if (Input.GetKey(KeyCode.D)) {
			SideMove(1);
		}

		if (Input.GetKey(KeyCode.W)) {
			ForwardMove(1);
		}

		if (Input.GetKey(KeyCode.S)) {
			ForwardMove(-1);
		}
		


		if (!Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.S) || !Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.D)) {

			SideMove(Input.GetAxis("HorizontalController"));
			ForwardMove(Input.GetAxis("VerticalController"));

		}

		if(Vector3.Distance(new Vector3(0,0,0), curMovement) >= 0.02) {
			transform.rotation = Quaternion.LookRotation(curMovement);
		}



		playerController.Move(new Vector3(0,-velocity,0));
		CameraPos.position = this.transform.position;
	}

	float velocity = 0;

	Vector3 curMovement = Vector3.zero;

	void SideMove(float amount) {
		playerController.Move(this.CameraPos.right * speed * amount * Time.deltaTime);
		curMovement += this.CameraPos.right * speed * amount * Time.deltaTime;

	}

	void ForwardMove(float amount) {
		playerController.Move(this.CameraPos.forward * speed * amount * Time.deltaTime);
		curMovement += this.CameraPos.forward * speed * amount * Time.deltaTime;
	}
}
