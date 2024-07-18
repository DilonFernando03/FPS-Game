using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightScript : MonoBehaviour
{
    [SerializeField]
    private new Light light;
    [SerializeField]
    private Toggle noColorBlind, protanopia, deuteranopia, tritanopia;
    private Color defaultColor;

    // Update is called once per frame
    void Start()
    {   
        //save the original color of the light incase player does not have colorblindness
        defaultColor = light.color;
    }
    void Update()
    {
        //changes the color of the light depending on the colorblind mode picked
        if (protanopia.isOn == true)
        {
            light.color = Color.yellow;
        }
        if (deuteranopia.isOn == true)
        {
            light.color = Color.blue;
        }
        if (tritanopia.isOn == true)
        {
            light.color = Color.red;
        }
        if (noColorBlind.isOn == true)
        {
            light.color = defaultColor;
        }
    }
}
