using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AITypes { Enemy, Tree }
public enum EnemyType { Generic_Enemy }
public enum TreeType { Generic_Tree }


public abstract class GenericAI : Entity
{
	public AIController controller;
}
