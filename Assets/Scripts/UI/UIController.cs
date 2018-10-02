using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

	public List<ToggleUIElement> ToggleActiveWithButton = new List<ToggleUIElement>();

	void Start () {
	}

	bool initiated = false;

	void Update () {
		if (!initiated) {
			initiated = true;
			foreach (ToggleUIElement ui in ToggleActiveWithButton) {
				if (ui.SpawnOnStart) {
					ui.canvas = Instantiate(ui.Prefab, ui.Parent);
				}

				if (ui.CameraAttachment != null) {
					ui.canvas.worldCamera = ui.CameraAttachment;
				}
				if (ui.canvas != null) {


					ui.canvas.gameObject.SetActive(ui.EnabledOnStart);

				}
			}
		}
		
		foreach(ToggleUIElement ui in ToggleActiveWithButton) {
			if (Input.GetKeyDown(ui.key)) {
				if (ui.canvas != null) {
					bool setToggle = !ui.canvas.gameObject.activeSelf;

					ui.canvas.gameObject.SetActive(setToggle);

				}
			}
		}

	}
}
