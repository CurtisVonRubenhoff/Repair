using System;
using System.Collections.Generic;
using UnityEngine;

public enum AnimalType {
    GOAT,
    MOLE,
    CHILD
}

public class Animal : Interactive {
    public AnimalType animalType; 

    public AnimalType myLove;
    public Animal myPartner;
    private bool amMatched;

    [SerializeField]
    private GameObject myChild;

    [SerializeField]
    PartnerSpot partnerSpot;

    private List<Animal> previousMatches = new List<Animal>();

    public void TakeNewPartner(Animal newPartner) {
        // don't do anything if i'm with my love
        if (amMatched) {
            Debug.Log("no way, jose");
        } else {
            var isMatch = CheckForMatch(newPartner);

            if (isMatch) {
                Debug.Log("my love, how i missed you");
                amMatched = true;

                // give the player my partner
                if (myPartner != null) {
                    myPartner.GetCarried(GameManager.instance.player);
                }

                // take the match
                BeMine(newPartner);
                newPartner.amMatched = true;

                CreateChild();
            } else {
                Debug.Log("who dis be");

                // check if we've been matched with this animal before
                var isPreviousMatch = !(previousMatches.Find(x => x == newPartner) == null);

                if (isPreviousMatch) {
                    // Deny the match
                    Debug.Log("nah");

                } else {
                    // Take the match
                    Debug.Log("fine for now");
                    BeMine(newPartner);
                }
            }
        }
    }

    private bool CheckForMatch(Animal protentialPartner) {
        return protentialPartner.animalType == myLove;
    }

    public void GetCarried(GameObject player) {
        if (!(animalType == AnimalType.CHILD)) {
            myPartner.myPartner = null;
            GameManager.instance.playerCarrying = this;
        }

        myPartner = null;
        transform.SetParent(player.transform);
    }

    private void CreateChild() {
        // ding dong
        Debug.Log("yup I am born");

        var child = GameObject.Instantiate(myChild, transform.position, transform.rotation);

        child.GetComponent<Animal>().GetCarried(GameManager.instance.player);
    }

    public override void Interact() {
        Debug.Log("doink");
        base.Interact();

        var amICarried = GameManager.instance.playerCarrying == this;
        var carried = GameManager.instance.playerCarrying;

        // don't do anything if i'm the one being carried
        if (!amICarried) {
            if (carried == null) {
                // the player isn't carrying anything, so pick me up
                GetCarried(GameManager.instance.player);
            } else {
                // the player is offering me a partner

                var isChild = carried.animalType == AnimalType.CHILD;

                if (isChild) {
                    Debug.Log("that's obscene");
                } else {
                    // animals only take fully-grown partners
                    TakeNewPartner(carried);
                }
            } 
        }
    }

    public void BeMine(Animal match) {
        // the match no longer needs to be a child of the player
        match.transform.SetParent(null);
        
        // put animal next to me
        match.gameObject.transform.position = partnerSpot.transform.position;
        match.gameObject.transform.rotation = partnerSpot.transform.rotation;

        // set the match's partnerspot to the one I'm on
        match.partnerSpot = partnerSpot.neighborSpot;
    }
}