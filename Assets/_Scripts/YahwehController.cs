using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class YahwehController: MonoBehaviour {


    
    public static void YouDidIt() {
        TextMaster.ShowText(
            new Message(
                false,
                "Well thank me you actually fucking did it.",
                "Yahweh"
        ));
    }

    public static void ScoldNoah(Animal fuckee, Animal fucker) {
        TextMaster.ShowText(
            new Message(
                false,
                Dictionaries.animalPairs[new Tuple<AnimalType, AnimalType>(fucker.animalType, fuckee.animalType)],
                "Yahweh"
        ));
    }
}
