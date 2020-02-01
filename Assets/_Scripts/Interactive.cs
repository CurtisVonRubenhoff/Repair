using System;
using UnityEngine;


public class Interactive : MonoBehaviour {
    private bool canUse = false;

    private void Update() {
        if (canUse) {
            if (Input.GetButtonDown("Interact")) {
                Debug.Log($"{gameObject.name} activated by player");
                Interact();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            Debug.Log($"{gameObject.name} <color=green>found</color> player");
            canUse = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            Debug.Log($"{gameObject.name} <color=red>lost</color> player");
            canUse = false;
        }
    }

    public virtual void Interact() {
        canUse = false;
        // Implement in child class
    }
}