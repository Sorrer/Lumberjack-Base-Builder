using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AITypes { Enemy, Tree }

public abstract class GenericAI : Entity
{
	public AIController controller;
}
