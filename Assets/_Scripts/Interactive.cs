using System;
using UnityEngine;


public class Interactive : MonoBehaviour {
    private bool canUse = false;

    private void Update() {
        if (canUse) {
            if (Input.GetButtonDown("Interact")) {
                Interact();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            canUse = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            canUse = false;
        }
    }

    public virtual void Interact() {
        canUse = false;
        // Implement in child class
    }
}