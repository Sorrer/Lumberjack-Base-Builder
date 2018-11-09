using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage : MonoBehaviour {
    [SerializeField]
    private int numRows, numCols;

    [SerializeField]
    private bool canPickUp;

    private GameObject[,] storage;

    // Use this for initialization
    void Start() {
        storage = new GameObject[numRows, numCols];
        Debug.Log(storage.Length);
    }

    public bool HasSpace() {
        for (int i = 0; i < numRows; i++) {
            for (int j = 0; j < numCols; j++)
                if (storage[i, j] == null)
                    return true;
        }

        return false;
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log("Touching " + collision.gameObject.name);
        if (canPickUp && HasSpace() && collision.gameObject.GetComponent<Item>() != null) {
            PickupItem(collision.gameObject);
        }
    }

    private void PickupItem(GameObject g) {
        for (int i = 0; i < numRows; i++) {
            for (int j = 0; j < numCols; j++) {
                Item pItem = g.GetComponent<Item>();

                if (storage[i, j] == null) {
                    Debug.Log("done");
                    storage[i, j] = g;
                    pItem.PickupItem(gameObject);
                    return;
                }
            }
        }
    }
}
