using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Toggle noColorBlind, protanopia, deuteranopia, tritanopia;

    void Update()
    {
        //changes the color of the health bar depending on the color blind mode picked
        if (protanopia.isOn == true)
        {
            image.color = Color.yellow;
        }
        if (deuteranopia.isOn == true)
        {
            image.color = Color.blue;
        }
        if (tritanopia.isOn == true)
        {
            image.color = Color.red;
        }
        if (noColorBlind.isOn == true)
        {
            image.color = Color.red;
        }
    }

    //sets healthbar value to max on game start
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

    }

    //this method updates the healthbar value throughout the game
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    //this method will be called when the Player or AI dies
    public void DestroyHealthBar()
    {
        Destroy(gameObject);
    }

    //this method returns the current health of the player or AI
    public float GetHealth()
    {
        return slider.value;
    }


}
