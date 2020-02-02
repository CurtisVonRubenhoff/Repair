using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CycleColor1 : MonoBehaviour {
    [SerializeField]
    Light thing;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float h, s, v;

        var curr = thing.color;
        Color.RGBToHSV(curr, out h, out s, out v);
        h += .001f;
        if (h > 1.0f) h = 0f;

        var newColor = Color.HSVToRGB(h, s, v);

        thing.color = newColor;
	}
}