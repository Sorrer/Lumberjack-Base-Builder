using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ToggleUIElement {

	public Canvas canvas;

	public KeyCode key;

	public bool EnabledOnStart = false;

	[Space(8)]

	[Header("Spawn Canvas")]

	public bool SpawnOnStart = false;

	public Canvas Prefab;
	public Transform Parent;
	[Space(4)]

	public Camera CameraAttachment;
}
