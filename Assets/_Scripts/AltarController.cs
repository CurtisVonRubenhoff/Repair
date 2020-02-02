using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarController : Interactive
{

    private int childCount = 0;

    private bool introComplete = false;
    private int currentMessage = 0;

    [SerializeField]
    private List<Message> introMessages = new List<Message>();

    [SerializeField]
    private GameObject Wood;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Interact() {
        base.Interact();

        if (introComplete) {
            ClaimChild();
        } else {
            DoIntro();
        }
    }

    private void DoIntro() {
        TextMaster.ShowText(introMessages[currentMessage]);

        currentMessage++;

        if (currentMessage == introMessages.Count) {
            introComplete = true;
        } else {
            canUse = true;
        }

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

        if (toBeKilled.Count == 0 && childCount < 8) {
            TextMaster.ShowText(new Message(false, "Noah, what the FUCK are you DOING. BRING ME THE YOUNG!!", "Yahweh"));
        }

        foreach (var wood in toFollow) {
            wood.GetCarried();
        }

    }
}