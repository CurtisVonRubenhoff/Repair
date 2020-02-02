using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CopyText : MonoBehaviour
{

    [SerializeField]
    Text mainBox;

    [SerializeField]
    Text myBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myBox.text = mainBox.text;

        float h, s, v;

        var curr = myBox.color;
        Color.RGBToHSV(curr, out h, out s, out v);
        h += .001f;
        if (h > 1.0f) h = 0f;

        var newColor = Color.HSVToRGB(h, s, v);

        myBox.color = newColor;
    }
}
