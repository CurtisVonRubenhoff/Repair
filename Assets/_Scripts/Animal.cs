using System;
using System.Collections.Generic;
using UnityEngine;
using Klak.Motion;

public enum AnimalType {
    GOAT,
    CAT,
    CHILD,
    WOOD,
    APE,
    DUCK,
    ELEPHANT,
    SLOTH,
    TURTLE,
    KANGAROO
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
    public SmoothFollow mySmooth;

    [SerializeField]
    private AudioSource myNoise;


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

                // take the match
                BeMine(newPartner);
                newPartner.amMatched = true;

                // give the player my partner
                if (myPartner != null) {
                    myPartner.GetCarried();
                }

                CreateChild();
                GameManager.instance.player.MakeChain();
            } else {
                // Deny the match
                Debug.Log("nah");
            }
        }
    }

    private bool CheckForMatch(Animal protentialPartner) {
        return protentialPartner.animalType == myLove;
    }

    public void GetCarried() {
        var col = gameObject.GetComponent<SphereCollider>();
        var player = GameManager.instance.player;

        if (col != null) {
            col.enabled = false;
        }

        if (isAdultAnimal()) {
            myPartner.myPartner = null;
            GameManager.instance.playerCarrying = this;
        }

        var pc = player;

        myPartner = null;
        pc.AcceptFollower(this);
        pc.MakeChain();
        mySmooth.enabled = true;

        if (myNoise != null) {
            myNoise.Play();
        } 
        //transform.SetParent(player.transform);
    }

    private void CreateChild() {
        // ding dong
        Debug.Log("yup I am born");

        var child = GameObject.Instantiate(myChild, transform.position, transform.rotation);

        child.GetComponent<Animal>().GetCarried();
    }

    public override void Interact() {
        Debug.Log("doink");
        base.Interact();

        var player = GameManager.instance.player;

        var amICarried = player.followTrail.Contains(this);
        var carried = GameManager.instance.playerCarrying;

        // don't do anything if i'm the one being carried
        if (!amICarried) {
            if (carried == null) {
                // the player isn't carrying anything, so pick me up
                GetCarried();
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

    public bool isAdultAnimal() {
        return animalType != AnimalType.CHILD && animalType != AnimalType.WOOD;
    }

    public void BeMine(Animal match) {
        var player = GameManager.instance.player;
        // the match no longer needs to be a child of the player
        //atch.transform.SetParent(null);
        match.mySmooth.enabled = false;
        player.followTrail.Remove(match);
        player.MakeChain();
        // mySmooth._target = null;
        
        // put animal next to me
        match.gameObject.transform.position = partnerSpot.transform.position;
        //match.gameObject.transform.rotation = partnerSpot.transform.rotation;

        // set the match's partnerspot to the one I'm on
        match.partnerSpot = partnerSpot.neighborSpot;
        if (myNoise != null)
            myNoise.Play();
    }
}