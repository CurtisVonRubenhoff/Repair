using System;
using UnityEngine;


public class Interactive : MonoBehaviour {
    private bool canUse = false;

    public virtual void Update() {
        if (canUse) {
            if (Input.GetButtonDown("Interact")) {
                Debug.Log($"{gameObject.name} activated by player");
                Interact();
            }
        }
    }


    public virtual void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Player") {
            TextMaster.IndicatorOn(gameObject.name);
            Debug.Log($"{gameObject.name} <color=green>found</color> player");
            canUse = true;
        }
    }

    public virtual void OnTriggerExit(Collider col) {
        if (col.gameObject.tag == "Player") {
            TextMaster.EndText();
            Debug.Log($"{gameObject.name} <color=red>lost</color> player");
            canUse = false;
        }
    }

    public virtual void Interact() {
        canUse = false;
        TextMaster.EndText();
        // Implement in child class
    }
}