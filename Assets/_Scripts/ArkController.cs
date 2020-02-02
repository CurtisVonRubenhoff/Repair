using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArkController : Interactive
{
    [SerializeField]
    private int woodCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Interact() {
        Debug.Log("doing thing");
        base.Interact();
        if (woodCount == 8) {
            YahwehController.YouDidIt();
        } else {
            ClaimWood();
        }
    }

    private void ClaimWood() {
        var gm = GameManager.instance;
        var player = gm.player;
        List<Animal> toBeKilled = new List<Animal>();

        Debug.Log("looking for children");

        for(var i = 0; i < player.followTrail.Count; i++) {
            var test = player.followTrail[i];

            Debug.Log($"looking at {test.gameObject.name}");

            if (test.animalType == AnimalType.WOOD) {
                Debug.Log("found child");
                
                woodCount++;
                toBeKilled.Add(test);
            }
        }

        foreach (var vermin in toBeKilled) {
            player.followTrail.Remove(vermin);
            player.MakeChain();

            Destroy(vermin.gameObject);
        }

        if (toBeKilled.Count == 0 && woodCount < 8) {
            TextMaster.ShowText(new Message(false, "You do not have enough wood to fix the Ark.", ""));
        }

        if (woodCount == 8) {
            YahwehController.YouDidIt();
        }
    }
}
