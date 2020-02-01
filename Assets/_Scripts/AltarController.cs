﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarController : Interactive
{

    private int childCount = 0;

    [SerializeField]
    private GameObject Wood;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Interact() {
        Debug.Log("doing thing");
        base.Interact();
        ClaimChild();
    }

    private void ClaimChild() {
        var gm = GameManager.instance;
        var player = gm.player;
        List<Animal> toBeKilled = new List<Animal>();
        List<Animal> toFollow = new List<Animal>();

        Debug.Log("looking for children");

        for(var i = 0; i < player.followTrail.Count; i++) {
            var test = player.followTrail[i];

            Debug.Log($"looking at {test.gameObject.name}");

            if (test.animalType == AnimalType.CHILD) {
                Debug.Log("found child");
                var wood = GameObject.Instantiate(Wood, transform.position, transform.rotation);
                
                childCount++;
                toBeKilled.Add(test);
                toFollow.Add(wood.GetComponent<Animal>());
            }
        }

        foreach (var vermin in toBeKilled) {
            player.followTrail.Remove(vermin);
            player.MakeChain();

            Destroy(vermin.gameObject);
        }

        foreach (var wood in toFollow) {
            wood.GetCarried();
        }

    }
}
