using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Animal playerCarrying;

    public static GameManager instance;

    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance == null) {
            GameManager.instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
