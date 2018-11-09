using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {
    [SerializeField]
    private int
        curStack = 1,
        maxStack = 1;

    [SerializeField]
    private string
        itemID = "item.generic.item";

	// Use this for initialization
	void Start () {
        InitItem();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateItem(null);
	}

    /// <summary>
    /// Initializes the item when it is spawned in
    /// </summary>
    public abstract void InitItem();

    /// <summary>
    /// Updates the item every second
    /// </summary>
    /// <param name="src">Item holder</param>
    public abstract void UpdateItem(GameObject src);

    /// <summary>
    /// Uses the item
    /// </summary>
    /// <param name="src">Item holder</param>
    public abstract void UseItem(GameObject src);

    /// <summary>
    /// Picks up the item
    /// </summary>
    /// <param name="src">Item holder</param>
    public virtual void PickupItem(GameObject src) {
        transform.position = new Vector3(-999, -999, -999);

        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
            rb.useGravity = false;
    }

    /// <summary>
    /// Drops the item
    /// </summary>
    /// <param name="src">Item holder</param>
    public virtual void DropItem(GameObject src) {
        curStack--;

        if (curStack == 0) {
            Destroy(gameObject);
        }
    }

    public int Stack {
        get {
            return curStack;
        }

        set {
            curStack = value;
        }
    }

    public int MaxStack {
        get {
            return maxStack;
        }
    }

    public string ID {
        get {
            return itemID;
        }

        set {
            itemID = value;
        }
    }
}
