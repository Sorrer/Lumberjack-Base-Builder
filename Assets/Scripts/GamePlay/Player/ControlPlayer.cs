using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlModes { Player, BaseBuilding}

public class ControlPlayer : MonoBehaviour {

	CharacterController playerController;
	public float speed = 7f;

	public Transform CameraPos;
	public Camera cam;
	public CameraMovement cameraMovement;
	public AttachTransform cameraAttachment;

	public KeyCode SwitchMode = KeyCode.B;

	void Start () {
		playerController = this.GetComponent<CharacterController>();
		
	}

	public ControlModes controlMode = ControlModes.Player;

	void BaseBuildingMovement() {
		
		//Initiates base building camera settings and movement
		if (initSwitchMode) {
			cameraMovement.ZoomEnabled = false;
			cam.transform.localPosition = new Vector3(0, 25, -4.408f);
			cam.transform.localRotation = Quaternion.Euler(80, 0, 0);
			cameraMovement.XZMovementEnabled = true;
			cameraAttachment.enabled = false;
			initSwitchMode = false;
		}


		//Switch to playermode when pressed
		if (Input.GetKeyDown(SwitchMode)) {
			print("Switching to Player");
			controlMode = ControlModes.Player;
			GlobalGame.ControlMode = ControlModes.Player;
			initSwitchMode = true;
		}

	}

	bool initSwitchMode = false;

	void Update() {

		//Prevent updates if game is paused
		if (GlobalGame.Paused) return;

		switch (controlMode) {
			case ControlModes.BaseBuilding:
				BaseBuildingMovement();
				return;
		}


		//When control mode is player, init to reset values back to normal.
		if (initSwitchMode) {
			cameraMovement.XZMovementEnabled = false;
			cameraMovement.ZoomEnabled = true;
			cameraAttachment.enabled = true;
			cameraMovement.ZoomUpdate();
			initSwitchMode = false;
		}



		


		//Switch to basebuilding when pressed
		if (Input.GetKeyDown(SwitchMode)) {
			print("Switching to BaseBuilding");
			controlMode = ControlModes.BaseBuilding;
			GlobalGame.ControlMode = ControlModes.BaseBuilding;
			initSwitchMode = true;
		}
	}
	

	private void FixedUpdate() {
		switch (controlMode) {
			case ControlModes.BaseBuilding:
				BaseBuildingMovement();
				return;
		}

		curMovement = Vector3.zero;
		sideMoveActive = false;
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


		//CONTROLLER INPUT 

		//TODO Make controller inputs change control mode (From pc to console and vis versa)

		//if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D) && lastMousePouse == Input.mousePosition) {

		//	SideMove(Input.GetAxis("HorizontalController"));
		//	ForwardMove(Input.GetAxis("VerticalController"));

		//	if (Vector3.Distance(new Vector3(0, 0, 0), curMovement) >= 0.02) {
		//		transform.rotation = Quaternion.LookRotation(curMovement);
		//	}

		//} else {
		//}

		UpdateAngle();
		

		transform.rotation = Quaternion.AngleAxis((-CurrentAngle) - 180, Vector3.up);


		if (!playerController.isGrounded)
			curMovement.y = -9.87f * Time.fixedDeltaTime;	
		playerController.Move(curMovement);
		curMovement = new Vector3(0,0,0);
	}

	[Header("Rotation")]

	public float TargetAngle = 0;
	public float RotationSpeed = 0.1f;
	public float MinAngleRotation = 0.1f;
	public float CurrentAngle = 0;

	void UpdateAngle() {

		TargetAngle = GetMouseAngle();

		//Normalize within 10 Revolutions
		TargetAngle = (TargetAngle + 3600) % 360;
		CurrentAngle = (CurrentAngle + 3600) % 360;

		//Calculate the distance from current to target
		float UpperDistance = Mathf.Abs(CurrentAngle - (TargetAngle + 360));
		float LowerDistance = Mathf.Abs(CurrentAngle - (TargetAngle - 360));
		float NormalDistance = Mathf.Abs(CurrentAngle - TargetAngle );
		
		//Find the smallest distance, than move towards it
		if(UpperDistance < NormalDistance || LowerDistance < NormalDistance) {
			
			if (UpperDistance < LowerDistance) {
				CurrentAngle += RotationSpeed * Time.fixedDeltaTime;
			} else {
				CurrentAngle -= RotationSpeed * Time.fixedDeltaTime;
			}

		} else {

			if(CurrentAngle < TargetAngle) {
				CurrentAngle += RotationSpeed * Time.fixedDeltaTime;
			} else {
				CurrentAngle -= RotationSpeed * Time.fixedDeltaTime;
			}
		}

		//Normalize angle back to 0-360
		CurrentAngle = (CurrentAngle + 3600) % 360;

		//If angle is in range, snap to it
		if (CurrentAngle > TargetAngle - MinAngleRotation && CurrentAngle < TargetAngle + MinAngleRotation) {
			CurrentAngle = TargetAngle;
		}
		
		//Also if angle is in range, snap to it (But make sures it passes the devide line [Cross between 360 to 0])
		if(UpperDistance > -MinAngleRotation+ 1 && UpperDistance < MinAngleRotation + 1) {
			CurrentAngle = TargetAngle;
		}




	}
	
	float GetMouseAngle() {
		return ((Mathf.Atan2(Input.mousePosition.y - GetCenterScreen().y, Input.mousePosition.x - GetCenterScreen().x) * 180 / Mathf.PI) + 180) - CameraPos.eulerAngles.y - 90;
	}

	Vector3 GetCenterScreen() {
		return new Vector3(Screen.width / 2, Screen.height / 2);
	}


	//float velocity = 9.87f;

	Vector3 curMovement = Vector3.zero;

	bool sideMoveActive = false;

	void SideMove(float amount) {
		//playerController.Move(this.CameraPos.right * speed * amount * Time.deltaTime);
		curMovement += this.CameraPos.right * speed * amount * Time.fixedDeltaTime;
		sideMoveActive = true;
	}

	void ForwardMove(float amount) {
		//playerController.Move(this.CameraPos.forward * speed * amount * Time.deltaTime);

		if (sideMoveActive) {
			curMovement *= 0.75f;
			curMovement += this.CameraPos.forward * speed * amount * Time.fixedDeltaTime * 0.75f;
		} else {
			curMovement += this.CameraPos.forward * speed * amount * Time.fixedDeltaTime;
		}
	}
}
