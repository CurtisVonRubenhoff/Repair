using System.Collections;
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

        Debug.Log("looking for children");

        for(var i = 0; i < player.followTrail.Count; i++) {
            var test = player.followTrail[i];

            if (test.animalType == AnimalType.CHILD) {
                player.followTrail.Remove(test);
                player.MakeChain();
                childCount++;

                Destroy(test.gameObject);

                var wood = GameObject.Instantiate(Wood, transform.position, transform.rotation);

                wood.GetComponent<Animal>().GetCarried();
            }
        }
    }
}
