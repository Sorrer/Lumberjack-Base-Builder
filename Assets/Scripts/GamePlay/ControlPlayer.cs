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
		if (initSwitchMode) {
			cameraMovement.ZoomEnabled = false;
			cam.transform.localPosition = new Vector3(0, 25, -4.408f);
			cam.transform.localRotation = Quaternion.Euler(80, 0, 0);
			cameraMovement.XZMovementEnabled = true;
			cameraAttachment.enabled = false;
			initSwitchMode = false;
		}


		if (Input.GetKeyDown(SwitchMode)) {
			print("Switching to Player");
			controlMode = ControlModes.Player;
			initSwitchMode = true;
		}

	}

	bool initSwitchMode = false;

	void Update() {
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

		if (Input.GetKeyDown(SwitchMode)) {
			print("Switching to BaseBuilding");
			controlMode = ControlModes.BaseBuilding;
			initSwitchMode = true;
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



		//if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D) && lastMousePouse == Input.mousePosition) {

		//	SideMove(Input.GetAxis("HorizontalController"));
		//	ForwardMove(Input.GetAxis("VerticalController"));

		//	if (Vector3.Distance(new Vector3(0, 0, 0), curMovement) >= 0.02) {
		//		transform.rotation = Quaternion.LookRotation(curMovement);
		//	}
			
		//} else {
		//}

			UpdateAngle();

		lastMousePouse = Input.mousePosition;

		transform.rotation = Quaternion.AngleAxis((-CurrentAngle) - 180, Vector3.up);
		

	}

	Vector3 lastMousePouse = new Vector3();

	private void FixedUpdate() {
		if (!playerController.isGrounded)
			curMovement.y = -9.87f * Time.deltaTime;	
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


		float UpperDistance = Mathf.Abs(CurrentAngle - (TargetAngle + 360));
		float LowerDistance = Mathf.Abs(CurrentAngle - (TargetAngle - 360));
		float NormalDistance = Mathf.Abs(CurrentAngle - TargetAngle );
		
		if(UpperDistance < NormalDistance || LowerDistance < NormalDistance) {
			
			if (UpperDistance < LowerDistance) {
				print("Upper" + UpperDistance + " " + NormalDistance);
				CurrentAngle += RotationSpeed;
			} else {
				print("Lower" + UpperDistance + " " + NormalDistance);
				CurrentAngle -= RotationSpeed;
			}

		} else {

			if(CurrentAngle < TargetAngle) {
				CurrentAngle += RotationSpeed;
			} else {
				CurrentAngle -= RotationSpeed;
			}
		}

		CurrentAngle = (CurrentAngle + 3600) % 360;

		if (CurrentAngle > TargetAngle - MinAngleRotation && CurrentAngle < TargetAngle + MinAngleRotation) {
			CurrentAngle = TargetAngle;
		}
		
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


	float velocity = 9.87f;

	Vector3 curMovement = Vector3.zero;

	bool sideMoveActive = false;

	void SideMove(float amount) {
		//playerController.Move(this.CameraPos.right * speed * amount * Time.deltaTime);
		curMovement += this.CameraPos.right * speed * amount * Time.deltaTime;
		sideMoveActive = true;
	}

	void ForwardMove(float amount) {
		//playerController.Move(this.CameraPos.forward * speed * amount * Time.deltaTime);

		if (sideMoveActive) {
			curMovement *= 0.5f;
			curMovement += this.CameraPos.forward * speed * amount * Time.deltaTime * 0.5f;
		} else {
			curMovement += this.CameraPos.forward * speed * amount * Time.deltaTime;
		}
	}
}
